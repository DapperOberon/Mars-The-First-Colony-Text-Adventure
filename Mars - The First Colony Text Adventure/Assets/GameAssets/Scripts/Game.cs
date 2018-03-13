using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	// Setup chapters and scenes
	public Chapter chapter = Chapter.One;
	public enum Chapter { One, Two, Three };
	public Scene scene = Scene.Start;
	public enum Scene { Start };

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
		GUIManager.instance._storyText.text = "You wake up in bed. It is 5:00 Martian Time.";
	}
	#endregion
}
