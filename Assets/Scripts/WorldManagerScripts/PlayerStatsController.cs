using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static ClockManager;

public class PlayerStatsController : MonoBehaviour
{
    /**
     * 1. Track player health and any changes to it
     * 2. Track player damage and any changes to it
     * 3. Track player stress and any changes to it
     * 4. Have those changes saved to the savefile so the combat
     * levels can handle it.
     */

    private static PlayerStatsController instance;

    [Header("Player Name")]
    public string playerName;

    [Header("Health Component")]
    public float maxHealth;
    private float actualMaxHealth;
    public int timesDrugWasTaken;
    private const float HEALTH_DECREASE_PER_DRUG_USE = 0.1f;
    public Slider healthBar;

    [Header("Attack Component")]
    public float baseAttackDamage;

    [Header("Stress Component")]
    public Slider stressBar;
    public float initialStressLevel;

    #region Initialization
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Warning! More than one PlayerStatsController in Scene!");
        }
        instance = this;
    }

    private void Start()
    {
        LoadData();
        InitializeHealthBar();
        InitializeStressBar();
    }

    private void LoadData()
    {
        SaveFileManager sfm = SaveFileManager.GetInstance();
        PlayerData saveFile = sfm.saveFile;
        if (saveFile != null)
        {
            playerName = saveFile.playerName;
            maxHealth = saveFile.maxHealth;
            timesDrugWasTaken = saveFile.timesDrugWasTaken;
            baseAttackDamage = saveFile.baseAttackDamage;
            initialStressLevel = saveFile.currentStressLevel;
            Debug.Log("Savefile loaded to PlayerStatsController");
        }
        else
        {
            playerName = "Fallback";
            maxHealth = 100;
            timesDrugWasTaken = 0;
            baseAttackDamage = 10;
            initialStressLevel = 30.0f;
            Debug.LogError($"No save file found. PlayerStatsController will default to fall-back.");
        }
    }

    public static PlayerStatsController GetInstance()
    {
        return instance;
    }

    #endregion

    #region Name Methods

    public string GetPlayerName()
    {
        return playerName;
    }

    #endregion

    #region Health Stats

    // Completed. No need to touch any more.

    /// <summary>
    /// Initializes the values of the health bar at game start with the maxHealth
    /// and actualMaxHealth values.
    /// </summary>
    private void InitializeHealthBar()
    {
        healthBar.maxValue = maxHealth;
        SetActualMaxHealth();
        healthBar.value = actualMaxHealth;
    }

    /// <summary>
    /// Updates the health bar with the current maxHealth and actualMaxHealth values.
    /// </summary>
    private void UpdateHealthBar()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = actualMaxHealth;
    }

    /// <summary>
    /// Calculates the new actualMaxHealth of the player based on the number of times a
    /// drug was taken and the health decrease per drug item used.
    /// </summary>
    private void SetActualMaxHealth()
    {
        actualMaxHealth = maxHealth - (maxHealth * (timesDrugWasTaken * HEALTH_DECREASE_PER_DRUG_USE));
    }

    /// <summary>
    /// A function for simulating the effects of drug use on health. When called, it calculates
    /// the effects of drug use on health based on a set of parametersencapsulated within
    /// PlayerStatsController.
    /// </summary>
    public void SimulateEffectsOfDrugUse()
    {
        timesDrugWasTaken++;
        SetActualMaxHealth();
        UpdateHealthBar();
    }

    /// <summary>
    /// Increases the maxHealth attribute of the player. This function also takes into
    /// changes the actualMaxHealth attribute of the player accordingly.
    /// </summary>
    /// <param name="change"></param>
    public void IncreaseMaxHealth(int change)
    {
        maxHealth = maxHealth + change;
        SetActualMaxHealth();
        UpdateHealthBar();
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetActualMaxHealthStat()
    {
        return actualMaxHealth;
    }

    #endregion

    #region Damage Stats

    // Completed. No need to touch any more.

    public float GetBaseAttackDamage()
    {
        return baseAttackDamage;
    }

    public void IncreaseGetBaseAttackDamage(int change)
    {
        baseAttackDamage = baseAttackDamage + change;
    }

    #endregion

    #region Stress Stats


    // Completed. No need to touch any more.

    /**
     * Implementation Notes:
     * Originally, this function would have its own currentStressLevel and
     * a const MAX_STRESS to track the stress level of the player, but that
     * seems redudant since the stressBar does that anyways.
     */

    public void InitializeStressBar()
    {
        stressBar.value = initialStressLevel;
        stressBar.maxValue = 100;
    }

    public void IncreaseStress(int amount)
    {
        stressBar.value = stressBar.value + amount;
        if (stressBar.value > stressBar.maxValue)
        {
            stressBar.value = stressBar.maxValue;
        }
    }

    public void ReduceStress(int amount)
    {
        stressBar.value = stressBar.value - amount;
        if (stressBar.value < 0)
        {
            stressBar.value = 0;
        }
    }

    public float GetCurrentStressLevel()
    {
        return stressBar.value;
    }

    #endregion
}
