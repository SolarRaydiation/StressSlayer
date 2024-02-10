using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiologyCollectible : Collectible
{
    public float statBonus;
    protected override void ApplyStatusEffectOnPlayer(GameObject player)
    {
        Debug.Log($"The player name is {player.name}");

        // add bonus to speed
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.IncreaseCurrentMovementSpeedTemporarily
            (
            playerMovement.GetCurrentMovementSpeed() + (playerMovement.GetCurrentMovementSpeed() * statBonus),
            statusEffectDuration
            );
        
        // add bonus to damage
        EntityStatsScript ess = player.GetComponent<EntityStatsScript>();
        ess.IncreaseCurrentAttackDamageTemporarily
            (
            ess.GetCurrentAttackDamage() + (ess.GetCurrentAttackDamage() * statBonus),
            statusEffectDuration
            );
    }
    protected override void ApplyStatusEffectOnEnemy(GameObject enemy)
    {
        // add bonus to speed here
        EnemyBehaviorScript ebs = enemy.GetComponent<EnemyBehaviorScript>();
        StartCoroutine(ebs.TemporarilyInceaseSpeed(statBonus, statusEffectDuration));

        // add bonus to damage
        EntityStatsScript ess = enemy.GetComponent<EntityStatsScript>();
        ess.IncreaseCurrentAttackDamageTemporarily
            (
            ess.GetCurrentAttackDamage() + (ess.GetCurrentAttackDamage() * statBonus),
            statusEffectDuration
            );
    }

    protected override void ExecuteOtherStartFunctions()
    {
        // intentionally left blank
    }
}
