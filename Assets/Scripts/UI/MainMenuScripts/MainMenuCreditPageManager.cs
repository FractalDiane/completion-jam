using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCreditPageManager : MonoBehaviour
{
    [SerializeField] private float timeInterval;
    [SerializeField] private UIMover[] nameTexts;
    [SerializeField] private UIMover[] roleTexts;
    [SerializeField] private UIMover cancelButton;

    void OnDisable()
    {
        StopAllCoroutines();
    }

    // Update is called once per frame
    public void ShowCredit()
    {
        foreach(UIMover m in nameTexts)
            m.Initialize();
        foreach(UIMover m in roleTexts)
            m.Initialize();
        cancelButton.Initialize();
        StartCoroutine(ShowCreditRoutine());
    }

    private IEnumerator ShowCreditRoutine()
    {
        if(nameTexts.Length != roleTexts.Length)
            Debug.LogError("MainMenuCreditPageManager: name Texts and role Texts size do not match.");
        for(int x = 0; x < Mathf.Min(nameTexts.Length, roleTexts.Length); ++x)
        {
            StartCoroutine(nameTexts[x].Move());
            StartCoroutine(roleTexts[x].Move());
            yield return new WaitForSeconds(timeInterval);
        }
        StartCoroutine(cancelButton.Move());
    }
}
