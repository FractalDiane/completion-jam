using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	static LevelManager singleton = null;

	[SerializeField]
	float levelTime = 10f;

	[SerializeField]
	int numberOfRequests = 3;
	
	float currentTime;

	int failedRequests = 0;

	bool levelStarted = false;

	public static LevelManager Singleton { get => singleton; }

	void Awake()
	{
		if (singleton != null)
		{
			Destroy(singleton.gameObject);
			singleton = this;
		}

		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		currentTime = levelTime;
		levelStarted = true;
	}

	void Update()
	{
		currentTime -= Time.deltaTime;
		if (currentTime <= 0)
		{
			WinLevel();
		}
	}

	void WinLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
		currentTime = levelTime;
	}
}