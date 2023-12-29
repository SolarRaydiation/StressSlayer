using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    public float statusEffectDuration;
    private float collectibleLifeTime = 10.0f; // needed to delete avoid clutter

    private void Start()
    {
        StartCoroutine(DeleteCollectible(collectibleLifeTime));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyStatusEffectOnPlayer(other.gameObject);
            Destroy(gameObject);
        } else if(other.CompareTag("Enemy"))
        {
            ApplyStatusEffectOnEnemy(other.gameObject);
            Destroy(gameObject);
        }
    }
    protected abstract void ApplyStatusEffectOnPlayer(GameObject player);
    protected abstract void ApplyStatusEffectOnEnemy(GameObject enemy);

    IEnumerator DeleteCollectible(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);

    }
}
