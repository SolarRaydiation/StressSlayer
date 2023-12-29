using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviSciCollectible : Collectible
{
    protected override void ApplyStatusEffectOnPlayer(GameObject player)
    {
        EntityStatsScript ess = player.gameObject.GetComponent<EntityStatsScript>();
        ess.IncreaseHealth(ess.GetActualMaxHealth());
    }
    protected override void ApplyStatusEffectOnEnemy(GameObject enemy)
    {
        EntityStatsScript ess = enemy.gameObject.GetComponent<EntityStatsScript>();
        ess.IncreaseHealth(ess.GetMaxHealth());
    }
}
