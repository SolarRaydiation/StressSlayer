using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    private bool hasFinishedSpawning = false;
    private void Awake()
    {
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
    }

    public void SpawnEnemy()
    {
        if(!hasFinishedSpawning)
        {
            Instantiate(enemyToSpawn, spawnPosition, spawnRotation);
            hasFinishedSpawning = true;
        }
        
    }
}
