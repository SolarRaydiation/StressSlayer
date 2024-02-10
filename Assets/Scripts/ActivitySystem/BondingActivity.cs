using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BondingActivity : Activity
{
    // I can forsee future issues with this activity, because other activities
    // will need access to this flag somehow; even if the bonding activity script
    // is not on the same scene. Here's a potential solution.

    // the Activity superclass will probably need to have a function to get the
    // flag's value from a save file, while only the BondingActivity subclass
    // can update the save file if the flag has a new update.

    [SerializeField] private bool investedMoreNineHours = false; 
    [SerializeField] private int hoursInvested;

    public override string DisplayStatIncreaseBenefits(int hoursInvested)
    {
        return null;
    }

    public override void IncreasePlayerStat(int hoursInvested)
    {
        // intentionally left blank
    }

    /* =============================================
     * INITIALIZATION FUNCTIONS
     * ========================================== 

    private void HasInvestedMoreThanNineHours()
    {
        if(hoursInvested >= 9)
        {
            investedMoreNineHours = true;
        }
    }

    public bool GetInvestedNineHoursFlag()
    {
        return investedMoreNineHours;
    }

    /* =============================================
     * Inherited Functions
     * ========================================== 

    public override void ExecuteActivity(int hoursSpentOnActivity)
    {
        if (IsThereEnoughTimeForActivity(hoursSpentOnActivity))
        {
            ReduceStressLevel(hoursSpentOnActivity);
            hoursInvested++;
            HasInvestedMoreThanNineHours();
            clockManager.MoveTimeForwardByHours(hoursSpentOnActivity);
        } else
        {
            Debug.Log($"Unable to complete {activityName}. Not enough time left.");
        }
    }

    protected override void IncreasePlayerStat(int hoursSpentOnActivity)
    {
        // intentionally left blank
    }*/
}
