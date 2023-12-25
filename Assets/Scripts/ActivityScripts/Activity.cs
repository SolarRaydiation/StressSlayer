
using System;
using UnityEngine;

public abstract class Activity : MonoBehaviour
{
    [Header("Activity - Details")]
    public string activityName;
    public int stressReducedPerHour;
    public string statName;
    public bool hasStressReduction;
    public bool hasStatIncrease;

    [Header("Activity - Script References")]
    [SerializeField] protected ActivitySystem activitySystem;
    [SerializeField] protected StressManager stressManager;
    [SerializeField] protected PlayerStatsManager playerStatsManager;
    [SerializeField] protected ClockManager clockManager;

    /* =============================================
     * Initialization Methods
     * ========================================== */
    private void Start()
    {
        GetStressManagerFromWorldManager();
        GetPlayerStatsManagerFromWorldManager();
        GetClockManagerFromWorldManager();
        GetActivitySystemFromWorldManager();
    }

    private void GetStressManagerFromWorldManager()
    {
        try
        {
            GameObject worldManager = GameObject.FindGameObjectWithTag("WorldManager");
            stressManager = worldManager.GetComponent<StressManager>();
        } catch (Exception e)
        {
            Debug.LogError($"{name} could not locate WorldManager and the StressManager script!: " + e);
        }
    }

    private void GetActivitySystemFromWorldManager()
    {
        // find activity system
        try
        {
            activitySystem = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<ActivitySystem>();
        }
        catch (Exception e)
        {
            Debug.LogError($"{name} could not locate WorldManager and the ActivitySystem script!: " + e);
        }
    }

    private void GetPlayerStatsManagerFromWorldManager()
    {
        try
        {
            GameObject worldManager = GameObject.FindGameObjectWithTag("WorldManager");
            playerStatsManager = worldManager.GetComponent<PlayerStatsManager>();
        }
        catch (Exception e)
        {
            Debug.LogError($"{name} could not locate WorldManager and the PlayerStatsManager script!: " + e);
        }
    }

    private void GetClockManagerFromWorldManager()
    {
        try
        {
            GameObject worldManager = GameObject.FindGameObjectWithTag("WorldManager");
            clockManager = worldManager.GetComponent<ClockManager>();
        }
        catch (Exception e)
        {
            Debug.LogError($"{name} could not locate WorldManager and the ClockManager script!: " + e);
        }
    }

    /* =============================================
     * Methods
     * ========================================== */

    // function for applying the rewards for engaging in this activity
    public abstract void ExecuteActivity(int hoursSpentOnActivity);

    // function for increasing the player stat
    protected abstract void IncreasePlayerStat(int hoursSpentOnActivity);

    // function for reducing stress (if any) for engaging in this activity
    protected void ReduceStressLevel(int hoursPassed)
    {
        stressManager.ReduceStress(stressReducedPerHour * hoursPassed);
    }

    // function for checking if there is time to do the activity
    protected bool IsThereEnoughTimeForActivity(int hoursToSpend)
    {
        if(hoursToSpend <= clockManager.GetNumberOfHoursInDayLeft())
        {
            return true;
        } else
        {
            return false;
        }
    }
    // function to tirggering activity system to work
    public void ActivateActivitySystem()
    {
        activitySystem.Initialize(this);
    }

    public string CalculateStressReduction(int hoursPassed)
    {
        return $"You will {stressReducedPerHour * hoursPassed} points";
    }

    public string CalculateStatIncreaseBenefits(int hoursPassed)
    {
        return $"You will {hoursPassed} points in {statName}";
    }
}
