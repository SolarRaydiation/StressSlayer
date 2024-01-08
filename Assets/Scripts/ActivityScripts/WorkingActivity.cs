using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingActivity : Activity
{
    [Header("Working Activity Variables")]
    public int earningsPerHour;

    public override string DisplayStatIncreaseBenefits(int hoursInvested)
    {
        return $"You will gain {earningsPerHour * hoursInvested} cash";
    }


    public override void IncreasePlayerStat(int hoursInvested)
    {
        playerInventoryManager.IncreaseCash(earningsPerHour * hoursInvested);
    }
}
