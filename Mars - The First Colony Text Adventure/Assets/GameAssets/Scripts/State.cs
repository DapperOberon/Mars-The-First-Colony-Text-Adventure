using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject
{
    [SerializeField] Sprite storyImage;
    [TextArea(10,14)] [SerializeField] string storyText;

    [SerializeField] State[] nextStates;
    [SerializeField] AudioClip[] oneShots;
    [SerializeField] AudioClip[] ambientSounds;
    [SerializeField] AudioClip music;

    public string GetStateStory()
    {
        return storyText;
    }

    public Sprite GetStateImage()
	{
        return storyImage;
	}

    public State[] GetNextStates()
    {
        return nextStates;
    }

    public AudioClip[] GetOneShots()
	{
        return oneShots;
	}

    public AudioClip[] GetAmbientSounds()
	{
        return ambientSounds;
	}

    public AudioClip GetMusic()
	{
        return music;
	}
}
