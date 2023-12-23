using System;
using UnityEngine;

public abstract class ActivityManager : MonoBehaviour
{
    [SerializeField] private ActivitySystem activitySystem;

    private void Start()
    {
        // find activity system
        try
        {
            activitySystem = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<ActivitySystem>();
        } catch (Exception e)
        {
            Debug.LogError($"{name} could not locate WorldManager and the ActivitySystem script!: " + e);
        }
        
    }
    public void Initialize()
    {
        activitySystem.Initialize();
    }

    public abstract void StartActivity(int hoursSpentOnActivity);
}
