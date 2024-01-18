using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ApperanceControlManager : MonoBehaviour
{
    public int timeAvailableStart;
    public int timeAvailableEnd;
    private ClockManager clockManager;

    void Start()
    {
        clockManager = ClockManager.GetInstance();
        ExecuteStartFunctions();
        CheckAvailability();
    }

    protected void CheckAvailability()
    {
        if (clockManager.currentHour >= timeAvailableStart && clockManager.currentHour <= timeAvailableEnd)
        {
            ExecuteWithinAvailableHours();
        }
        else
        {
            ExecuteOutsideAvailableHours();
        }
    }

    protected abstract void ExecuteStartFunctions();
    protected abstract void ExecuteWithinAvailableHours();
    protected abstract void ExecuteOutsideAvailableHours();
}
