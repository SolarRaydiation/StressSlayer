using System;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    // others
    public string currentSceneLocation;
    public string lastSceneLocation;

    // module tracking
    public bool moduleOneComplete;
    public bool moduleTwoComplete;
    public bool moduleThreeComplete;

    #endregion

    // regular play
    public PlayerData(PlayerMovement pm, PlayerStatsController psc, ClockManager cm,
        NextLevelPreInitializer nlpi, PlayerInventoryManager pim, string nextScene)
    {
        // Player Movement
        try
        {
            baseMovementSpeed = pm.baseMovementSpeed;
            Debug.Log("Successfully retrieved PlayerMovement data!");
        }
        catch (Exception e)
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
        }
        catch (Exception e)
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

        // Name of scene
        try
        {
            currentSceneLocation = nextScene;
            lastSceneLocation = SceneManager.GetActiveScene().name;
        }
        catch
        {
            Debug.LogWarning("Could not get scene name!");
        }
    }

    // combat level
    public PlayerData(PlayerMovement pm, PlayerStatsScript pss, StressManager sm, ClockManager cm,
         GameboostManager gbm, string nextScene)
    {
        // Player Movement
        try
        {
            baseMovementSpeed = pm.baseMovementSpeed;
            Debug.Log("Successfully retrieved PlayerMovement data!");
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Failed to completely retrieve data from PlayerMovement!: {e}");
        }

        // Player Stats Script and Stress Manager
        try
        {
            playerName = pss.playerName;
            maxHealth = pss.maxHealth;
            timesDrugWasTaken = pss.timesDrugWasTaken;
            baseAttackDamage = pss.baseAttackDamage;
            currentStressLevel = sm.currentStressLevel;
            Debug.Log("Successfully retrieved Player Stats Script and Stress Manager data!");
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Failed to completely retrieve data from Player Stats Script and Stress Manager!: {e}");
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
            nextLevelDifficulty = 1.5f;
            Debug.Log("Successfully retrieved NextLevelPreInitializer data!");
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Failed to completely retrieve data from NextLevelPreInitializer!: {e}");
        }

        // Gameboost Manager
        try
        {
            cashRemaining = gbm.cashRemaining;
            fruitsAndVegetablesOwned = gbm.fruitsAndVegetablesOwned;
            cortisolInjectorsOwned = gbm.cortisolInjectorsOwned;
            assortedDrugsOwned = gbm.assortedDrugsOwned;
            Debug.Log("Successfully retrieved Gameboost Manager data!");
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Failed to completely retrieve data from Gameboost Manager!: {e}");
        }

        // Name of scene
        try
        {
            currentSceneLocation = nextScene;
            lastSceneLocation = SceneManager.GetActiveScene().name;
        }
        catch
        {
            Debug.LogWarning("Could not get scene name!");
        }
    }

    // save for module two and three
    public PlayerData(PlayerData pd, bool isModuleOneComplete, bool isModuleTwoComplete, bool isModuleThreeComplete )
    {
        baseMovementSpeed = pd.baseMovementSpeed;

        playerName = pd.playerName;
        maxHealth = pd.maxHealth;
        timesDrugWasTaken = pd.timesDrugWasTaken;
        baseAttackDamage = pd.baseAttackDamage;
        currentStressLevel = pd.currentStressLevel;

        if(isModuleOneComplete && isModuleTwoComplete)
        {
            // set up for Module Two
            currentDaySection = DaySection.Afternoon;
            currentHour = 15;
            currentDay = Day.Wednesday;
        } else if(isModuleOneComplete)
        {
            currentDaySection = DaySection.Afternoon;
            currentHour = 15;
            currentDay = Day.Tuesday;
        }

        nextLevelDifficulty = pd.nextLevelDifficulty;

        // Player Inventory Manager
        cashRemaining = pd.cashRemaining;
        fruitsAndVegetablesOwned = pd.fruitsAndVegetablesOwned;
        cortisolInjectorsOwned = pd.cortisolInjectorsOwned;
        assortedDrugsOwned = pd.assortedDrugsOwned;

        // others
        currentSceneLocation = "";
        lastSceneLocation = "";

        // module tracking
        moduleOneComplete = isModuleOneComplete;
        moduleTwoComplete = isModuleTwoComplete;
        moduleThreeComplete = isModuleThreeComplete;
    }



    public PlayerData()
    {
        baseMovementSpeed = 12;
        playerName = "";
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
        currentSceneLocation = "";
        lastSceneLocation = "";
        moduleOneComplete = false;
        moduleTwoComplete = false;
        moduleThreeComplete = false;
    }
}
