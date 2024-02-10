using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyBehaviorScript : MonoBehaviour
{
    [Header("Enemy Behavior")]
    public GameObject target;
    public float movementSpeed;
    public float cooldownPeriod;

    [Header("Enemy Attack")]
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    public float knockbackStrength;
    public AudioSource attackSFX;

    private Transform enemyTransform;
    private Transform targetTransform;
    private bool playerInRange;
    private bool isAttackOnCooldown;
    private Animator animator;
    private EnemyStatsScript ess;
    private int positionEqualityThreshold = 1;

    void Start()
    {
        GetComponentReferences();
    }

    private void GetComponentReferences()
    {
        enemyTransform = GetComponent<Transform>();
        GetPlayerTransform();
        animator = GetComponent<Animator>();
        ess = GetComponent<EnemyStatsScript>(); 
    }

    private void GetPlayerTransform()
    {
        target = GameObject.Find("Player");
        targetTransform = target.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if(playerInRange && !isAttackOnCooldown)
        {
            AttackPlayer();
        } else
        {
            MoveEnemy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Player found!");
            playerInRange = true;
        }
    }

    private void MoveEnemy()
    {
        if(targetTransform != null)
        {
            float distance = Vector3.Distance(enemyTransform.position, targetTransform.position);
            if (distance < positionEqualityThreshold)
            {
                // do nothing
                return;
            }

            // move left if the player is in the left, right if the player is in the right
            if (targetTransform.position.x < enemyTransform.position.x)
            {
                transform.Translate(Vector3.left * Time.deltaTime * movementSpeed);
                Vector3 currentScale = transform.localScale;
                currentScale.x = -2.5f;
                transform.localScale = currentScale;
            }
            else if (targetTransform.position.x > enemyTransform.position.x)
            {
                transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
                Vector3 currentScale = transform.localScale;
                currentScale.x = 2.5f;
                transform.localScale = currentScale;
            }
        } else
        {
            // do nothing
        }
    }

    private void AttackPlayer()
    {
        if (attackSFX != null)
        {
            attackSFX.Play();
        }
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,
            attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            PlayerStatsScript pss = enemy.gameObject.GetComponent<PlayerStatsScript>();
            pss.DecreaseHealth(ess.GetCurrentAttackDamage());
            Debug.Log($"We hit {enemy.name}!");
        }
        isAttackOnCooldown = true;
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(cooldownPeriod);
        isAttackOnCooldown = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void KnockbackEnemy()
    {
        if (targetTransform != null)
        {
            // move left if the player is in the left, right if the player is in the right

            // knockback right if the player is in the left of the enemy    
            if (targetTransform.position.x < enemyTransform.position.x)
            {
               transform.Translate(Vector3.right * Time.deltaTime * movementSpeed * knockbackStrength);
            }

            // knockback left if player is in the right of the enemy
            else if (targetTransform.position.x > enemyTransform.position.x)
            {
                transform.Translate(Vector3.left * Time.deltaTime * movementSpeed * knockbackStrength);
            }
        }
    }

    public IEnumerator TemporarilyInceaseSpeed(float multiplicand, float duration)
    {
        float originalSpeed = movementSpeed;
        movementSpeed = originalSpeed * multiplicand;
        yield return new WaitForSeconds(duration);
        movementSpeed = originalSpeed;
    }
}
