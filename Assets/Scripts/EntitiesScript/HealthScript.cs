using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class HealthScript : MonoBehaviour
{
    [Header("Entity Flag")]
    public bool isPlayer;

    [Header("Entity Health Values")]
    public float initialMaxHealth;
    public float initialActualHealth; // value not used if isPlayer set to false

    [Header("Internals")]
    [SerializeField] private float currentHealth;
    [SerializeField] private bool isHealthZero;
    private float maxHealth;
    [SerializeField] private float actualMaxHealth; // value not used if isPlayer set to false
    private Animator animator; // for visual cues

    void Start()
    {
        InitializeHealthValues();
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

    public void GetAnimationComponent()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void IncreaseHealth(float healthIncrease)
    {
        currentHealth = currentHealth + healthIncrease;
        if(currentHealth > maxHealth && !isPlayer)
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
        if(currentHealth > 0)
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
}
