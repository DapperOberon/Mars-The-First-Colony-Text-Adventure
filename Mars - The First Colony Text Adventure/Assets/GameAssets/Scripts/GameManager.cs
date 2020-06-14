using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	[SerializeField] State menuState;
	[SerializeField] State gameState;
	State state;
	State[] nextStates;
	public Coroutine fadeCoroutine;

	void Awake()
	{
		//Debug.Log(GetType().Name + " - Awoken. Initializing Singleton pattern. instance Id : " + gameObject.GetInstanceID());

		if (instance == null)
		{
			//Debug.Log(GetType().Name + " - Setting first instance. instance Id : " + gameObject.GetInstanceID());

			//if not, set instance to this
			instance = this;

			//Sets this to not be destroyed when reloading scene
			DontDestroyOnLoad(gameObject);
		}
		else if (instance != this)
		{
			//Debug.LogWarning(GetType().Name + " - Destroying secondary instance. instance Id : " + gameObject.GetInstanceID());

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GlobalManager.
			DestroyImmediate(gameObject);

			return;
		}

	}

	private void Start()
	{
		state = menuState;
		AudioManager.instance.PlayMusic(state.GetMusic());

		if (SceneManager.GetActiveScene().name == "Splash")
		{
			//Debug.Log(SceneManager.GetActiveScene().name);
			StartCoroutine(ToMainMenu());
		}
		else if(SceneManager.GetActiveScene().name == "Game")
		{
			state = gameState;
			GUIManager.instance.storyText.text = state.GetStateStory();
			GUIManager.instance.storyImage.sprite = state.GetStateImage();
			if (state.GetMusic() != null)
			{
				AudioManager.instance.PlayMusic(state.GetMusic());
			}
		}
	}

	private void Update()
	{
		if (SceneManager.GetActiveScene().name == "MainMenu")
		{
			//Debug.Log(SceneManager.GetActiveScene().name);
			if (Input.anyKeyDown)
			{
				StartCoroutine(ToGame());
			}
		}
		else if (SceneManager.GetActiveScene().name == "Game")
		{
			//Debug.Log(SceneManager.GetActiveScene().name);
			ManageState();
		}
	}

	private IEnumerator ToMainMenu()
	{
		yield return new WaitForSeconds(6f);

		GUIManager.instance.ToMainMenu();

		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}

	private IEnumerator ToGame()
	{
		yield return StartCoroutine(GUIManager.instance.Fade());
		GUIManager.instance.ToGame();

		state = gameState;
		GUIManager.instance.storyImage.sprite = state.GetStateImage();
		GUIManager.instance.storyText.text = state.GetStateStory();
		if (state.GetMusic() != null)
		{
			AudioManager.instance.PlayMusic(state.GetMusic());
		}

		SceneManager.LoadScene("Game", LoadSceneMode.Single);
	}

	private void ManageState()
	{
		//Debug.Log(state.name);
		nextStates = state.GetNextStates();

		for (int i = 0; i < nextStates.Length; i++)
		{
			if (Input.GetKeyDown(KeyCode.Alpha1 + i))
			{
				//yield return StartCoroutine(SetStoryText(i));
				if (fadeCoroutine == null)
				{
					fadeCoroutine = StartCoroutine(SetStateContent(i));
				}

			}
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			state = gameState;
			GUIManager.instance.storyText.text = state.GetStateStory();
			GUIManager.instance.storyImage.sprite = state.GetStateImage();
			if(state.GetMusic() != null)
			{
				AudioManager.instance.PlayMusic(state.GetMusic());
			}
		}
		//GUIManager.instance.storyText.text = state.GetStateStory();
	}

	private IEnumerator SetStateContent(int i)
	{
		state = nextStates[i];
		if (state.GetMusic() != null)
		{
			AudioManager.instance.FadeMusicOut(1.5f);
		}
		yield return StartCoroutine(GUIManager.instance.Fade());
		GUIManager.instance.storyImage.sprite = state.GetStateImage();
		GUIManager.instance.storyText.text = state.GetStateStory();
		if(state.GetMusic() != null)
		{
			AudioManager.instance.PlayMusic(state.GetMusic());
			AudioManager.instance.FadeMusicIn(3f);
		}
		yield return new WaitForSeconds(1.5f);
		fadeCoroutine = null;
	}
}
