
using System;
using System.Data.Common;
using UnityEngine;

public abstract class Activity : MonoBehaviour
{
    public string adverb;                                         // the adverb of activity
    public int stressReducedPerHour;                              // how much stress is reduced during activity         
    public bool willReduceStress;
    public bool willIncreaseStat;

    [Header("Internals")]
    protected ActivitySystem activitySystem;
    protected PlayerStatsController playerStatsController;
    protected ClockManager clockManager;
    protected PlayerInventoryManager playerInventoryManager;
    protected NextLevelPreInitializer nextLevelPreInitializer;

    /* =============================================
     * Initialization Methods
     * ========================================== */
    private void Start()
    {
        try
        {
            activitySystem = ActivitySystem.GetInstance();
            playerStatsController = PlayerStatsController.GetInstance();
            clockManager = ClockManager.GetInstance();
            playerInventoryManager = PlayerInventoryManager.GetInstance();
            nextLevelPreInitializer = NextLevelPreInitializer.GetInstance();
        } catch (Exception e)
        {
            Debug.LogError("Could not retrieve static references from WorldManager!: " + e);
        }
    }

    /* =============================================
     * Methods Related to Reducing Stress
     * ========================================== */
    public void ReduceStressLevel(int hoursInvested)
    {
        playerStatsController.ReduceStress(stressReducedPerHour * hoursInvested);
    }

    public string DisplayStressReductionBenefits(int hoursInvested)
    {
        return $"You will reduce your stress by {stressReducedPerHour * hoursInvested}.";
    }

    /* =============================================
     * Methods Related to Improving Player Stats
     * ========================================== */
    
    // any increase in the stat level of the player
    public abstract void IncreasePlayerStat(int hoursInvested);

    // shows by how much those stats will increase
    public abstract string DisplayStatIncreaseBenefits(int hoursInvested);

    /* =============================================
     * Core Methods
     * ========================================== */

    public void TriggerActivitySystem()
    {
        ActivitySystem activitySystem = ActivitySystem.GetInstance();
        activitySystem.EnterActivityMode(this);
    }
}
