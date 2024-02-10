using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [Header("Public Variables")]
    public float durationBetweenSpawn;
    public GameObject collectiblePrefab;
    public Vector2 spawnAreaSize = new Vector2(10f, 10f);

    [Header("Internals")]
    public bool canSpawnCollectibles = false;
    private float timeRemaining;

    void Start()
    {
        timeRemaining = durationBetweenSpawn;
    }

    public void SetFlag(bool b)
    {
        canSpawnCollectibles = b;
    }

    private void FixedUpdate()
    {
        if(canSpawnCollectibles)
        {
            timeRemaining = timeRemaining - Time.deltaTime;
            if(timeRemaining <= 0)
            {
                SpawnCollectible();
                timeRemaining = durationBetweenSpawn;
            }
        }
    }

    private void SpawnCollectible()
    {
        Vector2 centerPosition = transform.position;
        Vector2 randomPosition = new Vector2(Random.Range(centerPosition.x - spawnAreaSize.x / 2f, centerPosition.x + spawnAreaSize.x / 2f),
                                             Random.Range(centerPosition.y - spawnAreaSize.y / 2f, centerPosition.y + spawnAreaSize.y / 2f));
        Instantiate(collectiblePrefab, randomPosition, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        // Draw a wireframe rectangle representing the spawn area
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 0f));
    }
}
