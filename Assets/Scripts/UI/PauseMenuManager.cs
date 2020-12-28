using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPage;
    [SerializeField] private GameObject mainMenuConfimPage;
    [SerializeField] private GameObject exitConfirmPage;

    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            if(pauseMenuPage.activeSelf)
                Resume();
            else
                Pause();
        }
    }

    void Pause()
    {
        pauseMenuPage.SetActive(true);
        mainMenuConfimPage.SetActive(false);
        exitConfirmPage.SetActive(false);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenuPage.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CancelConfirmation()
    {
        mainMenuConfimPage.SetActive(false);
        exitConfirmPage.SetActive(false);
    }

    public void ShowMainMenuConfirmation()
    {
        mainMenuConfimPage.SetActive(true);
    }

    public void ShowExitConfirmation()
    {
        exitConfirmPage.SetActive(true);
    }
}
