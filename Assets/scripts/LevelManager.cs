using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


//save and load management for saving the status of the game, respectively the status of items in individual levels
//the state is stored in binary files after each end of the level
[System.Serializable]
public class LevelManager : MonoBehaviour
{

    public static Dictionary<int,PlayerData> levelData;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        levelData = SaveSystem.LoadData();
        Debug.Log(levelData);
        if (levelData == null)
        {
            levelData = new Dictionary<int, PlayerData>();
            levelData.Add(SceneManager.GetActiveScene().buildIndex, new PlayerData(0,0,0));
            Debug.Log("new dict");
        }
    }

    public static void SaveLevelData (PlayerData playerData)
    {

        if (levelData.ContainsKey(SceneManager.GetActiveScene().buildIndex + 1))
        {
            levelData[SceneManager.GetActiveScene().buildIndex + 1] = playerData;

        } else
        {
            levelData.Add(SceneManager.GetActiveScene().buildIndex + 1, playerData);
        }

        SaveSystem.SaveData(levelData);
    }

    public static PlayerData GetLevelData ()
    {
        return levelData[SceneManager.GetActiveScene().buildIndex];
    }
}
//a class that groups a player's stored data
[System.Serializable]
public class PlayerData
{
    public int batteries;
    public int speedPowerUps;
    public int eys;

    public PlayerData (int Levelbatteries, int LevelspeedPowerUps, int Leveleys)
    {
    batteries = Levelbatteries;
    speedPowerUps = LevelspeedPowerUps;
    eys = Leveleys;
    }
}

public static class SaveSystem {

    public static void SaveData (Dictionary<int, PlayerData> dict)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string file = Application.persistentDataPath + "levelData.fun";
        FileStream stream = new FileStream(file, FileMode.Create);

        formatter.Serialize(stream, dict);
        stream.Close();
    }

    public static Dictionary<int, PlayerData> LoadData ()
    {
        string file = Application.persistentDataPath + "levelData.fun";
        if (File.Exists(file))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(file, FileMode.Open);

            Dictionary<int, PlayerData> data = formatter.Deserialize(stream) as Dictionary<int, PlayerData>;

            stream.Close();
            Debug.Log("Data loaded from file");
            Debug.Log(data);

            return data;
        }else
        {
            Debug.LogError("FIle not found");
            return null;
        }
    }
}

