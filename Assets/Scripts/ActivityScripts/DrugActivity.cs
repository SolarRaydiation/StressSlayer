using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugActivity : Activity
{
    public override void ExecuteActivity(int hoursSpentOnActivity)
    {
        stressManager.ReduceStress(hoursSpentOnActivity * stressReducedPerHour);
        playerStatsManager.SimulateEffectsOfDrugUse();
        clockManager.MoveTimeForwardByHours(hoursSpentOnActivity);
    }
    protected override void IncreasePlayerStat(int hoursSpentOnActivity)
    {
        // intentionally left blank
    }
}
