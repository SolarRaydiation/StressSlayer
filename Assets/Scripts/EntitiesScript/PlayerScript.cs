using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [Header("Entity Health Values")]
    public Slider healthBar;


    [Header("Internrals")]
    [SerializeField] private float initialMaxHealth;
    [SerializeField] private float initialActualMaxHealth;
    [SerializeField] private float baseAttackDamage;
    [SerializeField] private float currentHealth;
    [SerializeField] private bool isHealthZero;
    [SerializeField] private float maxHealth;
    [SerializeField] private float actualMaxHealth; // value not used if isPlayer set to false
    [SerializeField] private float currentAttackDamage;
    [SerializeField] private Animator animator;

    void Start()
    {
        SaveFileManager sfm = SaveFileManager.GetInstance();
        PlayerData saveFile = sfm.saveFile;
        if(saveFile != null)
        {
            initialMaxHealth = saveFile.maxHealth;
            int timesDrugWasTaken = saveFile.timesDrugWasTaken;
            initialActualMaxHealth = maxHealth - (maxHealth * (timesDrugWasTaken * 0.1f));
            baseAttackDamage = saveFile.baseAttackDamage;
        }

        try
        {
            animator = gameObject.GetComponent<Animator>();
        }
        catch (Exception e)
        {
            Debug.LogError($"GameObject {name} does not have an animator component!: {e}");
        }

        actualMaxHealth = initialActualMaxHealth;
        currentHealth = initialActualMaxHealth;
        healthBar.value = currentHealth;
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
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth > actualMaxHealth)
        {
            currentHealth = actualMaxHealth;
        }
        UpdateHealthBar();
        animator.SetTrigger("HitHealItem");
    }

    public void DecreaseHealth(float healthDecrease)
    {
        currentHealth = currentHealth - healthDecrease;
        if (currentHealth > 0)
        {
            animator.SetTrigger("RecievedDamage");
            UpdateHealthBar();
        }
        else
        {
            isHealthZero = true;
            UpdateHealthBar();
            StartCoroutine(KillEntity("Dead"));
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.value = currentHealth;
    }

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
