using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIManager : MonoBehaviour {

	public static GUIManager instance = null;

	public GameObject _mainMenu;
	public GameObject _chapterOne;

	public TextMeshProUGUI _storyText;

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		// Reset scenes to default start
		_mainMenu.SetActive(true);
		_chapterOne.SetActive(false);
	}

	private void Update()
	{
		if (Input.anyKey)
		{
			_mainMenu.SetActive(false);
			_chapterOne.SetActive(true);
		}
	}
}
