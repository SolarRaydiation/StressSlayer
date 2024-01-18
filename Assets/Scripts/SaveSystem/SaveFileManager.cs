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
        if(instance != null)
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

    #region Methods

    private PlayerData LoadPlayerData()
    {
        return SaveSystem.LoadData();
    }

    public void SavePlayerData(string levelName)
    {
        SaveSystem.SaveData(levelName);
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
        Debug.Log(SaveSystem.CheckIfSaveFileExists());
        return SaveSystem.CheckIfSaveFileExists();
    }

    #endregion
}
