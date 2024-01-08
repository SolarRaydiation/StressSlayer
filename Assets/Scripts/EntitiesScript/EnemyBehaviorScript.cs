using System;
using UnityEngine;

public class EnemyBehaviorScript : MonoBehaviour
{
    [Header("Movement Variables")]
    public GameObject target;
    public float movementSpeed;
    private Rigidbody2D rb;
    private Transform enemyTransform;
    private Transform targetTransform;
    
    void Start()
    {
        GetComponentReferences();
    }

    private void GetComponentReferences()
    {
        enemyTransform = GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        targetTransform = target.transform;
    }

    private void FixedUpdate()
    {
        MoveEnemy();
    }


    /* =============================================
     * Movement-Related Functions
     * ========================================== */

    private void MoveEnemy()
    {
        // move left if the player is in the left, right if the player is in the right
        if (targetTransform.position.x < enemyTransform.position.x)
        {
            Debug.Log("Moving to the left!");
            transform.Translate(Vector3.left * Time.deltaTime * movementSpeed);
            //rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
        }
        else if (targetTransform.position.x > enemyTransform.position.x)
        {
            Debug.Log("Moving to the right!");
            transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
            //rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
        } else
        {
            // do nothing
        }
    }
}
