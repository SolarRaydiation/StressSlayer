using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public const string SAVEFILE_PATH = "/savedata.bin";
    public const string SOUNDSETTINGS_PATH = "/soundsettings.bin";

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

    public static void SaveDataForModuleTwo(bool isModuleOneComplete, bool isModuleTwoComplete, bool isModuleThreeComplete)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SAVEFILE_PATH;
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveFileManager sfm = SaveFileManager.GetInstance();

        PlayerData data = new PlayerData(sfm.saveFile, isModuleOneComplete, isModuleTwoComplete, isModuleThreeComplete);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadData()
    {
        Debug.Log("Loading save file...");
        string path = Application.persistentDataPath + SAVEFILE_PATH;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Debug.Log("Savefile found in " + path);
            PlayerData data = (PlayerData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        else
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


    #region Sound Settings

    public static SoundSettings CreateNewSoundSettings()
    {
        Debug.Log("Creating sound settings...");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SOUNDSETTINGS_PATH;
        FileStream stream = new FileStream(path, FileMode.Create);

        SoundSettings data = new SoundSettings();
        formatter.Serialize(stream, data);
        stream.Close();
        return data;
    }

    public static SoundSettings LoadSoundSettings()
    {
        Debug.Log("Loading sound settings...");
        string path = Application.persistentDataPath + SOUNDSETTINGS_PATH;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Debug.Log("SoundSettings found in " + path);
            SoundSettings data = (SoundSettings) formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log($"Sound settings not found in {path}. Using default SoundSettings instead.");
            return new SoundSettings();
        }
    }

    public static void SaveSoundSettings()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SOUNDSETTINGS_PATH;
        FileStream stream = new FileStream(path, FileMode.Create);

        SoundSettings data = new SoundSettings(
            AudioManager.instance.currentBGMVolume,
            AudioManager.instance.currentSFXVolume,
            AudioManager.instance.isBGMMuted,
            AudioManager.instance.isSFXMuted
        );
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static bool CheckIfSoundSettingsExist()
    {
        string path = Application.persistentDataPath + SOUNDSETTINGS_PATH;
        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void DeleteSoundSettings()
    {
        Debug.Log("Deleting sound settings file...");
        string path = Application.persistentDataPath + SOUNDSETTINGS_PATH;
        File.Delete(path);
    }

    #endregion
}
