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
    
    protected override void IncreasePlayerStat(int hoursSpentOnActivity)
    {
        // intentionally left blank
        // this function is not needed
    }

    public override void ExecuteActivity(int hoursSpentOnActivity)
    {
        if(nlpi != null)
        {
            if(IsThereEnoughTimeForActivity(hoursSpentOnActivity))
            {
                if(nlpi.ReduceNextLevelDifficulty(hoursSpentOnActivity *
                    difficultyReductionPerHour))
                {
                    Debug.Log("Difficulty reduced!");
                    clockManager.MoveTimeForwardByHours(hoursSpentOnActivity);
                }
                else
                {
                    Debug.Log("Difficulty limit reached!");
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
