using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    #region Initialization
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

    #endregion

    #region Core Methods

    /// <summary>
    /// Called when the players intends to start an activity.
    /// </summary>
    /// <param name="a">The activity to be started.</param>
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

    /// <summary>
    /// Not to be confused with ExitActivity(). This is called only when the
    /// activity in question has been completed.
    /// </summary>
    public void ExitActivityMode()
    {
        StartCoroutine(ResetAvailabilityOfInteractables());
        confirmationScreen.SetActive(false);
        Animator animator = fadeoutScreen.GetComponent<Animator>();
        animator.SetTrigger("FadeOut");
        activityScreen.SetActive(false);
        ShowCanvases();
        activityInstance = null;
    }

    IEnumerator ResetAvailabilityOfInteractables()
    {
        yield return null;
        GameObject[] interactableList = GameObject.FindGameObjectsWithTag("Interactable");
        foreach (GameObject interactable in interactableList)
        {
            InteractableController ic = interactable.GetComponent<InteractableController>();
            if(ic != null)
            {
                ic.RecheckAvailability();
            }
        }

        GameObject[] activityInteractableList = GameObject.FindGameObjectsWithTag("ActivityInteractable");
        foreach (GameObject interactable in activityInteractableList)
        {
            InteractableController ic = interactable.GetComponent<InteractableController>();
            if (ic != null)
            {
                ic.RecheckAvailability();
            }
        }
    }

    #endregion


    #region UI Support
    private void GetReferences()
    {
        Transform userPromptTransform = activityScreen.transform.Find("UserPrompt").transform;
        userPromptText = userPromptText.GetComponent<TextMeshProUGUI>();

        Transform insertBenefitsTransform = activityScreen.transform.Find("ActivityBenefits").transform;
        insertBenefitsText = insertBenefitsTransform.GetComponent<TextMeshProUGUI>();
    }

    private void FillInUserPromptText()
    {
        userPromptText.SetText($"Do you want to spend {hoursToInvest} hours {activityInstance.adverb}?");
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

    /// <summary>
    /// For telling the activity system to go ahead with the activity in question.
    /// </summary>
    public void StartActivity()
    {
        Animator animator = fadeoutScreen.GetComponent<Animator>();
        activityInstance.ReduceStressLevel(hoursToInvest);
        activityInstance.IncreasePlayerStat(hoursToInvest);
        clockManager.MoveForwardTimeByNHours(hoursToInvest);
        StartCoroutine(OpenConfirmationScreen(animator));
    }

    /// <summary>
    /// For short circuiting the activity system before the player carries out.
    /// the activity.
    /// </summary>
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


    #endregion

    #region Canvases Control

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

    #endregion
}