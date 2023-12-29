using System;
using System.Collections;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class RoomManager : MonoBehaviour
{
    [Header("Public Variables")]
    public GameObject exitDoor;
    public CollectibleSpawner collectiblesSpawner;
    public GameObject[] enemySpawners;
    public Animator animator;

    [Header("Flags")]
    [SerializeField] protected bool hasRoomStarted;
    [SerializeField]  protected bool hasRoomEnded;
    
    /* ===========================================
     * Initialization Functions
     * ========================================== */

    private void Start()
    {
        
    }

    /* ===========================================
     * Core Functions
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

    private void RoomStart()
    {
        // spawn all enemies
        foreach (GameObject obj in enemySpawners)
        {
            EnemySpawner es = obj.GetComponent<EnemySpawner>();
            es.SpawnEnemy();
        }

        // tell collectible spawner they can start spawning
        collectiblesSpawner.SetFlag(true);

        // disable boxcollider to prevent animation looping
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        // animation
        TriggerStartingAnimation();

        // provide children classes room to implement their own stuff
        ExecuteRoomStartFunctions();
    }

    private void RoomClear()
    {
        // open exit door
        WallMover temp = exitDoor.GetComponent<WallMover>();
        temp.SetCanMoveUp(true);

        // tell collectible spawner to stop spawning
        collectiblesSpawner.SetFlag(false);

        // ending animation
        TriggerEndingAnimation();

        // let children take care of anything on their end
        ExecuteRoomEndFunctions();

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


    /* ===========================================
     * Supporting Functions
     * ========================================== */

    // needed to delete objects that are no longer needed for the game
    private void ImprovePerformance()
    {
        // destroy all enemy spawners
        foreach (GameObject obj in enemySpawners)
        {
            GameObject.Destroy(obj);
            StartCoroutine(AsyncDeleteObject(obj, 1.0f));
        }

        // delete all collectibles
        try
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Collectibles");
            foreach (GameObject obj in enemies)
            {
                StartCoroutine(AsyncDeleteObject(obj, 1.0f));
            }
        } catch (Exception ex)
        {

        }
        

        // delete collectible spawner itself
        StartCoroutine(AsyncDeleteObject(collectiblesSpawner.gameObject, 1.0f));

        // finally, delete the room manager itself
        enabled = false;
    }

    // for triggering the start of the room
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RoomStart();
        hasRoomStarted = true;
    }

    // for asynchronous deletion of objects
    protected IEnumerator AsyncDeleteObject(GameObject obj, float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(obj);
    }

    /* ===========================================
     * Supporting Animations
     * ========================================== */

    protected abstract void TriggerStartingAnimation();
    protected abstract void TriggerEndingAnimation();
    protected abstract void ExecuteRoomStartFunctions();
    protected abstract void ExecuteRoomEndFunctions();
}
