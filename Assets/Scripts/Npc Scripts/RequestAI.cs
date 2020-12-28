using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestAI : MonoBehaviour
{
    [SerializeField] TimeMeter timerLogic;
    // Start is called before the first frame update
    void Start()
    {
        timerLogic = gameObject.GetComponent<TimeMeter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateRequest()
    {

    }
}
