using System.Collections;
using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour {

	public static GUIManager Instance = null;

	//public GameObject mainMenu;
	//public GameObject story;
	//public GameObject storyBg;
	public TextMeshProUGUI storyText;

	//[Space]
	//public GameObject chapterOne;
	//public GameObject chapterTwo;

	private Animator anim;

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		anim = GetComponent<Animator>();

		//// Reset scenes to default start
		//mainMenu.SetActive(true);
		//story.SetActive(false);
		//storyBg.SetActive(false);
		//chapterOne.SetActive(false);
		//chapterTwo.SetActive(false);
	}

	public IEnumerator FirstFade()
	{
		anim.Play("GUIFade");

		yield return new WaitForSeconds(1.5f); // Half of the total length of fade clip. If clip length is change, this value must change too.

		//mainMenu.SetActive(false);
		//.SetActive(true);
		//storyBg.SetActive(true);
		//chapterOne.SetActive(true);
	}

	public IEnumerator Fade()
	{
		anim.Play("GUIFade");

		yield return new WaitForSeconds(1.5f); // Half of the total length of fade clip. If clip length is change, this value must change too.
	}

	private IEnumerator FadeTo(GameObject from, GameObject to)
	{
		anim.Play("GUIFade");

		yield return new WaitForSeconds(1.5f); // Half of the total length of fade clip. If clip length is change, this value must change too.

		from.SetActive(false);
		to.SetActive(true);
	}
}
