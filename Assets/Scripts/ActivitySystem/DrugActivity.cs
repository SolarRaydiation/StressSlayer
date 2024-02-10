using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugActivity : Activity
{
    /* public override string DisplayStatIncreaseBenefits(int hoursInvested)
     {
         throw new System.NotImplementedException();
     }

     public override void ExecuteActivity(int hoursSpentOnActivity)
     {
         ReduceStressLevel(hoursSpentOnActivity);
         playerStatsManager.SimulateEffectsOfDrugUse();
         clockManager.MoveTimeForwardByHours(hoursSpentOnActivity);
     }
     protected override void IncreasePlayerStat(int hoursSpentOnActivity)
     {
         // intentionally left blank
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
