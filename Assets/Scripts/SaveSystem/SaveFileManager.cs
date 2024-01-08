using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFileManager : MonoBehaviour
{
    private static SaveFileManager instance;
    public PlayerData saveFile;

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

    public void SavePlayerData()
    {
        SaveSystem.SaveData();
    }

    public void SavePlayerDataAsync()
    {
        StartCoroutine(SaveGameStateAsync());
    }

    IEnumerator SaveGameStateAsync()
    {
        yield return null;
        SavePlayerData();
    }

    private PlayerData LoadPlayerData()
    {
        return SaveSystem.LoadData();
    }
}
