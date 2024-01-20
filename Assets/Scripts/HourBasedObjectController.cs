using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayBasedObjectController : ApperanceControlManager
{
    protected override void ExecuteOutsideAvailableHours()
    {
        gameObject.SetActive(false);
    }

    protected override void ExecuteStartFunctions()
    {
        // intentionally left blank
    }

    protected override void ExecuteWithinAvailableHours()
    {
        gameObject.SetActive(true);
    }

    public void RecheckAvailability()
    {
        CheckAvailability();
    }
}
