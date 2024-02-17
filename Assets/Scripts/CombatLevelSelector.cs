using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatLevelSelector : MonoBehaviour
{
    [SerializeField] LevelLoader levelLoader;

    [Header("Levels")]
    public CombatLevelDetails[] combatLevels;
    public string moduleTwoLevel;

    private void Start()
    {
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    public void SelectLevel()
    {
        // check if we are on a Module 2 level
        SaveFileManager sfm = SaveFileManager.GetInstance();
        PlayerData playerData = sfm.saveFile;
        if(playerData != null)
        {
            if (!playerData.moduleTwoComplete)
            {
                Debug.Log("Run Module 2 work!");
                levelLoader.LoadNextLevel(moduleTwoLevel);
                return;
            }
        }

        // check if current hour
        ClockManager cm = ClockManager.GetInstance();

        CombatLevelDetails level = Array.Find(combatLevels, x => x.Day == cm.GetCurrentDay());
        if(level != null)
        {
            levelLoader.LoadNextLevel(level.levelName);
            return;
        }
        else
        {
            Debug.LogWarning($"Could not find the associated level for {level.Day}!");
            return;
        }
    }
}
