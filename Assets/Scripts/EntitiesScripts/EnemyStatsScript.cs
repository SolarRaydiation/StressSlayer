using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsScript : EntityStatsScript
{
    protected override void ExecuteOtherStartFunctions()
    {
        NextLevelPreInitializer nlpi = NextLevelPreInitializer.GetInstance();
        if(nlpi != null )
        {
            baseAttackDamage = baseAttackDamage * nlpi.nextLevelDifficulty;
            maxHealth = initialMaxHealth;
            currentHealth = maxHealth;
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }
    }

    protected override void EntityDeathFunction()
    {
        StartCoroutine(KillEntity("Dead"));
    }

    protected override void ExecuteOtherDecreaseHealthFunctions()
    {
        animator.SetTrigger("Hurt");
    }

    protected override void ExecuteOtherIncreaseHealthFunctions()
    {
        animator.SetTrigger("Heal");
    }
}
