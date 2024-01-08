using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ClockManager : MonoBehaviour
{
    private static ClockManager instance;
    public Transform clockHandTransform;
    public TextMeshProUGUI clockTimeText;
    public enum DaySection {
        Morning,                // 0
        Afternoon,              // 1
        Evening                 // 2
    }

    public enum Day
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
    }

    [Header("Day and Time")]
    public DaySection currentDaySection;
    public int currentHour;
    public Day currentDay;

    [Header("Internals")]
    [SerializeField] private const int MAX_HOURS = 23;                                      
    [SerializeField] private const int CHANGE_IN_ROTATION_PER_HOUR_PASSED = -30;            // it takes around -30 degrees of rotation to move the clockhand by one hour

    #region Initialization
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is more than one instance of ClockManager in the Scene!");
        }
        instance = this;
        ResetClock();
        currentHour = 7;
        UpdateClockUI();
        SetDaySection();
    }

    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        SaveFileManager sfm = SaveFileManager.GetInstance();
        PlayerData saveFile = sfm.saveFile;
        if (saveFile != null)
        {
            currentDaySection = saveFile.currentDaySection;
            currentHour = saveFile.currentHour;
            currentDay = saveFile.currentDay;
            Debug.Log("Savefile loaded to ClockManager");
        }
        else
        {
            currentDaySection = DaySection.Morning;
            currentHour = 6;
            currentDay = Day.Monday;
            Debug.LogError($"No save file found. ClockManager will default to fall-back.");
        }
    }

    #endregion

    #region Core Methods

    public void MoveForwardTimeByNHours(int n)
    {
        if(currentHour + n > MAX_HOURS)
        {
            currentHour = MAX_HOURS - (currentHour + n);
        }

        currentHour += n;
        SetDaySection();
    }

    public void SetDaySection()
    {
        if (currentHour < 12)
        {
            currentDaySection = DaySection.Morning;
            UpdateClockUI();
            return;
        }

        if (currentHour < 19)
        {
            currentDaySection = DaySection.Afternoon;
            UpdateClockUI();
            return;
        }

        currentDaySection = DaySection.Evening;
        UpdateClockUI();
        return;
    }

    public void UpdateClockUI()
    {
        // Update the clock itself
        clockHandTransform.eulerAngles = new Vector3(0, 0, CHANGE_IN_ROTATION_PER_HOUR_PASSED * currentHour);

        // Update the text displaying the time
        int hourToShow = currentHour % 12;
        if(hourToShow ==  0)
        {
            hourToShow = 12;
        }
        if (currentHour < 12)
        {
            clockTimeText.text = string.Concat(hourToShow.ToString(), ":00am");
        }
        else
        {
            clockTimeText.text = string.Concat(hourToShow.ToString(), ":00pm");
        }
    }

    public void ResetClock()
    {
        currentHour = 0;
        clockHandTransform.eulerAngles = new Vector3(0, 0, 0);
        clockTimeText.SetText("");
    }

    public void MoveDayForward()
    {
        currentDay = currentDay + 1;
    }

    #endregion

    #region Getter Methods

    public int GetCurrentHour()
    {
        return currentHour;
    }

    public int GetTimeLeftInDay()
    {
        return MAX_HOURS - currentHour;
    }

    public DaySection GetCurrentDaySection()
    {
        return currentDaySection;
    }

    public Day GetCurrentDay()
    {
        return currentDay;
    }

    public static ClockManager GetInstance()
    {
        return instance;
    }

    #endregion
}
