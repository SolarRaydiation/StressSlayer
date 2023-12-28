using System;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Public Variables")]
    public GameObject exitDoor;
    public GameObject[] enemySpawners;

    [Header("Internals")]
    [SerializeField] private bool hasRoomStarted;
    [SerializeField] private bool hasRoomEnded;

    /* ===========================================
     * Initialization Functions
     * ========================================== */

    void FixedUpdate()
    {
        if(hasRoomStarted && !hasRoomEnded)
        {
            hasRoomEnded = AreThereEnemiesLeft();
        }

        if(hasRoomEnded)
        {
            RoomClear();
        }
    }

    public void RoomStart()
    {
        // spawn all enemies
        foreach (GameObject obj in enemySpawners)
        {
            EnemySpawner es = obj.GetComponent<EnemySpawner>();
            es.SpawnEnemy();
        }
    }

    public void RoomClear()
    {
        // open exit door
        WallMover temp = exitDoor.GetComponent<WallMover>();
        temp.SetCanMoveUp(true);

        // clean up room and remove all unneeded items to improve performance
        ImprovePerformance();
    }

    private bool AreThereEnemiesLeft()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length == 0)
        {
            return true;
        }
        return false;
    }

    private void ImprovePerformance()
    {
        foreach (GameObject obj in enemySpawners)
        {
            GameObject.Destroy(obj);
        }
        GameObject.Destroy(gameObject);
    }

    /* ===========================================
     * Supporting Functions
     * ========================================== */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RoomStart();
        hasRoomStarted = true;
    }

}
