using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookreadingActivity : Activity
{
    /*
    public override void ExecuteActivity(int hoursSpentOnActivity)
    {
        IncreasePlayerStat(hoursSpentOnActivity);
        ReduceStressLevel(hoursSpentOnActivity);
        clockManager.MoveTimeForwardByHours(hoursSpentOnActivity);
    }
    protected override void IncreasePlayerStat(int hoursSpentOnActivity)
    {
        playerStatsManager.IncreasePlayerAttackDamageStat(hoursSpentOnActivity);
    }*/
    public override string DisplayStatIncreaseBenefits(int hoursInvested)
    {
        throw new System.NotImplementedException();
    }

    public override void IncreasePlayerStat(int hoursInvested)
    {
        throw new System.NotImplementedException();
    }
}
