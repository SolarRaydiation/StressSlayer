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
    [SerializeField] private int maxHealth;
    [SerializeField] private int attackDamage;
    [SerializeField] private int timesDrugWasTaken; // this file can potentially handle the drug system you've been thinking of
    [SerializeField] private int cashRemaining;

    /* =============================================
     * Initialization
     * ========================================== */

    private void Start()
    {
        LoadPlayerStats();
        UpdateCashRemainingTextUI();
    }

    private void LoadPlayerStats()
    {
        // later replace this with getting the stats from
        // a savefile
        maxHealth = 10;
        attackDamage = 1;
        timesDrugWasTaken = 0;
        cashRemaining = 0;
    }

    /* =============================================
     * Getter/Setter Methods
     * ========================================== */

    public int GetPlayerHealthStat()
    {
        return maxHealth;
    }
    public void IncreasePlayermaxHealthStat(int changeInmaxHealth)
    {
        maxHealth = maxHealth + changeInmaxHealth;
    }

    public int GetPlayerAttackDamageStat()
    {
        return attackDamage;
    }

    public void IncreasePlayerAttackDamageStat(int changeInAttackDamage)
    {
        attackDamage = attackDamage + changeInAttackDamage;
    }

    public int GetPlayerCashStat()
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
     * UI Update Methods
     * ========================================== */

    private void UpdateCashRemainingTextUI()
    {
        cashRemainingText.text = cashRemaining.ToString();
    }


}
