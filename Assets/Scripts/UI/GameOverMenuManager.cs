using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuManager : UIManager
{
    [SerializeField] private GameObject winPage;
    [SerializeField] private GameObject losePage;
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        AudioManager.instance.PlaySFX(AudioFileName.UIClick);
    }

    public void ShowGameLosePage()
    {
        Time.timeScale = 0;
        losePage.SetActive(true);
        winPage.SetActive(false);
    }

    public void ShowGameWinPage()
    {
        Time.timeScale = 0;
        losePage.SetActive(false);
        winPage.SetActive(true);
    }
}
