using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    SaveFileManager sfm;

    [Header("Start New Game")]
    public GameObject saveFileAlreadyExists;
    public GameObject noSaveFilePresent;

    private void Start()
    {
        sfm = SaveFileManager.GetInstance();
    }

    #region Create New Game Method and Supporting Methods
    public void CreateNewGame()
    {
        if(sfm.DoesSaveFileExists())
        {
            saveFileAlreadyExists.SetActive(true);
        } else
        {
            SetupNewGame();
        }
    }

    public void SetupNewGame()
    {
        SaveSystem.DeleteData();
        SaveSystem.CreateNewSaveFile();
        PlayerData playerData = SaveSystem.LoadData();
        AsyncManager asyncManager = AsyncManager.GetInstance();
        asyncManager.LoadLevel(playerData.currentSceneLocation);
    }

    public void CloseSaveFileAlreadyExistsWindow()
    {
        saveFileAlreadyExists.SetActive(false);
    }

    #endregion

    #region Load Game Methods
    public void LoadGame()
    {
        /*
         1. Check if game file exists. If yes, load it.
        Else, throw an error.
         */
        if (sfm.DoesSaveFileExists())
        {
            SetupExistingGame();
        }
        else
        {
            noSaveFilePresent.SetActive(true);
        }
    }

    private void SetupExistingGame()
    {
        PlayerData playerData = SaveSystem.LoadData();
        AsyncManager asyncManager = AsyncManager.GetInstance();
        asyncManager.LoadLevel(playerData.currentSceneLocation);
    }

    public void CloseNoSaveFilePresentWindow()
    {
        noSaveFilePresent.SetActive(false);
    }
    #endregion

    #region Exit Methods
    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion
}
