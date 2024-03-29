using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFileManager : MonoBehaviour
{
    private static SaveFileManager instance;
    public PlayerData saveFile;

    #region Initialization
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("There is more than one instance of SaveFileManager in the scene!");
        }
        instance = this;

        saveFile = LoadPlayerData();
    }

    public static SaveFileManager GetInstance()
    {
        return instance;
    }

    #endregion

    public PlayerData LoadPlayerData()
    {
        return SaveSystem.LoadData();
    }

    public void ReloadPlayerData()
    {
        saveFile = LoadPlayerData();
    }

    public void SavePlayerData(string levelName)
    {
        SaveSystem.SaveData(levelName);
    }

    public void SavePlayerData_Combat(string levelName)
    {
        SaveSystem.SaveData_Combat(levelName);
    }

    public void SavePlayerDataForModuleTwo()
    {
        SaveSystem.SaveDataForModuleTwo(true, false, false);
    }

    public void SavePlayerDataForFreeplay()
    {
        SaveSystem.SaveDataForModuleTwo(true, true, false);
    }

    public void SavePlayerDataAsync(string levelName)
    {
        StartCoroutine(SaveGameStateAsync(levelName));
    }

    IEnumerator SaveGameStateAsync(string levelName)
    {
        yield return null;
        SavePlayerData(levelName);
    }

    public bool DoesSaveFileExists()
    {
        return SaveSystem.CheckIfSaveFileExists();
    }

}