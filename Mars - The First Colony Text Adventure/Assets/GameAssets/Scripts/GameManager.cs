using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	// Setup chapters and scenes
	public Chapter chapter = Chapter.One;
	public enum Chapter { One, Two, Three };
	public Scene scene = Scene.Start;
	public enum Scene { Start };

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}

	private void Update()
	{
		switch (chapter)
		{
			case Chapter.One:
				ChapterOne();
				break;
			case Chapter.Two:
				ChapterTwo();
				break;
			case Chapter.Three:
				ChapterThree();
				break;
		}

		if (Input.anyKeyDown)
		{			
			StartCoroutine(GUIManager.instance.FirstFade());
			SaveManager.instance.Load();
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			SaveManager.instance.Save();
			Debug.Log("Current chapter: " + SaveManager.instance.state.Chapter);
		}
	}

	// Chapters
	#region
	// One
	private void ChapterOne()
	{
		switch (scene)
		{
			case Scene.Start:
				SceneStart();
				break;
		}
	}

	// Two
	private void ChapterTwo()
	{

	}

	// Three
	private void ChapterThree()
	{

	}
	#endregion

	// Scenes
	#region
	// Start
	private void SceneStart()
	{
		GUIManager.instance.storyText.text = "You wake up in bed. It is 5:00 Martian Time.";
	}
	#endregion
}
