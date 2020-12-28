using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { Medicine, Toys, GetSeated };

public class RequestAI : MonoBehaviour
{
    [SerializeField] bool timeIsReducing = false;
    [SerializeField] ItemType requiredObject;

    [SerializeField] float maxRequestGap = 30;
    [SerializeField] float currentRequestGap = 30;
    [SerializeField] bool gapActive = false;
    [SerializeField] ItemType[] requestsList;
    [SerializeField] Slider timeSlider;
    [SerializeField] Gradient timeGradient;
    [SerializeField] Image timeFill;
    // Start is called before the first frame update
    void Start()
    {
        SetMaxSatisfaction(10f, requiredObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("DebugComplete"))
        {
            CheckRequirement(ItemType.GetSeated);
        }
        if (timeIsReducing)
        {
            timeSlider.value -= Time.deltaTime;
            timeFill.color = timeGradient.Evaluate(timeSlider.value / timeSlider.maxValue);
            if (timeSlider.value <= 0)
            {
                timeIsReducing = false;
                // call timer fail
                Debug.Log("Timer Failed 1");
            }
        }

        if (gapActive)
        {
            currentRequestGap -= Time.deltaTime;
            if(currentRequestGap <= 0)
            {
                gapActive = false;
                timeSlider.gameObject.SetActive(true);
                //select new request
                SetMaxSatisfaction(30f, requestsList[Random.Range(0, System.Enum.GetValues(typeof(ItemType)).Length - 1)]);
            }
        }
    }

    public void SetMaxSatisfaction(float max, ItemType obj)
    {
        timeSlider.maxValue = max;
        timeSlider.value = timeSlider.maxValue;
        timeFill.color = timeGradient.Evaluate(1f);
        timeIsReducing = true;
        requiredObject = obj;
    }

    public void CheckRequirement(ItemType obj)
    {
        if (requiredObject == obj)
        {
            timeSlider.maxValue = 0;
            timeSlider.value = 0;
            timeIsReducing = false;
            currentRequestGap = maxRequestGap;
            gapActive = true;
            timeSlider.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Timer Failed 2");
        }
    }
}
