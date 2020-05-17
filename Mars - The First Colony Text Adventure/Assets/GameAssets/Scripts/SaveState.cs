using System;

[System.Serializable]
public class SaveState
{
	public GameManager.Chapter Chapter { set; get; }
	public DateTime LastSaveTime { set; get; }
}
