using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingActivity : Activity
{
    [Header("Working Activity - Variables")]
    public int earningsPerHour;
    public override void ExecuteActivity(int hoursSpentOnActivity)
    {
        if(IsThereEnoughTimeForActivity(hoursSpentOnActivity))
        {
            IncreasePlayerStat(hoursSpentOnActivity);
            clockManager.MoveTimeForwardByHours(hoursSpentOnActivity);
        }
    }

    protected override void IncreasePlayerStat(int hoursSpentOnActivity)
    {
        playerStatsManager.IncreasePlayerCashStat(earningsPerHour * hoursSpentOnActivity);
    }
}
