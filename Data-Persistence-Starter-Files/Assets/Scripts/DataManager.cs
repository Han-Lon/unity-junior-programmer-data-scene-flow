using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// For managing persistent data between scenes and sessions
public class DataManager : MonoBehaviour
{
    // Create MainManager as a static variable so it can be accessed across other scripts
    // One interesting note is if we had 10 other MainManager scripts and they all modified this var in some way, they'd all be modifying the same var
    public static DataManager Instance;

    public string currentPlayerName;
    public string highScorePlayerName;
    public int highScore;

    private void Awake()
    {
        // If we already have an instance of MainManager, destroy this one and return
        // This would happen if we kept going back and forth from the game scene to the main menu
        // This is a singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Create a new instance of MainManager and set it to be persisted across scenes
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Attempt to load previous save state
        LoadGame();
    }

    // Best practice to use separate sub-classes for saving and loading data
    [System.Serializable]
    public class SaveData
    {
        public string playerName;
        public int highScore;
    }

    // Save game by committing the current name of the highest scoring player and their score to a save file
    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.playerName = highScorePlayerName;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    // Load game from save file (if it exists)
    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScorePlayerName = data.playerName;
            highScore = data.highScore;
        }
    }
}
