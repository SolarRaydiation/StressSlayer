using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyingActivity : Activity
{
    [Header("Studying Activity - Variables")]
    public float difficultyReductionPerHour = 0.25f;

    [Header("Studying Activity - Internals")]
    [SerializeField] NextLevelPreInitializer nlpi;

    /* =============================================
     * Initialization Functions
     * ========================================== */
    private void GetNextLevelPreInitializerFromWorldManager()
    {
        try
        {
            GameObject worldManager = GameObject.FindGameObjectWithTag("WorldManager");
            nlpi = worldManager.GetComponent<NextLevelPreInitializer>();
        }
        catch (Exception e)
        {
            Debug.LogError("Could not find NextLevelPreInitializer component from WorldManager!: " + e);
        }
    }


    /* =============================================
     * Inherited Functions
     * ========================================== */

    protected override void IncreasePlayerStat(int hoursSpentOnActivity)
    {
        // intentionally left blank
        // this function is not needed
    }

    public override void ExecuteActivity(int hoursSpentOnActivity)
    {
        if(nlpi != null)
        {
            if (IsThereEnoughTimeForActivity(hoursSpentOnActivity))
            {
                if(nlpi.CanReduceLevelDifficulty(hoursSpentOnActivity * difficultyReductionPerHour))
                {
                    nlpi.ReduceLevelDifficulty(hoursSpentOnActivity * difficultyReductionPerHour);
                    clockManager.MoveTimeForwardByHours(hoursSpentOnActivity);
                    Debug.Log("Difficulty reduced!");
                } else
                {
                    Debug.Log("Can't reduce difficulty!");
                }
            } else
            {
                Debug.Log("Not enough time to carry out activity!");
            }
        } else
        {
            GetNextLevelPreInitializerFromWorldManager();
            ExecuteActivity(hoursSpentOnActivity);
        }
    }
}
