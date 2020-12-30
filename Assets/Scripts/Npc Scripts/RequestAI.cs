using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { Toys, Medicine, Doctor, DoctorKit, None };

public class RequestAI : MonoBehaviour
{
    [SerializeField] bool timeIsReducing = false;
    [SerializeField] ItemType requiredObject;
    [SerializeField] float requestTime;

    [SerializeField] float maxRequestGap = 30;
    [SerializeField] float currentRequestGap = 30;
    [SerializeField] bool gapActive = false;

    public bool GapActive { get => gapActive; }

    [SerializeField] ItemType[] requestsList;
    [SerializeField] GameObject[] imageList;
    [SerializeField] GameObject afImage;
    [SerializeField] GameObject textBubble;
    [SerializeField] int listIndex = 0;
    [SerializeField] Slider timeSlider;
    [SerializeField] Gradient timeGradient;
    [SerializeField] Image timeFill;

    [SerializeField] GameObject itemDropPoint;

    public struct Items
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        SetMaxSatisfaction(requestTime, requestsList[listIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("DebugComplete"))
        {
            CheckRequirement(requiredObject);
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
                LevelManager.Singleton.FailRequest();
                textBubble.SetActive(false);
                imageList[listIndex].SetActive(false);
                timeSlider.maxValue = 0;
                timeSlider.value = 0;
                timeIsReducing = false;
                currentRequestGap = maxRequestGap;
                gapActive = true;
                timeSlider.gameObject.SetActive(false);
            }
        }

        if (gapActive)
        {
            currentRequestGap -= Time.deltaTime;
            if(currentRequestGap <= 0)
            {
                gapActive = false;
                //select new request
                if (listIndex < requestsList.Length - 1)
                {
                    timeSlider.gameObject.SetActive(true);
                    listIndex += 1;
                    SetMaxSatisfaction(requestTime, requestsList[listIndex]);
                    imageList[listIndex].SetActive(true);
                }
            }
        }
    }

    public void SetMaxSatisfaction(float max, ItemType obj)
    {
        textBubble.SetActive(true);
        imageList[listIndex].SetActive(true);
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
            textBubble.SetActive(false);
            afImage.SetActive(false);
            imageList[listIndex].SetActive(false);
            timeSlider.maxValue = 0;
            timeSlider.value = 0;
            timeIsReducing = false;
            currentRequestGap = maxRequestGap;
            gapActive = true;
            timeSlider.gameObject.SetActive(false);
            requiredObject = ItemType.None;
            LevelManager.Singleton.PassRequest();
        }
        else
        {
            LevelManager.Singleton.FailRequest();
        }
    }
}
