using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NextLevelPreInitializer : MonoBehaviour
{
    private const float INTIAL_LEVEL_DIFFICULTY = 1.5f;
    private const float MINIMUM_LEVEL_DIFFICULTY = 0.5f;

    [Header("UI Externals")]
    public TextMeshProUGUI nextLevelDifficultyText;

    [Header("Next Level Difficulty")]
    public float nextLevelDifficulty;

    [Header("Internals")]
    private static NextLevelPreInitializer instance;


    //private const float DIFFICULTY_REDUCTION_PER_HOUR = 0.25f;

    /* =============================================
     * Initialization
     * ========================================== */

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("There is more than one NextLevelPreInitializer in the Scene!");
        }
        instance = this;
    }

    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        SaveFileManager sfm = SaveFileManager.GetInstance();
        PlayerData saveFile = sfm.saveFile;
        if (saveFile != null)
        {
            nextLevelDifficulty = saveFile.nextLevelDifficulty;
            Debug.Log("Savefile loaded to NextLevelPreInitializer");
        }
        else
        {
            nextLevelDifficulty = INTIAL_LEVEL_DIFFICULTY;
            Debug.LogError($"No save file found. NextLevelPreInitializer will default to fall-back.");
        }
    }

    public static NextLevelPreInitializer GetInstance()
    {
        return instance;
    }


    /* =============================================
     * Difficulty update functions
     * ========================================== */

    public void ReduceLevelDifficulty(float difficultyReductionRate)
    {
        if(nextLevelDifficulty - difficultyReductionRate < MINIMUM_LEVEL_DIFFICULTY)
        {
            nextLevelDifficulty = MINIMUM_LEVEL_DIFFICULTY;
        } else
        {
            nextLevelDifficulty = nextLevelDifficulty - difficultyReductionRate;
        }
        
        //UpdateNextLevelDifficultyTextUI();
    }

    public bool CanReduceLevelDifficulty(float difficultyReductionRate)
    {
        if((nextLevelDifficulty - difficultyReductionRate) >=  MINIMUM_LEVEL_DIFFICULTY)
        {
            Debug.Log($"nextLevelDifficulty: {nextLevelDifficulty} | difficultyReductionRate: {difficultyReductionRate}");
            Debug.Log($"{nextLevelDifficulty - difficultyReductionRate} >= {MINIMUM_LEVEL_DIFFICULTY}");
            return true;
        } else
        {
            Debug.Log($"{nextLevelDifficulty} {difficultyReductionRate}");
            Debug.Log($"{nextLevelDifficulty - difficultyReductionRate} >= {MINIMUM_LEVEL_DIFFICULTY}");
            return false;
        }
    }

    public float GetNextLevelDifficulty()
    {
        return nextLevelDifficulty;
    }

    public void UpdateNextLevelDifficultyTextUI()
    {
        nextLevelDifficultyText.text = nextLevelDifficulty.ToString();
    }
}
