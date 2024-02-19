using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    SaveFileManager sfm;

    [Header("Main Menu Screens")]
    public GameObject saveFileAlreadyExists;
    public GameObject gameLockScreen;
    public GameObject noSaveFilePresent;
    public GameObject modulesListPanel;

    [Header("Module List Buttons")]
    public Button moduleOne;
    public Button moduleTwo;
    public Button freePlay;

    [Header("Module Details Object References")]
    public GameObject moduleDetailsPanel;
    private CanvasGroup mddCanvasGroup;
    public TextMeshProUGUI moduleName;
    public TextMeshProUGUI moduleDetails;
    public Button playButton;

    private void Start()
    {
        sfm = SaveFileManager.GetInstance();
        mddCanvasGroup = moduleDetailsPanel.GetComponent<CanvasGroup>();
        mddCanvasGroup.alpha = 0;
        mddCanvasGroup.interactable = false;
        mddCanvasGroup.blocksRaycasts = false;
    }

    #region New Game Methods
    public void CreateNewGame()
    {
        if(sfm.DoesSaveFileExists())
        {
            // if there is a save file, give player option to create new one
            saveFileAlreadyExists.SetActive(true);  
        } else
        {
            // if there is no save file, create a new one
            SetupNewGame();                            
        }
    }

    // called when creating a save file when there is no pre-existing save file
    public void SetupNewGame()                      
    {
        CreateNewSaveFile();
        OpenModulesList();
    }

    // called when creating a save file when there is one already existing in game
    public void CreateNewSaveFile()
    {
        SaveSystem.DeleteData();
        SaveSystem.CreateNewSaveFile();
        SaveFileManager sfm = SaveFileManager.GetInstance();
        sfm.ReloadPlayerData();
        OpenModulesList();
    }

    #endregion

    #region Load Game Methods
    public void LoadGame()
    {
        if (sfm.DoesSaveFileExists())
        {
            if(SaveSystem.CheckIfGameLockExist())
            {
                gameLockScreen.SetActive(true);
            } else
            {
                OpenModulesList();
            }
        }
        else
        {
            noSaveFilePresent.SetActive(true);
        }
    }

    public void LoadLatestLevel()
    {
        SaveFileManager sfm = SaveFileManager.GetInstance();
        sfm.ReloadPlayerData();
        PlayerData pd = sfm.saveFile;
        AsyncManager asyncm = AsyncManager.GetInstance();
        asyncm.LoadLevel(pd.currentSceneLocation);
    }

    #endregion

    #region Modules Management Methods

    private enum ModuleType
    {
        ModuleOne, ModuleTwo, FreePlay,
    }

    public void OpenModulesList()
    {
        modulesListPanel.SetActive(true);
        CheckModuleAvailability();
    }

    public void CloseModulesList()
    {
        modulesListPanel.SetActive(false);
    }

    private void CheckModuleAvailability()
    {
        moduleOne.onClick.RemoveAllListeners();
        moduleTwo.onClick.RemoveAllListeners();
        freePlay.onClick.RemoveAllListeners();

        SaveFileManager sfm = SaveFileManager.GetInstance();
        PlayerData saveFile = sfm.saveFile;

        // Module One
        if(!saveFile.moduleOneComplete)
        {
            moduleOne.interactable = true;
            moduleOne.onClick.AddListener(() =>
            {
                PopulateScreen(ModuleType.ModuleOne);
                try
                {
                    AudioManager.instance.PlaySFX("TapSFX");
                }
                catch (Exception ex)
                {
                    Debug.LogWarning("Could not play TapSFX from Module One button! " + ex);
                }
            });
        } else
        {
            moduleOne.interactable = false;
            moduleOne.gameObject.GetComponent<Image>().color = new Color(87, 299, 62);
        }

        // Module Two
        if (saveFile.moduleOneComplete && !saveFile.moduleTwoComplete)
        {
            moduleTwo.interactable = true;
            moduleTwo.onClick.AddListener(() =>
            {
                PopulateScreen(ModuleType.ModuleTwo);
                try
                {
                    AudioManager.instance.PlaySFX("TapSFX");
                } catch (Exception ex)
                {
                    Debug.LogWarning("Could not play TapSFX from Module Two button! " + ex);
                }
                
            });
        }
        else
        {
            moduleTwo.interactable = false;
            if(saveFile.moduleOneComplete)
            {
                moduleTwo.gameObject.GetComponent<Image>().color = new Color(87, 299, 62);
            }
        }

        if(saveFile.moduleOneComplete && saveFile.moduleTwoComplete)
        {
            freePlay.interactable = true;
            freePlay.onClick.AddListener(() =>
            {
                PopulateScreen(ModuleType.FreePlay);
                try
                {
                    AudioManager.instance.PlaySFX("TapSFX");
                }
                catch (Exception ex)
                {
                    Debug.LogWarning("Could not play TapSFX from Freeplay button! " + ex);
                }
            });

        }
        else
        {
            freePlay.interactable = false;
        }
    }

    private void PopulateScreen(ModuleType mt)
    {
        /*
            public TextMeshProUGUI moduleName;
            public TextMeshProUGUI moduleDetails;
            public Button playButton;
         */

        switch (mt)
        {
            case ModuleType.ModuleOne:
                moduleName.SetText("Module One");
                moduleDetails.SetText
                    ("In this module, you will learn how to the play the game. You will also learn how what " +
                    "stress is and a simple way to learn how to manage stress"
                    );
                playButton.onClick.AddListener(() =>
                {
                    SaveSystem.DeleteData();
                    SaveSystem.CreateNewSaveFile();
                    SaveSystem.CreateNewGameLock();
                    SaveFileManager sfm = SaveFileManager.GetInstance();
                    sfm.ReloadPlayerData();
                    Debug.Log("Playing Module One");
                    AsyncManager asyncm = AsyncManager.GetInstance();
                    asyncm.LoadLevel("Tutorial_BedroomScene");
                });
                break;
            case ModuleType.ModuleTwo:
                moduleName.SetText("Module Two");
                moduleDetails.SetText
                    ("In this module, you will learn about the Bilog ng Buhay and learn how to use it to destress" +
                    "yourself."
                    );
                playButton.onClick.AddListener(() =>
                {
                    SaveSystem.CreateNewGameLock();
                    Debug.Log("Playing Module two");
                    AsyncManager asyncm = AsyncManager.GetInstance();
                    asyncm.LoadLevel("Module2-TeacherScene1");
                });
                break;
            case ModuleType.FreePlay:
                moduleName.SetText("Free Play");
                moduleDetails.SetText
                    ("Play the game with no restrictions"
                    );
                playButton.onClick.AddListener(() =>
                {
                    SaveSystem.DeleteData();
                    SaveSystem.CreateNewSaveFile();
                    SaveSystem.CreateNewGameLock();
                    SaveFileManager sfm = SaveFileManager.GetInstance();
                    sfm.ReloadPlayerData();
                    AsyncManager asyncm = AsyncManager.GetInstance();
                    asyncm.LoadLevel("Tutorial_BedroomScene");
                    Debug.Log("Playing free Play mode");
                });
                break;
            default:
                break;
        }

        mddCanvasGroup.interactable = true;
        mddCanvasGroup.blocksRaycasts = true;
        mddCanvasGroup.alpha = 1;
    }

    public void CloseModuleDetailsWindow()
    {
        mddCanvasGroup.alpha = 0;
        mddCanvasGroup.interactable = false;
        mddCanvasGroup.blocksRaycasts = false;
        moduleName.SetText(string.Empty);
        moduleDetails.SetText(string.Empty);
        playButton.onClick.RemoveAllListeners();
    }

    #endregion

    #region Misc Methods

    public void CloseSaveFileAlreadyExistsWindow()
    {
        saveFileAlreadyExists.SetActive(false);
    }

    public void CloseNoSaveFilePresentWindow()
    {
        noSaveFilePresent.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion

    #region Deprecated
    [Obsolete()]
    private void SetupExistingGame()
    {
        PlayerData playerData = SaveSystem.LoadData();
        AsyncManager asyncManager = AsyncManager.GetInstance();
        asyncManager.LoadLevel(playerData.currentSceneLocation);
    }

    [Obsolete()]
    public void LoadIntoGame()
    {
        PlayerData playerData = SaveSystem.LoadData();
        AsyncManager asyncManager = AsyncManager.GetInstance();
        asyncManager.LoadLevel(playerData.currentSceneLocation);
    }
    #endregion
}
