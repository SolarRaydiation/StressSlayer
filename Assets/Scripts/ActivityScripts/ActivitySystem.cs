using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActivitySystem : MonoBehaviour
{
    private static ActivitySystem instance;

    [Header("Canvases To Hide")]
    public GameObject[] canvases;

    [Header("Activity Screen")]
    public GameObject activityScreen;
    public TextMeshProUGUI userPromptText;
    public TextMeshProUGUI insertBenefitsText;
    public GameObject fadeoutScreen;
    public GameObject confirmationScreen;
    private Activity activityInstance;
    private int hoursToInvest;

    private ClockManager clockManager;

    public void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("There is more than one instance of ActivitySystem in the Scene!");
        }
        instance = this;
    }

    private void Start()
    {
        clockManager = ClockManager.GetInstance();
    }

    public static ActivitySystem GetInstance()
    {
        return instance;
    }

    /* =============================================
     * Core Methods
     * ========================================== */

    public void EnterActivityMode(Activity a)
    {
        activityInstance = a;
        hoursToInvest = 1;
        activityScreen.SetActive(true);
        //GetReferences();
        FillInUserPromptText();
        FillInBenefitsText();
        HideCanvases();
    }

    public void ExitActivityMode()
    {
        confirmationScreen.SetActive(false);
        Animator animator = fadeoutScreen.GetComponent<Animator>();
        animator.SetTrigger("FadeOut");
        activityScreen.SetActive(false);
        ShowCanvases();
        activityInstance = null;
    }

    private void GetReferences()
    {
        Transform userPromptTransform = activityScreen.transform.Find("UserPrompt").transform;
        userPromptText = userPromptText.GetComponent<TextMeshProUGUI>();

        Transform insertBenefitsTransform = activityScreen.transform.Find("ActivityBenefits").transform;
        insertBenefitsText = insertBenefitsTransform.GetComponent<TextMeshProUGUI>();
    }

    private void FillInUserPromptText()
    {
        userPromptText.SetText($"Do you want to spend {hoursToInvest} {activityInstance.adverb}?");
    }

    private void FillInBenefitsText()
    {
        if(activityInstance.willReduceStress && activityInstance.willIncreaseStat)
        {
            insertBenefitsText.SetText
            (
                string.Concat
                (
                    activityInstance.DisplayStressReductionBenefits(hoursToInvest),
                    "\n",
                    activityInstance.DisplayStatIncreaseBenefits(hoursToInvest)
                )
           );
            return;
        }

        if (activityInstance.willReduceStress)
        {
            insertBenefitsText.SetText(activityInstance.DisplayStressReductionBenefits(hoursToInvest));
            return;
        }

        if(activityInstance.willIncreaseStat)
        {
            insertBenefitsText.SetText(activityInstance.DisplayStatIncreaseBenefits(hoursToInvest));
            return;
        }
        
    }

    /* =============================================
     * Button Support Methods
     * ========================================== */

    public void AddByOneHour()
    {
        if(hoursToInvest + 1 > clockManager.GetTimeLeftInDay())
        {
            return; // do nothing intentionally
        } else
        {
            hoursToInvest++;
            FillInUserPromptText();
            FillInBenefitsText();
        }
    }

    public void ReduceByOneHour()
    {
        if (hoursToInvest - 1 <= 0)
        {
            return; // do nothing intentionally
        }
        else
        {
            hoursToInvest--;
            FillInUserPromptText();
            FillInBenefitsText();
        }
    }

    public void StartActivity()
    {
        Animator animator = fadeoutScreen.GetComponent<Animator>();
        activityInstance.ReduceStressLevel(hoursToInvest);
        activityInstance.IncreasePlayerStat(hoursToInvest);
        clockManager.MoveForwardTimeByNHours(hoursToInvest);
        StartCoroutine(OpenConfirmationScreen(animator));
    }

    public void ExitActivity()
    {
        activityScreen.SetActive(false);
        ShowCanvases();
        activityInstance = null;
    }

    IEnumerator OpenConfirmationScreen(Animator animator)
    {
        animator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1.5f);
        confirmationScreen.SetActive(true);
    }

    /* =============================================
     * Show/Hide Canvases Methods
     * ========================================== */

    private void HideCanvases()
    {
        foreach (GameObject c in canvases)
        {
            c.SetActive(false);
        }
    }

    private void ShowCanvases()
    {
        foreach (GameObject c in canvases)
        {
            c.SetActive(true);
        }
    }
}
;