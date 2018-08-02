using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

	public static GUIManager instance = null;

	public GameObject _mainMenu;
	public GameObject _story;
	public GameObject _storyBg;
	public TextMeshProUGUI _storyText;

	[Space]
	public GameObject _chapterOne;
	public GameObject _chapterTwo;

	private Animator anim;

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
		anim = GetComponent<Animator>();

		// Reset scenes to default start
		_mainMenu.SetActive(true);
		_story.SetActive(false);
		_storyBg.SetActive(false);
		_chapterOne.SetActive(false);
		_chapterTwo.SetActive(false);
	}

	private void Update()
	{
		if (Input.anyKey)
		{
			StartCoroutine(FirstFade());
		}
	}

	private IEnumerator FirstFade()
	{
		anim.Play("GUIFade");

		yield return new WaitForSeconds(1.5f); // Half of the total length of fade clip. If clip length is change, this value must change too.

		_mainMenu.SetActive(false);
		_story.SetActive(true);
		_storyBg.SetActive(true);
		_chapterOne.SetActive(true);
	}

	private IEnumerator Fade(GameObject from, GameObject to)
	{
		anim.Play("GUIFade");

		yield return new WaitForSeconds(1.5f); // Half of the total length of fade clip. If clip length is change, this value must change too.

		from.SetActive(false);
		to.SetActive(true);
	}
}
