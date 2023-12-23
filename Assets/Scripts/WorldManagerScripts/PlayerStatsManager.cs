using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStatsManager : MonoBehaviour
{
    [Header("Flags")]
    // this file can potentially be used for both WorldManager and during Combat Levels
    public bool attachedToWorldManager;
    public bool attachedToPlayer;

    [Header("UI Externals")]
    public TextMeshProUGUI cashRemainingText;

    [Header("Serialized Private Variables - Player Stats")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float actualMaxHealth;
    [SerializeField] private float attackDamage;
    [SerializeField] private float cashRemaining;

    [Header("Serialized Private Variables - Drug System")]
    [SerializeField] private const float PERCENTAGE_DECREASE_PER_DRUG_USE = 0.05f;
    [SerializeField] private const int drugUseLimit = 5;
    [SerializeField] private int timesDrugWasTaken;
    [SerializeField] private bool KILL_PLAYER_LATER_FLAG;

    /* =============================================
     * Initialization
     * ========================================== */

    private void Start()
    {
        LoadPlayerStats();
        UpdateCashRemainingTextUI();
        KILL_PLAYER_LATER_FLAG = false;
    }

    private void LoadPlayerStats()
    {
        // later replace this with getting the stats from
        // a savefile
        maxHealth = 10;
        attackDamage = 1;
        cashRemaining = 0;
        SetPlayerActualMaxHealth();
    }

    /* =============================================
     * Getter/Setter Methods
     * ========================================== */

    public float GetPlayerHealthStat()
    {
        return maxHealth;
    }

    public float GetPlayerActualMaxHealthStat()
    {
        return actualMaxHealth;
    }

    public void IncreasePlayermaxHealthStat(int changeInmaxHealth)
    {
        maxHealth = maxHealth + changeInmaxHealth;
    }

    public float GetPlayerAttackDamageStat()
    {
        return attackDamage;
    }

    public void IncreasePlayerAttackDamageStat(int changeInAttackDamage)
    {
        attackDamage = attackDamage + changeInAttackDamage;
    }

    public float GetPlayerCashStat()
    {
        return cashRemaining;
    }

    public void IncreasePlayerCashStat(int changeInCash)
    {
        cashRemaining = cashRemaining + changeInCash;
        UpdateCashRemainingTextUI();
    }

    /// <summary>
    /// First checks if the changeInCash will not cause cashRemaining. If yes,
    /// returns true and deducts change in playercash. The calling function can
    /// also proceed in their execution. Otherwise, false.
    /// </summary>
    /// <param name="changeInCash"></param>
    /// <returns></returns>
    public bool DecreasePlayerCashStat(int changeInCash)
    {
        if((cashRemaining - changeInCash) >= 0)
        {
            cashRemaining = cashRemaining - changeInCash;
            UpdateCashRemainingTextUI();
            return true;
        } else
        {
            return false;
        }
    }

    public void DebugDecreasePlayerCashStat(int changeInCash)
    {
        Debug.Log(DecreasePlayerCashStat(changeInCash));
    }

    /* =============================================
     * Drug System
     * ========================================== */

    private void SetPlayerActualMaxHealth()
    {
        Debug.Log((timesDrugWasTaken * PERCENTAGE_DECREASE_PER_DRUG_USE));
        Debug.Log(maxHealth * (timesDrugWasTaken * PERCENTAGE_DECREASE_PER_DRUG_USE));
        actualMaxHealth = maxHealth - (maxHealth * (timesDrugWasTaken * PERCENTAGE_DECREASE_PER_DRUG_USE));
    }

    public void SimulateEffectsOfDrugUse()
    {
        timesDrugWasTaken++;
        if(timesDrugWasTaken <= drugUseLimit)
        {
            SetPlayerActualMaxHealth();
        } else
        {
            KILL_PLAYER_LATER_FLAG = true;
        }
    }

    public bool GetKillPlayerByDrugFlag()
    {
        return KILL_PLAYER_LATER_FLAG;
    }


    /* =============================================
     * UI Update Methods
     * ========================================== */

    private void UpdateCashRemainingTextUI()
    {
        cashRemainingText.text = cashRemaining.ToString();
    }


}
