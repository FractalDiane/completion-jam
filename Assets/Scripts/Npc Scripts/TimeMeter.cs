using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Type1 };

public class TimeMeter : MonoBehaviour
{
    [SerializeField] float maxTime = 0;
    [SerializeField] float currentTime = 0;
    [SerializeField] bool timeIsReducing = false;
    [SerializeField] ItemType requiredObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeIsReducing && currentTime <= 0)
        {
            timeIsReducing = false;
            // call timer fail
        }
        else if(timeIsReducing)
        {
            currentTime -= Time.deltaTime;
        }

    }

    public void SetMaxSatisfaction(float max, ItemType obj)
    {
        maxTime = max;
        currentTime = maxTime;
        timeIsReducing = true;
        requiredObject = obj;
    }

    public void CheckRequirement(ItemType obj)
    {
        if(requiredObject == obj)
        {
            maxTime = 0;
            currentTime = 0;
            timeIsReducing = false;
        }
        else
        {
            //failure
        }
    }
}
