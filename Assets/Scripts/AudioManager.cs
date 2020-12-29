using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; 
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    private float masterVolume = 1;
    private float musicVolume = 1;
    private float sfxVolume = 1;
    public AudioClip test;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    //==========================================================================================
    //Play Music
    //==========================================================================================
    public void PlaySFX(AudioClip clip, float volumeScale = 1)
    {
        sfxAudioSource.PlayOneShot(clip, volumeScale);
    }

    public void PlayMusic(AudioClip clip, bool loop = true, float volumeScale = 1)
    {
        musicAudioSource.clip = clip;
        musicAudioSource.loop = loop;
        musicAudioSource.volume = volumeScale;
        musicAudioSource.Play();
    }

    //==========================================================================================
    //Change/Get Mixer Values
    //==========================================================================================
    public float GetMasterVolume()
    {
        return masterVolume;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = value;
        mixer.SetFloat("MasterVolume",  Mathf.Log10(value) * 20);
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        mixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        mixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
    }
}
