using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : UIManager
{
    [SerializeField] private GameObject pauseMenuPage;
    [SerializeField] private ConfirmUIManager confirmUIManager;
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
        Time.timeScale = 0;
        confirmUIManager.CancelAllConfirmation(false);
        AudioManager.instance.PlaySFX(AudioFileName.UIClick);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenuPage.SetActive(false);
        AudioManager.instance.PlaySFX(AudioFileName.UIClick);
    }

}
