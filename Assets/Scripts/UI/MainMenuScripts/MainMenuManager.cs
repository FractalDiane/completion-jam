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
        AudioManager.instance.PlaySFX(AudioFileName.UIClick);
    }

    public void ShowOptionPage()
    {
        mainPage.SetActive(false);
        optionPage.SetActive(true);
        optionPageManager.Initialize();
        AudioManager.instance.PlaySFX(AudioFileName.UIClick);
    }

    public void ShowCreditPage()
    {
        mainPage.SetActive(false);
        creditPage.SetActive(true);
        AudioManager.instance.PlaySFX(AudioFileName.UIClick);
    }

    public void BackToMainPage()
    {
        mainPage.SetActive(true);
        optionPage.SetActive(false);
        creditPage.SetActive(false);
        AudioManager.instance.PlaySFX(AudioFileName.UIClick);
    }
}
