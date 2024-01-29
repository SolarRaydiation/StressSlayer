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
    public float initialMaxHealth;                                      // health w/o drug use considered
    public float initialActualMaxHealth;                                // health w drug use considered
    public Slider healthBar;

    [Header("Entity Damage Values")]
    public float baseAttackDamage;

    [Header("ESS Interrnrals")]
    [SerializeField] protected float currentHealth;
    [SerializeField] protected bool isHealthZero = false;
    public float maxHealth;                         // health w/o drug use considered
    [SerializeField] protected float actualMaxHealth;                   // health w drug use considered
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
    {/*
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
        healthBar.value = currentHealth;*/

        if (isPlayer)
        {
            SaveFileManager sfm = SaveFileManager.GetInstance();
            PlayerData saveFile = sfm.saveFile;

            // set the max health of the player as if no drugs were taken
            maxHealth = saveFile.maxHealth;
            healthBar.maxValue = saveFile.maxHealth;

            // set the actual max health of the player considerring the drugs
            // he or she has taken
            actualMaxHealth = saveFile.maxHealth - (saveFile.maxHealth * (saveFile.timesDrugWasTaken * 0.1f));

            // set current health to equal max health
            currentHealth = actualMaxHealth;
        } else // an enemy
        {
            maxHealth = initialMaxHealth;
            currentHealth = initialMaxHealth;
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
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

    // if the child classes need to execute their own start functions
    protected abstract void ExecuteOtherStartFunctions();

    private void Update()
    {
        if(currentHealth <= 0 && !isHealthZero)
        {
            isHealthZero = true;
            EntityDeathFunction();
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

    public void UpdateHealthBar()
    {
        healthBar.value = currentHealth;
    }

    protected abstract void ExecuteOtherIncreaseHealthFunctions();
    protected abstract void ExecuteOtherDecreaseHealthFunctions();
    protected abstract void EntityDeathFunction();

    protected IEnumerator KillEntity(string deathAnimationTrigger)
    {
        Debug.Log($"Destroying {gameObject.name}...");
        animator.SetTrigger(deathAnimationTrigger);
        yield return new WaitForSeconds(0.5f);
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
