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
    [SerializeField] private AudioFile[] audioFiles;
    private Dictionary<AudioFileName, AudioClip> audioLibrary;

    [SerializeField] AudioClip music;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            InitializeLibrary();
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusic(music);
    }

    //==========================================================================================
    //Play Music
    //==========================================================================================
    public void InitializeLibrary()
    {
        audioLibrary = new Dictionary<AudioFileName, AudioClip>();
        foreach(AudioFile audioFile in audioFiles)
        {  
            audioLibrary.Add(audioFile.name, audioFile.clip);
        }
    }

    public void PlaySFX(AudioClip clip, float volumeScale = 1)
    {
        sfxAudioSource.PlayOneShot(clip, volumeScale);
    }

    public void PlaySFX(AudioFileName clipName, float volumeScale = 1)
    {
        if(audioLibrary.ContainsKey(clipName))
            PlaySFX(audioLibrary[clipName], volumeScale);
        else
            Debug.LogError("AudioManager.PlaySFX(): " + clipName + " does not exist in the audio library.");
    }

    public void PlayMusic(AudioClip clip, bool loop = true, float volumeScale = 1)
    {
        musicAudioSource.clip = clip;
        musicAudioSource.loop = loop;
        musicAudioSource.volume = volumeScale;
        musicAudioSource.Play();
    }

    public void PlayMusic(AudioFileName clipName, bool loop = true, float volumeScale = 1)
    {
        if(audioLibrary.ContainsKey(clipName))
            PlayMusic(audioLibrary[clipName], loop, volumeScale);
        else
            Debug.LogError("AudioManager.PlaySFX(): " + clipName + " does not exist in the audio library.");
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

public enum AudioFileName
{
    UIHover,
    UIClick
}

[System.Serializable]
public class AudioFile
{
    public AudioFileName name;
    public AudioClip clip;
}
