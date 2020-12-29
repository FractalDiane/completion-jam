using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuManager : UIManager
{
    [SerializeField] private GameObject winPage;
    [SerializeField] private GameObject losePage;
    public void Retry()
    {
        Debug.Log("Retry yeah");
    }

    public void ShowGameLosePage()
    {
        losePage.SetActive(true);
        winPage.SetActive(false);
    }

    public void ShowGameWinPage()
    {
        losePage.SetActive(false);
        winPage.SetActive(true);
    }
}
