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
    void Start()
    {
        sp = gameObject.GetComponent<CollectibleSpawner>();
        cm = ClockManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if(cm.currentDaySection == ClockManager.DaySection.Morning)
        {
            sp.collectiblePrefab = morningCloud;
            return;
        }

        if (cm.currentDaySection == ClockManager.DaySection.Afternoon)
        {
            sp.collectiblePrefab = afternoonCloud;
            return;
        }

        if (cm.currentDaySection == ClockManager.DaySection.Evening)
        {
            sp.collectiblePrefab = eveningCloud;
            return;
        }
    }
}
