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
    public TextMeshProUGUI benefitsText;
    public Button startActivity;
    public Button cancelActivity;

    [Header("Internals")]
    [SerializeField] private string activityName;
    [SerializeField] private int maxHoursToInvest;
    [SerializeField] private int hoursToInvest;
    [SerializeField] private ClockManager clockManager;
    [SerializeField] private ActivitySystem activitySystem;
    [SerializeField] private Activity activity;

    /* =============================================
     * Core Functions
     * ========================================== */

    public void RefreshScreen(Activity a)
    {
        activity = a;
        SetScriptReferences();
        RetrieveInformation();
        UpdateScreen();
    }

    private void SetScriptReferences()
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

    private void RetrieveInformation()
    {
        maxHoursToInvest = clockManager.GetNumberOfHoursInDayLeft();
        hoursToInvest = 1;
    }

    private void UpdateScreen()
    {
        hoursToSpend.text = hoursToInvest.ToString();
        question.text = $"Do you want to spend {hoursToSpend.text} hours on {activity.activityName}?";
        benefitsText.text = "";
        if (activity.hasStatIncrease)
        {
            Debug.Log($"hasStatIncrease is set to true");
            benefitsText.text = activity.CalculateStatIncreaseBenefits(hoursToInvest) + "\n";
        }

        if (activity.hasStressReduction)
        {
            Debug.Log($"hasStressReduction is set to true");
            String.Concat(benefitsText.text, activity.CalculateStressReduction(hoursToInvest));
        }
    }

    /* =============================================
     * Support Methods
     * ========================================== */

    public void AddOneHour()
    {
        hoursToInvest++;
        if (hoursToInvest > maxHoursToInvest)
        {
            hoursToInvest = maxHoursToInvest;
        }
        UpdateScreen();
    }

    public void RemoveOneHour()
    {
        hoursToInvest--;
        if(hoursToInvest < 1)
        {
            hoursToInvest = 1;
        }
        UpdateScreen();
    }

    public void StartActivity()
    {
        activity.ExecuteActivity(hoursToInvest);
    }

    public void CancelActivity()
    {
        activitySystem.CancelInitialization();
    }
}
