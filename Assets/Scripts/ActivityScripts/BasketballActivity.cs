using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballActivity : Activity
{
    public override void ExecuteActivity(int hoursSpentOnActivity)
    {
        IncreasePlayerStat(hoursSpentOnActivity);
        stressManager.ReduceStress(hoursSpentOnActivity * stressReducedPerHour);
        clockManager.MoveTimeForwardByHours(hoursSpentOnActivity);
    }
    protected override void IncreasePlayerStat(int hoursSpentOnActivity)
    {
        playerStatsManager.IncreasePlayermaxHealthStat(hoursSpentOnActivity);
    }
}
