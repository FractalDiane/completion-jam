using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuOptionPageManager : MonoBehaviour
{
    #region Volumes
    [Header("Volume")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Language")]
    private int currentLanguage = 0;
    [SerializeField] private GameObject[] languageButtons;
    [SerializeField] private GameObject languageSelectionIcon;
    [SerializeField] private Vector3 languageSelectionIconPosDiff;

    //set initialze values of all UI elements on option page
    public void Initialize()
    {
        masterSlider.SetValueWithoutNotify(AudioManager.instance.GetMasterVolume());
        musicSlider.SetValueWithoutNotify(AudioManager.instance.GetMusicVolume());
        sfxSlider.SetValueWithoutNotify(AudioManager.instance.GetSFXVolume());
        ChangeLanguage(currentLanguage);
    }

    //Change real variables instead of testing ones
    public void OnMasterVolumeChanged()
    {
        AudioManager.instance.SetMasterVolume(masterSlider.value);
    }

    public void OnMusicVolumeChanged()
    {
        AudioManager.instance.SetMusicVolume(musicSlider.value);
    }

    public void OnSFXVolumeChanged()
    {
        AudioManager.instance.SetSFXVolume(sfxSlider.value);
    }
    #endregion

    #region Language
    //Todo: use Language Enum instead of int
    public void ChangeLanguage(int value)
    {
        AudioManager.instance.PlaySFX(AudioFileName.UIClick);
        if(currentLanguage != value)
        {
            currentLanguage = value;
            languageSelectionIcon.transform.position = languageButtons[currentLanguage].transform.position + languageSelectionIconPosDiff;
        }
    }
    #endregion
}
