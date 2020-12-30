using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	static LevelManager singleton = null;

	[SerializeField]
	float levelTime = 10f;

	[SerializeField]
	int numberOfRequests = 3;

	[SerializeField]
	GameObject gameOverCanvas = null;

	[SerializeField]
	Image timer;

	[SerializeField]
	GameObject[] strikesList;

	float currentTime;

	int failedRequests = 0;

	bool levelStarted = false;

	public static LevelManager Singleton { get => singleton; }

	void Awake()
	{
		if (singleton != null)
		{
			Destroy(singleton.gameObject);
		}

		singleton = this;
	}

	void Start()
	{
		currentTime = levelTime;
		levelStarted = true;
	}

	void Update()
	{
		currentTime -= Time.deltaTime;
		timer.fillAmount = currentTime / levelTime;
		if (currentTime <= 0)
		{
			WinLevel();
		}
	}

	public void FailRequest()
	{
		strikesList[failedRequests].SetActive(true);
		failedRequests++;
		if (failedRequests >= 3)
		{
			gameOverCanvas.GetComponentInChildren<GameOverMenuManager>().ShowGameLosePage();
			//Invoke(nameof(RestartLevel), 3.0f);
		}
	}

	public void PassRequest()
	{
		numberOfRequests--;
		if(numberOfRequests <= 0)
		{
			WinLevel();
		}
	}

	void WinLevel()
	{
		gameOverCanvas.GetComponentInChildren<GameOverMenuManager>().ShowGameWinPage();
		//Invoke(nameof(RestartLevel), 3.0f);
	}

	void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
		currentTime = levelTime;
	}
}
