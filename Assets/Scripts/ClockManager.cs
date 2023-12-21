using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Transform clockHandTransform;
    public TextMeshProUGUI clockTimeText;

    private int currentTimeInHours;
    private const int INITIAL_START_TIME_IN_HOURS = 3;
    private const int MAX_TIME_IN_HOURS = 12;
    private const int CHANGE_IN_ROTATION_PER_HOUR_PASSED = -30;
    
    private void Awake()
    {
        currentTimeInHours = INITIAL_START_TIME_IN_HOURS;
        UpdateCurrentTime();
    }
    private void UpdateCurrentTime()
    {
        // update the clock UI
        clockHandTransform.eulerAngles = new Vector3(0, 0, CHANGE_IN_ROTATION_PER_HOUR_PASSED * currentTimeInHours);

        // update the text displaying the time
        clockTimeText.text = string.Concat(currentTimeInHours.ToString(), ":00pm");
    }

    /// <summary>
    /// If the player performs an activity, the Clock system will first check if the
    /// number of hours they will spend in the activity is valid. That is, if the time
    /// will not exceed beyond twelve midnight. If it is valid, this function returns
    /// true to tell the subsystem handling the activity the player wants to perform that
    /// they can carry out the activity. Otherwise, false.
    /// </summary>
    /// <param name="hoursPassed"></param>
    /// <returns></returns>
    public bool MoveTimeForwardByHours(int hoursPassed)
    {
        if(currentTimeInHours + hoursPassed > MAX_TIME_IN_HOURS)
        {
            return false;
        } else
        {
            currentTimeInHours += hoursPassed;
            UpdateCurrentTime();
            return true;
        }
    }

    public int GetNumberOfHoursInDayLeft()
    {
        return MAX_TIME_IN_HOURS - currentTimeInHours;
    }  
}
