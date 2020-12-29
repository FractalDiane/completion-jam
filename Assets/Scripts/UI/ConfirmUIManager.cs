using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] confimPages;


    public void CancelAllConfirmation(bool playSound = true)
    {
        if(playSound)
            AudioManager.instance.PlaySFX(AudioFileName.UIClick);
        foreach(GameObject p in confimPages)
            p.SetActive(false);
    }

    public void ShowConfirmation(int index)
    {
        confimPages[index].SetActive(true);
        AudioManager.instance.PlaySFX(AudioFileName.UIClick);
    }
}
