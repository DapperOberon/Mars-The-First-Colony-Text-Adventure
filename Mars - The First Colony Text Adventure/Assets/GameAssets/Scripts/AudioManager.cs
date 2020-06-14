using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	// Audio players components.
	public AudioMixer mixer;
	public AudioSource musicSource;

	// Random pitch adjustment range.
	public float LowPitchRange = .95f;
	public float HighPitchRange = 1.05f;

	// Singleton instance.
	public static AudioManager instance = null;

	// Initialize the singleton instance.
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

	//// Play a single clip through the sound effects source.
	//public void Play(AudioClip clip)
	//{
	//	EffectsSource.clip = clip;
	//	EffectsSource.Play();
	//}

	// Play a single clip through the music source.
	public void PlayMusic(AudioClip clip)
	{
		musicSource.clip = clip;
		musicSource.Play();
	}

	public AudioMixer GetMixer()
	{
		return mixer;
	}

	public void FadeMusicOut(float fadeTime)
	{
		StartCoroutine(FadeMixerGroup.StartFade(mixer, "MusicVolume", fadeTime, -1f));
	}

	public void FadeMusicIn(float fadeTime)
	{
		StartCoroutine(FadeMixerGroup.StartFade(mixer, "MusicVolume", fadeTime, 1f));
	}


	//	// Play a random clip from an array, and randomize the pitch slightly.
	//	public void RandomSoundEffect(params AudioClip[] clips)
	//	{
	//		int randomIndex = Random.Range(0, clips.Length);
	//		float randomPitch = Random.Range(LowPitchRange, HighPitchRange);

	//		EffectsSource.pitch = randomPitch;
	//		EffectsSource.clip = clips[randomIndex];
	//		EffectsSource.Play();
	//	}
}
