using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSpecificScene : MonoBehaviour
{
    [SerializeField] private string nextScene;
    public void LoadAScene()
    {
        AudioManager.instance.PlaySFX(AudioFileName.UIClick);
		SceneManager.LoadScene(nextScene);
    }
}
