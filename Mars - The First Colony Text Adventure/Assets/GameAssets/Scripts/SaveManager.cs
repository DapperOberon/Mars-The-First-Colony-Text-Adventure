//using System;
//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
//using UnityEngine;

//[Serializable]
//public class SaveManager : MonoBehaviour
//{
//    public static SaveManager Instance = null;
//    private  const string SAVE_SEPERATOR = "#SAVE-VALUE#";
//    private string SAVE_FOLDER;

//    [Header("Logic")]
//    [SerializeField] private string saveFileName = "mars";
//    [SerializeField] private bool loadOnStart = false;
//    [SerializeField] private bool useBinaryFormat = false;
//    [SerializeField] private string binaryFileExtension = ".save";
//    [SerializeField] public SaveState state;
//    private BinaryFormatter formatter;

//    public void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//        }
//        else if (Instance != this)
//        {
//            Destroy(gameObject);
//        }

//        SAVE_FOLDER = Application.dataPath + "/Saves/";

//        // Setup the save folder
//        if (!Directory.Exists(SAVE_FOLDER))
//        {
//            Directory.CreateDirectory(SAVE_FOLDER);
//        }
//    }

//    // Start is called before the first frame update
//    public void Start()
//    {
//        // Initialize the formatter, make this script persist
//        formatter = new BinaryFormatter();

//        DontDestroyOnLoad(this.gameObject);

//        if (loadOnStart)
//        {
//            Load();
//        }
//    }

//    public void Save()
//    {

//        //If there's no previous state loaded, create new one
//        if (state == null)
//        {
//            state = new SaveState();
//            Debug.Log("No save file found, creating new save file...");
//        }

//        // Set the time at which we've tried saving
//        //state = new SaveState();

//        state.Chapter = GameManager.Instance.chapter;
//        state.LastSaveTime = DateTime.Now;

//        // Using binary format
//        if (useBinaryFormat)
//        {
//            var file = new FileStream(SAVE_FOLDER + saveFileName + binaryFileExtension, FileMode.Create, FileAccess.Write);
//            formatter.Serialize(file, state);
//            file.Close();
//        }
//        else
//        {
//            string json = JsonUtility.ToJson(state);
//            Debug.Log(json);
//            File.WriteAllText(SAVE_FOLDER + saveFileName + ".txt", json);
//        } 
        
//        Debug.Log("Game saved...");
//    }

//    public void Load()
//    {
//        formatter = new BinaryFormatter();
//        // Open a physical file, on your disk to hold the save
//        if (useBinaryFormat)
//        {
//            if(File.Exists(SAVE_FOLDER + saveFileName + binaryFileExtension))
//            {
//                var file = new FileStream(SAVE_FOLDER + saveFileName + binaryFileExtension, FileMode.Open, FileAccess.Read);
//                // If we found the file, open and read it
//                SaveState state = (SaveState)formatter.Deserialize(file);
//                file.Close();
//                Debug.Log("Game loaded...");
//            }
//            else
//            {
//                Debug.Log("No save file found, creating new save file...");
//                Save();
//            }
//        }
//        else
//        {
//            if(File.Exists(SAVE_FOLDER + saveFileName + ".txt"))
//            {
//                string json = File.ReadAllText(SAVE_FOLDER + saveFileName + ".txt");
//                SaveState state = JsonUtility.FromJson<SaveState>(json);
//                Debug.Log("Game loaded...");
//            }
//            else
//            {
//                Debug.Log("No save file found, creating new save file...");
//                Save();
//            }
//        }
            
//    }
//}
