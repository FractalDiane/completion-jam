using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : UIManager
{
    [SerializeField] private GameObject mainPage;
    [SerializeField] private GameObject optionPage;
    [SerializeField] private GameObject creditPage;

    [SerializeField] private MainMenuOptionPageManager optionPageManager;
    public void StartGame()
    {

    }

    public void ShowOptionPage()
    {
        mainPage.SetActive(false);
        optionPage.SetActive(true);
        optionPageManager.Initialize();
    }

    public void ShowCreditPage()
    {
        mainPage.SetActive(false);
        creditPage.SetActive(true);
    }

    public void BackToMainPage()
    {
        mainPage.SetActive(true);
        optionPage.SetActive(false);
        creditPage.SetActive(false);
    }
}
