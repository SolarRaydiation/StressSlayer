using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClockManager : MonoBehaviour
{
    private static ClockManager instance;
    public Transform clockHandTransform;
    public TextMeshProUGUI clockTimeText;
    public TextMeshProUGUI dayText;

    public enum DaySection
    {
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
    private const int MAX_HOURS = 23;
    private const int CHANGE_IN_ROTATION_PER_HOUR_PASSED = -30;            // it takes around -30 degrees of rotation to move the clockhand by one hour

    #region Initialization
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is more than one instance of ClockManager in the Scene!");
        }
        instance = this;
        Time.timeScale = 1.0f;
        
    }

    private void Start()
    {
        LoadData();
        UpdateClockUI();
        SetDaySection();
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
            UpdateDayUI(currentDay);
            Debug.Log("Savefile loaded to ClockManager");
        }
        else
        {
            currentDaySection = DaySection.Morning;
            currentHour = 6;
            currentDay = Day.Monday;
            UpdateDayUI(currentDay);
            Debug.LogError($"No save file found. ClockManager will default to fall-back.");
        }
    }

    #endregion

    #region Time Control Methods

    public void MoveForwardTimeByNHours(int n)
    {
        if (currentHour + n > MAX_HOURS)
        {
            currentHour = MAX_HOURS - (currentHour + n);
        }

        currentHour += n;
        SetDaySection();

        // repaint DayNight cycle in PamilyaEskinita Scene
        DayNightCycleManager dncm = DayNightCycleManager.instance;
        if(dncm != null)
        {
            dncm.PaintDayNightCycle();
        }
        
        // recheck availability of interactables
        InteractablesManager.instance.RecheckAvailability();
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

    private void UpdateClockUI()
    {
        if(clockHandTransform == null || clockTimeText == null || dayText == null)
        {
            Debug.LogWarning("UI for ClockManager not set");
            return;
        }

        // Update the clock itself
        if(clockHandTransform.gameObject.activeSelf)
        {
            clockHandTransform.eulerAngles = new Vector3(0, 0, CHANGE_IN_ROTATION_PER_HOUR_PASSED * currentHour);
        }
        

        // Update the text displaying the time
        if(clockTimeText.gameObject.activeSelf)
        {
            int hourToShow = currentHour % 12;
            if (hourToShow == 0)
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
    }

    private void UpdateDayUI(Day day)
    {
        if (clockHandTransform == null || clockTimeText == null || dayText == null)
        {
            Debug.LogWarning("UI for ClockManager not set");
            return;
        }

        if (dayText.gameObject.activeSelf)
        {
            switch (day)
            {
                case Day.Monday:
                    dayText.SetText("Monday");
                    break;
                case Day.Tuesday:
                    dayText.SetText("Tuesday");
                    break;
                case Day.Wednesday:
                    dayText.SetText("Wednesday");
                    break;
                case Day.Thursday:
                    dayText.SetText("Thursday");
                    break;
                case Day.Friday:
                    dayText.SetText("Friday");
                    break;
                default:
                    Debug.LogError($"Recieved invalid Day value {day} while updating Day UI!");
                    dayText.SetText("ERROR!");
                    break;
            }
        }
        
    }

    public void ResetClock()
    {
        currentHour = 0;
        clockHandTransform.eulerAngles = new Vector3(0, 0, 0);
        clockTimeText.SetText("");
    }

    public void SetToFreeRoam()
    {
        currentHour = 15;
    }

    public void MoveDayForward()
    {
        currentDay = currentDay + 1;
        currentHour = 7;
        UpdateClockUI();
        UpdateDayUI(currentDay);
        NextLevelPreInitializer nlpi = NextLevelPreInitializer.GetInstance();
        nlpi.ResetLevelDifficulty();

        SaveFileManager sfm = SaveFileManager.GetInstance();
        sfm.SavePlayerData(SceneManager.GetActiveScene().name);
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
