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
    public GameObject noSaveFilePresent;
    public GameObject modulesListPanel;

    [Header("Module List Buttons")]
    public Button moduleOne;
    public Button moduleTwo;
    public Button moduleThree;
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
        OpenModulesList();
    }

    #endregion

    #region Load Game Methods
    public void LoadGame()
    {
        if (sfm.DoesSaveFileExists())
        {
            OpenModulesList();
        }
        else
        {
            noSaveFilePresent.SetActive(true);
        }
    }

    #endregion

    #region Modules Management Methods

    private enum ModuleType
    {
        ModuleOne, ModuleTwo, ModuleThree, FreePlay,
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
        moduleThree.onClick.RemoveAllListeners();
        freePlay.onClick.RemoveAllListeners();

        SaveFileManager sfm = SaveFileManager.GetInstance();
        sfm.LoadPlayerData();
        PlayerData saveFile = sfm.saveFile;

        // Module One
        if (!saveFile.moduleOneComplete)
        {
            moduleOne.interactable = true;
            moduleOne.onClick.AddListener(() =>
                {
                    PopulateScreen(ModuleType.ModuleOne);
                    moduleOne.gameObject.GetComponent<AudioSource>().Play();
                });
        }
        else
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
                moduleTwo.gameObject.GetComponent<AudioSource>().Play();
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

        // Module Three
        if (saveFile.moduleOneComplete && saveFile.moduleTwoComplete && !saveFile.moduleThreeComplete)
        {
            moduleThree.interactable = true;
            moduleThree.onClick.AddListener(() =>
            {
                PopulateScreen(ModuleType.ModuleThree);
                moduleThree.gameObject.GetComponent<AudioSource>().Play();
            });
        }
        else
        {
            moduleThree.interactable = false;
            if(saveFile.moduleOneComplete && saveFile.moduleTwoComplete)
            {
                moduleThree.gameObject.GetComponent<Image>().color = new Color(87, 299, 62);
            }
        }

        if(saveFile.moduleOneComplete && saveFile.moduleTwoComplete && saveFile.moduleThreeComplete)
        {
            freePlay.interactable = true;
            freePlay.onClick.AddListener(() =>
            {
                PopulateScreen(ModuleType.FreePlay);
                freePlay.gameObject.GetComponent<AudioSource>().Play();
            });

        } else
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
                    Debug.Log("Playing Module one");
                });
                break;
            case ModuleType.ModuleTwo:
                moduleName.SetText("Module Two");
                moduleDetails.SetText
                    ("In this module, you will learn how to the play the game. You will also learn how what " +
                    "stress is and a simple way to learn how to manage stress"
                    );
                playButton.onClick.AddListener(() =>
                {
                    Debug.Log("Playing Module two");
                });
                break;
            case ModuleType.ModuleThree:
                moduleName.SetText("Module Three");
                moduleDetails.SetText
                    ("In this module, you will learn how to the play the game. You will also learn how what " +
                    "stress is and a simple way to learn how to manage stress"
                    );
                playButton.onClick.AddListener(() =>
                {
                    Debug.Log("Playing Module three");
                });
                break;
            case ModuleType.FreePlay:
                moduleName.SetText("Free Play");
                moduleDetails.SetText
                    ("Play the game with no restrictions"
                    );
                playButton.onClick.AddListener(() =>
                {
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
