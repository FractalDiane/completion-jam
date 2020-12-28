using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuOptionPageManager : MonoBehaviour
{
    #region Volumes
    [Header("Volume")]
    private float currentMasterVlume = 1;
    private float currentMusicVlume = 1;
    private float currentSFXVlume = 1;
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
        masterSlider.SetValueWithoutNotify(currentMasterVlume);
        musicSlider.SetValueWithoutNotify(currentMusicVlume);
        sfxSlider.SetValueWithoutNotify(currentSFXVlume);
        ChangeLanguage(currentLanguage);
    }

    //Change real variables instead of testing ones
    public void OnMasterVolumeChanged()
    {
        currentMasterVlume = masterSlider.value;
    }

    public void OnMusicVolumeChanged()
    {
        currentMusicVlume = musicSlider.value;
    }

    public void OnSFXVolumeChanged()
    {
        currentSFXVlume = sfxSlider.value;
    }
    #endregion

    #region Language
    //Todo: use Language Enum instead of int
    public void ChangeLanguage(int value)
    {
        if(currentLanguage != value)
        {
            currentLanguage = value;
            languageSelectionIcon.transform.position = languageButtons[currentLanguage].transform.position + languageSelectionIconPosDiff;
        }
    }
    #endregion
}
