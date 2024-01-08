using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class EntityStatsScript : MonoBehaviour
{
    // Movements are handled in a separate script

    [Header("Entity Flag")]
    public bool isPlayer;

    [Header("Entity Health Values")]
    public float initialMaxHealth;
    public float initialActualMaxHealth;               // value not used if isPlayer set to false
    public Slider healthBar;

    [Header("Entity Damage Values")]
    public float baseAttackDamage;

    [Header("ESS Interrnrals")]
    [SerializeField] protected float currentHealth;
    protected bool isHealthZero;
    protected float maxHealth;
    protected float actualMaxHealth; // value not used if isPlayer set to false
    [SerializeField] protected float currentAttackDamage;
    [SerializeField] protected Animator animator;                      // for visual cues

    void Start()
    {
        InitializeHealthValues();
        InitializeDamageValues();
        GetAnimationComponent();
        ExecuteOtherStartFunctions();
    }

    public void InitializeHealthValues()
    {
        isHealthZero = false;
        maxHealth = initialMaxHealth;
        healthBar.maxValue = initialMaxHealth;
        if (isPlayer)
        {
            actualMaxHealth = initialActualMaxHealth;
            currentHealth = initialActualMaxHealth;
        }
        else
        {
            currentHealth = initialMaxHealth;
        }
        healthBar.value = currentHealth;
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

    // if the child classes need to execute their own start functions
    protected abstract void ExecuteOtherStartFunctions();


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
        } else if (currentHealth > actualMaxHealth && isPlayer)
        {
            currentHealth = actualMaxHealth;
        }
        UpdateHealthBar();
        ExecuteOtherIncreaseHealthFunctions();
    }

    public void DecreaseHealth(float healthDecrease)
    {
        currentHealth = currentHealth - healthDecrease;
        if (currentHealth > 0)
        {
            ExecuteOtherDecreaseHealthFunctions();
            UpdateHealthBar();
        }
        else
        {
            isHealthZero = true;
            UpdateHealthBar();
            EntityDeathFunction();
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.value = currentHealth;
    }

    protected abstract void ExecuteOtherIncreaseHealthFunctions();
    protected abstract void ExecuteOtherDecreaseHealthFunctions();
    protected abstract void EntityDeathFunction();

    protected IEnumerator KillEntity(string deathAnimationTrigger)
    {
        animator.SetTrigger(deathAnimationTrigger);
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

    public void UpdateCurrentAttackDamage(float newAttackDamage)
    {
        currentAttackDamage = newAttackDamage;
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
