using System;
using System.Collections;
using UnityEngine;
using static System.TimeZoneInfo;

public class EntityStatsScript : MonoBehaviour
{
    // Movements are handled in a separate script

    [Header("Entity Flag")]
    public bool isPlayer;

    [Header("Entity Health Values")]
    public float initialMaxHealth;
    public float initialActualHealth;               // value not used if isPlayer set to false

    [Header("Entity Damage Values")]
    public float baseAttackDamage;

    [Header("Serialized Internals")]
    [SerializeField] private float currentHealth;
    [SerializeField] private bool isHealthZero;
    private float maxHealth;
    [SerializeField] private float actualMaxHealth; // value not used if isPlayer set to false
    [SerializeField] float currentAttackDamage;
    private Animator animator;                      // for visual cues

    void Start()
    {
        InitializeHealthValues();
        InitializeDamageValues();
        GetAnimationComponent();
    }

    public void InitializeHealthValues()
    {
        isHealthZero = false;
        maxHealth = initialMaxHealth;
        if (isPlayer)
        {
            actualMaxHealth = initialActualHealth;
            currentHealth = initialActualHealth;
        }
        else
        {
            currentHealth = initialMaxHealth;
        }
    }

    public void InitializeDamageValues()
    {
        currentAttackDamage = baseAttackDamage;
    }

    public void GetAnimationComponent()
    {
        try
        {
            animator = gameObject.GetComponent<Animator>();
        } catch (Exception e)
        {
            Debug.LogError($"GameObject {name} does not have an animator component!: {e}");
        }

    }


    /* =============================================
     * Health Methods
     * ========================================== */

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetActualMaxHealth()
    {
        return actualMaxHealth;
    }
    
    public void IncreaseHealth(float healthIncrease)
    {
        currentHealth = currentHealth + healthIncrease;
        if (currentHealth > maxHealth && !isPlayer)
        {
            currentHealth = maxHealth;
        } else
        {
            currentHealth = actualMaxHealth;
        }
        animator.SetTrigger("TookHealItem");
    }

    public void DecreaseHealth(float healthDecrease)
    {
        currentHealth = currentHealth - healthDecrease;
        if (currentHealth > 0)
        {
            animator.SetTrigger("TookDamage");
        }
        else
        {
            isHealthZero = false;
            StartCoroutine(KillEntity());
        }
    }

    IEnumerator KillEntity()
    {
        animator.SetTrigger("IsDead");
        yield return new WaitForSeconds(1.3f);
        GameObject.Destroy(gameObject);
    }

    /* =============================================
     * Attack Damage
     * ========================================== */

    public float GetCurrentAttackDamage()
    {
        return currentAttackDamage;
    }

    public float GetBaseAttackDamage()
    {
        return baseAttackDamage;
    }

    public void IncreaseCurrentAttackDamageTemporarily(float newAttackDamage, float seconds)
    {
        currentAttackDamage = newAttackDamage;
        StartCoroutine(AttackBonusDuration(seconds));
    }

    IEnumerator AttackBonusDuration(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        currentAttackDamage = baseAttackDamage;
    }
}
