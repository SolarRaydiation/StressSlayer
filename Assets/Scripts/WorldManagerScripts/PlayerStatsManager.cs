using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStatsManager : MonoBehaviour
{
    [Header("Flags")]
    // this file can potentially be used for both WorldManager and during Combat Levels
    public bool attachedToWorldManager;
    public bool attachedToPlayer;

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
    }
}
