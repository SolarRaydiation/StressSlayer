using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsScript : EntityStatsScript
{
    protected override void ExecuteOtherStartFunctions()
    {
        // intentionally left blank
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

    /* =============================================
     * For attacking enemies
     * ========================================== */

    [Header("Enemy Attack")]
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;

    public void AttackEnemy()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,
            attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            PlayerStatsScript pss = enemy.gameObject.GetComponent<PlayerStatsScript>();
            pss.DecreaseHealth(GetCurrentAttackDamage());
            Debug.Log($"We hit {enemy.name}!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
