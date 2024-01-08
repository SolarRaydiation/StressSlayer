using System;
using UnityEngine;
using static ClockManager;

[System.Serializable]
public class PlayerData
{
    #region Save Data
    // PlayerMovement
    public float baseMovementSpeed;

    // PlayerStatsController
    public string playerName;
    public float maxHealth;
    public int timesDrugWasTaken;
    public float baseAttackDamage;
    public float currentStressLevel;

    // ClockManager
    public DaySection currentDaySection;
    public int currentHour;
    public Day currentDay;

    // NextLevelPreInitializer
    public float nextLevelDifficulty;

    // Player Inventory Manager
    public int cashRemaining;
    public int fruitsAndVegetablesOwned;
    public int cortisolInjectorsOwned;
    public int assortedDrugsOwned;

    #endregion

    public PlayerData(PlayerMovement pm, PlayerStatsController psc, ClockManager cm, 
        NextLevelPreInitializer nlpi, PlayerInventoryManager pim)
    {
        // Player Movement
        try
        {
            baseMovementSpeed = pm.baseMovementSpeed;
            Debug.Log("Successfully retrieved PlayerMovement data!");
        } catch (Exception e)
        {
            Debug.LogWarning($"Failed to completely retrieve data from PlayerMovement!: {e}");
        }

        // Player Stats Controller
        try
        {
            playerName = psc.playerName;
            maxHealth = psc.maxHealth;
            timesDrugWasTaken = psc.timesDrugWasTaken;
            baseAttackDamage = psc.baseAttackDamage;
            currentStressLevel = psc.GetCurrentStressLevel();
            Debug.Log("Successfully retrieved PlayerStatsController data!");
        } catch (Exception e)
        {
            Debug.LogWarning($"Failed to completely retrieve data from PlayerStatsController!: {e}");
        }

        // ClockManager
        try
        {
            currentDaySection = cm.currentDaySection;
            currentHour = cm.currentHour;
            currentDay = cm.currentDay;
            Debug.Log("Successfully retrieved ClockManager data!");
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Failed to completely retrieve data from ClockManager!: {e}");
        }

        // NLPI
        try
        {
            nextLevelDifficulty = nlpi.GetNextLevelDifficulty();
            Debug.Log("Successfully retrieved NextLevelPreInitializer data!");
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Failed to completely retrieve data from NextLevelPreInitializer!: {e}");
        }

        // Player Inventory Manager
        try
        {
            cashRemaining = pim.GetCashStat();
            fruitsAndVegetablesOwned = pim.GetFruitsAndVegetablesOwned();
            cortisolInjectorsOwned = pim.GetCortisolInjectorsOwned();
            assortedDrugsOwned = pim.GetAssortedDrugsOwned();
            Debug.Log("Successfully retrieved PlayerInventoryManager data!");
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Failed to completely retrieve data from PlayerInventoryManager!: {e}");
        }
    }

    public PlayerData()
    {
        baseMovementSpeed = 12;
        playerName = "Juan";
        maxHealth = 10;
        timesDrugWasTaken = 0;
        baseAttackDamage = 5f;
        currentStressLevel = 30;
        currentDaySection = 0;
        currentHour = 7;
        currentDay = 0;
        nextLevelDifficulty = 1.5f;
        cashRemaining = 0;
        fruitsAndVegetablesOwned = 0;
        cortisolInjectorsOwned = 0;
        assortedDrugsOwned = 0;
    }
}
