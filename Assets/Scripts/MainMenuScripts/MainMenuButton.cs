using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField] private Vector3 initSize = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 maxSize = new Vector3(1.5f, 1.5f, 1.5f);
    [SerializeField] private float scalingSpeed = 0.2f;
    private bool mouseOnButton;

    // Start is called before the first frame update
    void Start()
    {
        mouseOnButton = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = mouseOnButton?maxSize:initSize;
        Vector3 current = transform.localScale;
        if(Vector3.Distance(target, current) > 0.01f)
        {
            Vector3 newScale = Vector3.Lerp(current, target, scalingSpeed);
            Debug.Log(newScale);
            transform.localScale = newScale;
        }
    }

    public void OnMouseEnterButton()
    {
        mouseOnButton = true;
        Debug.Log("yeah");
    }

    public void OnMouseExitButton()
    {
        mouseOnButton = false;
    }
}
