using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballActivity : Activity
{
    public override string DisplayStatIncreaseBenefits(int hoursInvested)
    {
        return $"You will increase your health by {hoursInvested} points.";
    }

    public override void IncreasePlayerStat(int hoursInvested)
    {
        playerStatsController.IncreaseMaxHealth(hoursInvested);
    }
}
