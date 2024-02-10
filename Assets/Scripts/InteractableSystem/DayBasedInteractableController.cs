using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ClockManager;

public class DayBasedInteractableController : MonoBehaviour
{
    public bool availableOnMonday;
    public bool availableOnTuesday;
    public bool availableOnWednesday;
    public bool availableOnThursday;
    public bool availableOnFriday;

    void Update()
    {
        CheckDayAvailability();
    }

    private void CheckDayAvailability()
    {
        ClockManager cm = ClockManager.GetInstance();

        switch (cm.currentDay)
        {
            case Day.Monday:
                if (availableOnMonday) { gameObject.SetActive(true); } else { gameObject.SetActive(false); }
                break;
            case Day.Tuesday:
                if (availableOnTuesday) { gameObject.SetActive(true); } else { gameObject.SetActive(false); }
                break;
            case Day.Wednesday:
                if (availableOnWednesday) { gameObject.SetActive(true); } else { gameObject.SetActive(false); }
                break;
            case Day.Thursday:
                if (availableOnThursday) { gameObject.SetActive(true); } else { gameObject.SetActive(false); }
                break;
            case Day.Friday:
                if (availableOnFriday) { gameObject.SetActive(true); } else { gameObject.SetActive(false); }
                break;
            default:
                Debug.LogError($"Recieved unknown Day type {cm.currentDay} while checking day availability!");
                gameObject.SetActive(false);
                break;
        }
    }
}
