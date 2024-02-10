using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsScript : EntityStatsScript
{
    [Header("PSS Specific")]
    public string playerName;
    public int timesDrugWasTaken;
    public float healthRegenRate;
    public static PlayerStatsScript instance;

    protected override void ExecuteOtherStartFunctions()
    {
        SaveFileManager sfm = SaveFileManager.GetInstance();
        PlayerData saveFile = sfm.saveFile;
        timesDrugWasTaken = saveFile.timesDrugWasTaken;
        playerName = saveFile.playerName;
        StartCoroutine(RegenerateHealth());

        if(instance != null )
        {
            Debug.LogWarning("More than one instance of PlayerStatsScript in the scene!");
        }
        instance = this;
    }

    public static PlayerStatsScript GetInstance()
    {
        return instance;
    }

    protected override void EntityDeathFunction()
    {
        Debug.Log("PlayerDead");
        StartCoroutine(KillEntity("Dead"));
    }

    protected override void ExecuteOtherDecreaseHealthFunctions()
    {
        animator.SetTrigger("RecievedDamage");
    }

    protected override void ExecuteOtherIncreaseHealthFunctions()
    {
        animator.SetTrigger("HitHealItem");
    }

    /* =============================================
     * For attacking enemies
     * ========================================== */

    [Header("Player Attack")]
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;

    public void AttackEnemy()
    {
        try
        {
            AudioManager.instance.PlaySFX("SwordSwing");
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Could not play HurtSound SFX in {name}: {e}");
        }

        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,
            attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            EnemyStatsScript ess = enemy.gameObject.GetComponent<EnemyStatsScript>();
            ess.DecreaseHealth(GetCurrentAttackDamage());
            Debug.Log($"We hit {enemy.name}!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    /* =============================================
     * Drug stuff
     * ========================================== */

    public void SimulateEffectsOfDrugUse()
    {
        timesDrugWasTaken++;
        actualMaxHealth = maxHealth - (maxHealth * (timesDrugWasTaken * 0.1f));
        currentHealth -= 1;
        UpdateHealthBar();
    }

    /* =============================================
     * Health Regen
     * ========================================== */

    IEnumerator RegenerateHealth()
    {
        yield return new WaitForSeconds(healthRegenRate);
        IncreaseHealth(1);
        StartCoroutine(RegenerateHealth());
    }
}
