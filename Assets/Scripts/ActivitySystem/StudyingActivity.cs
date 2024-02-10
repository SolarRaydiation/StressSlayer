using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyingActivity : Activity
{
    [Header("Studying Activity Variables")]
    public float difficultyReductionPerHour = 0.25f;

    public override string DisplayStatIncreaseBenefits(int hoursInvested)
    {
        return $"You will reduce the difficulty of the next level to {(hoursInvested * difficultyReductionPerHour) * 100}%" +
            $"from {nextLevelPreInitializer.GetNextLevelDifficulty() * 100}% (minimum level difficulty of 50%).";
    }

    public override void IncreasePlayerStat(int hoursInvested)
    {
        nextLevelPreInitializer.ReduceLevelDifficulty(hoursInvested * difficultyReductionPerHour);
    }

    /* =============================================
     * Inherited Functions
     * ========================================== 

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

    public override string DisplayStatIncreaseBenefits(int hoursInvested)
    {
        throw new NotImplementedException();
    }*/
}
