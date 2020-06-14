using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

	public static GUIManager instance = null;
	public TextMeshProUGUI storyText;
	public Image storyImage;
	[SerializeField] private GameObject gameUI;
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject splashUI;
	private Animator anim;

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
		anim = GetComponent<Animator>();
	}

	public void ToMainMenu()
	{
		splashUI.SetActive(false);
		mainMenu.SetActive(true);
		gameUI.SetActive(false);
	}

	public void ToGame()
	{
		splashUI.SetActive(false);
		mainMenu.SetActive(false);
		gameUI.SetActive(true);
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
