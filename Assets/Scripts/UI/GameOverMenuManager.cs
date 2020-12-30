using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuManager : UIManager
{
    [SerializeField] private GameObject losePage;
    [SerializeField] private GameObject winPage;
    [SerializeField] private bool hasNextLevel = true;
    [SerializeField] private string nextLevelSceneName;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private GameObject mainMenuButton;
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
        if(hasNextLevel)
        {
            nextLevelButton.SetActive(true);
            mainMenuButton.SetActive(false);
        }else{
            nextLevelButton.SetActive(false);
            mainMenuButton.SetActive(true);
        }
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(nextLevelSceneName);
        Time.timeScale = 1;
    }
}
