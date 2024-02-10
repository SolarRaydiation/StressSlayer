using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    public float statusEffectDuration;
    private float collectibleLifeTime = 10.0f; // needed to delete avoid clutter

    [Header("Text Prefab")]
    public GameObject floatingTextPrefab;
    public string floatingText;

    private void Start()
    {
        ExecuteOtherStartFunctions();
        StartCoroutine(DeleteCollectible(collectibleLifeTime));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyStatusEffectOnPlayer(other.gameObject);
            SpawnFloatingText();
            Destroy(gameObject);
        } else if(other.CompareTag("Enemy"))
        {
            ApplyStatusEffectOnEnemy(other.gameObject);
            SpawnFloatingText();
            Destroy(gameObject);
        }
    }
    protected abstract void ApplyStatusEffectOnPlayer(GameObject player);
    protected abstract void ApplyStatusEffectOnEnemy(GameObject enemy);

    protected abstract void ExecuteOtherStartFunctions();

    private void SpawnFloatingText()
    {
        GameObject obj = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        obj.GetComponent<FloatingTextScript>().text = floatingText;
    }

    IEnumerator DeleteCollectible(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);

    }
}
