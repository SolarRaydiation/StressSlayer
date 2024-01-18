using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookreadingActivity : Activity
{
    public override string DisplayStatIncreaseBenefits(int hoursInvested)
    {
        return $"You will increase your damage by {hoursInvested} points.";
    }

    public override void IncreasePlayerStat(int hoursInvested)
    {
        playerStatsController.IncreaseGetBaseAttackDamage(hoursInvested);
    }
}