using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public const string SAVEFILE_PATH = "/savedata.bin";
    public static void SaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SAVEFILE_PATH;
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(
            PlayerMovement.GetInstance(),
            PlayerStatsController.GetInstance(),
            ClockManager.GetInstance(),
            NextLevelPreInitializer.GetInstance(),
            PlayerInventoryManager.GetInstance()
            );

        Debug.Log("We have reached this point in the program.");
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadData()
    {
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

    private static PlayerData CreateNewSaveFile()
    {
        Debug.Log("Creating new save file...");

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SAVEFILE_PATH;
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();
        formatter.Serialize(stream, data);
        Debug.Log("Savefile created and stored to " + path);
        stream.Close();
        return data;
    }
}
