using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnHelper : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Floor")
        {
            Destroy(rb);
            Destroy(boxCollider);
            Destroy(this);
        }
    }
}
