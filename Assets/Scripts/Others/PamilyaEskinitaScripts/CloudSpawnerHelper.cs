using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawnerHelper : MonoBehaviour
{
    CollectibleSpawner sp;
    ClockManager cm;

    public GameObject morningCloud;
    public GameObject afternoonCloud;
    public GameObject eveningCloud;

    // Update is called once per frame
    void Start()
    {
        sp = gameObject.GetComponent<CollectibleSpawner>();
        cm = ClockManager.GetInstance();
        sp.canSpawnCollectibles = true;

        if (cm.currentDaySection == ClockManager.DaySection.Morning)
        {
            sp.collectiblePrefab = morningCloud;
            sp.canSpawnCollectibles = true;
            enabled = false;
            return;
        }

        if (cm.currentDaySection == ClockManager.DaySection.Afternoon)
        {
            sp.collectiblePrefab = afternoonCloud;
            sp.canSpawnCollectibles = true;
            enabled = false;
            return;
        }

        if (cm.currentDaySection == ClockManager.DaySection.Evening)
        {
            sp.collectiblePrefab = eveningCloud;
            sp.canSpawnCollectibles = true;
            enabled = false;
            return;
        }

        
    }
}
