using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public const string SAVEFILE_PATH = "/savedata.bin";

    public static bool CheckIfSaveFileExists()
    {
        string path = Application.persistentDataPath + SAVEFILE_PATH;
        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void SaveData(string nextSceneName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SAVEFILE_PATH;
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(
            PlayerMovement.GetInstance(),
            PlayerStatsController.GetInstance(),
            ClockManager.GetInstance(),
            NextLevelPreInitializer.GetInstance(),
            PlayerInventoryManager.GetInstance(),
            nextSceneName
            );
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveData_Combat(string nextSceneName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SAVEFILE_PATH;
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(
            PlayerMovement.GetInstance(),
            PlayerStatsScript.GetInstance(),
            StressManager.GetInstance(),
            ClockManager.GetInstance(),
            GameboostManager.GetInstance(),
            nextSceneName
            );
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadData()
    {
        Debug.Log("Loading save file...");
        string path = Application.persistentDataPath + SAVEFILE_PATH;
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Debug.Log("Savefile found in " + path);
            PlayerData data = (PlayerData) formatter.Deserialize(stream);
            stream.Close();
            return data;
        } else
        {
            Debug.Log($"Savefile not found in {path}. Using default data instead.");
            return new PlayerData();
        }
    }

    public static PlayerData CreateNewSaveFile()
    {
        Debug.Log("Creating new save file...");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SAVEFILE_PATH;
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();
        formatter.Serialize(stream, data);
        stream.Close();
        return data;
    }

    public static void DeleteData()
    {
        Debug.Log("Deleting save file...");
        string path = Application.persistentDataPath + SAVEFILE_PATH;
        File.Delete(path);
    }
}
