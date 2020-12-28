using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class ResizeObjectOnSelect : MonoBehaviour
{
    [SerializeField] private Vector3 initSize = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 maxSize = new Vector3(1.5f, 1.5f, 1.5f);
    [SerializeField] private float scalingSpeed = 0.2f;
    private bool mouseOnObject;

    // Start is called before the first frame update
    void Start()
    {
        mouseOnObject = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = mouseOnObject?maxSize:initSize;
        Vector3 current = transform.localScale;
        if(Vector3.Distance(target, current) > 0.01f)
        {
            Vector3 newScale = Vector3.Lerp(current, target, scalingSpeed);
            transform.localScale = newScale;
        }
    }

    //needs to be called by EventTrigger component, set state when mouse enter object
    public void OnMouseEnterObject()
    {
        mouseOnObject = true;
    }

    //needs to be called by EventTrigger component, set state when mouse enter object
    public void OnMouseExitObject()
    {
        mouseOnObject = false;
    }

    //reset the state when disabled to solve issue: when going to another page and go back the button remain selected
    void OnDisable()
    {
        mouseOnObject = false;
        transform.localScale = initSize;
    }
}
