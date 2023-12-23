using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivityConfirmationScreenManager : MonoBehaviour
{
    [Header("User Interface")]
    public Button addHours;
    public Button removeHours;
    public TextMeshProUGUI hoursToSpend;
    public TextMeshProUGUI question;
    public Button startActivity;
    public Button cancelActivity;

    [Header("Internals")]
    [SerializeField] private string activityName;
    [SerializeField] private int maxHoursToInvest;
    [SerializeField] private int hoursToInvest;
    [SerializeField] private ClockManager clockManager;
    [SerializeField] private ActivitySystem activitySystem;

    void Start()
    {
        // find ClockManager system
        try
        {
            clockManager = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<ClockManager>();
        }
        catch (Exception e)
        {
            Debug.LogError($"{name} could not locate WorldManager and the ClockManager script!: " + e);
        }

        // find activity system
        try
        {
            activitySystem = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<ActivitySystem>();
        }
        catch (Exception e)
        {
            Debug.LogError($"{name} could not locate WorldManager and the ActivitySystem script!: " + e);
        }
    }

    public void RefreshScreen()
    {
        RetrieveInformation();
        UpdateConfirmationScreenInformation();
    }


    private void RetrieveInformation()
    {
        maxHoursToInvest = clockManager.GetNumberOfHoursInDayLeft();
        hoursToInvest = 0;
    }

    private void UpdateConfirmationScreenInformation()
    {
        hoursToSpend.text = hoursToInvest.ToString();
        question.text = $"Do you want to spend {hoursToSpend.text} hours on XXXXX?";
    }

    public void AddOneHour()
    {
        hoursToInvest++;
        if (hoursToInvest > maxHoursToInvest)
        {
            hoursToInvest = maxHoursToInvest;
        }
        hoursToSpend.text = hoursToInvest.ToString();
        question.text = $"Do you want to spend {hoursToSpend.text} hours on XXXXX?";
    }

    public void RemoveOneHour()
    {
        hoursToInvest--;
        if(hoursToInvest < 0)
        {
            hoursToInvest = 0;
        }
        hoursToSpend.text = hoursToInvest.ToString();
        question.text = $"Do you want to spend {hoursToSpend.text} hours on XXXXX?";
    }

    public void StartActivity()
    {

    }

    public void CancelActivity()
    {
        activitySystem.CancelInitialization();
    }
}
