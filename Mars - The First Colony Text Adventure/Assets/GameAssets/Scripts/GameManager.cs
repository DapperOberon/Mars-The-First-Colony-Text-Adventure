using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	[SerializeField] State menuState;
	[SerializeField] State gameState;
	State state;

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
	}

	private void Update()
	{
		if (SceneManager.GetActiveScene().name == "MainMenu")
		{
			//Debug.Log(SceneManager.GetActiveScene().name);
			if (Input.anyKeyDown)
			{
				ToGame();
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
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}

	private void ToGame()
	{
		StartCoroutine(GUIManager.instance.Fade());
		state = gameState;
		SceneManager.LoadScene("Game", LoadSceneMode.Single);
	}

	private void ManageState()
	{
		var nextStates = state.GetNextStates();

		for (int i = 0; i < nextStates.Length; i++)
		{
			if (Input.GetKeyDown(KeyCode.Alpha1 + i))
			{
				state = nextStates[i];
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			state = gameState;
		}

		GUIManager.instance.storyText.text = state.GetStateStory();
	}
}
