using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance = null;

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

    [Header("Logic")]
    [SerializeField] private string saveFileName = "mars.sav";
    [SerializeField] private bool loadOnStart = false;
    [HideInInspector] public SaveState state;
    private BinaryFormatter formatter;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the formatter, make this script persist
        formatter = new BinaryFormatter();
        DontDestroyOnLoad(this.gameObject);

        if (loadOnStart)
        {
            Load();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        // If there's no previous state loaded, create new one
        if (state == null)
        {
            state = new SaveState();
            Debug.Log("No save file found, creating new save file...");
        }

        // Set the time at which we've tried saving
        state.Chapter = GameManager.instance.chapter;
        state.LastSaveTime = DateTime.Now;

        var file = new FileStream(saveFileName, FileMode.Create, FileAccess.Write);
        formatter.Serialize(file, state);
        file.Close();
        Debug.Log("Game saved...");
    }

    public void Load()
    {
        // Open a physical file, on your disk to hold the save
        try
        {
            var file = new FileStream(saveFileName, FileMode.Open, FileAccess.Read);
            // If we found the file, open and read it
            state = (SaveState)formatter.Deserialize(file);
            file.Close();
            Debug.Log("Game loaded...");
        }
        catch
        {
            Debug.Log("No save file found, creating new save file...");
            Save();
        }
    }
}
