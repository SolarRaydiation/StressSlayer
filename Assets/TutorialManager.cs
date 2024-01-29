using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public Animator animator;

    [Header("SFX")]
    public AudioSource ping;

    [Header("Tutorial UI")]
    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;

    [Header("Tutorial Checklist")]
    public bool tutorialComplete;
    public bool finishedFirstStep;                      // Move character using the joystick
    public bool finishedSecondStep;                     // Interact objects using the interact button
    public bool finishedThirdStep;                      // Talk to brother
    public bool finishedFourthStep;                     // Leave the house
    public bool finishedFifthStep;                      // Keep going right and go to school
    public bool finishedSixthStep;                      // Talk to teacher (cutscene right here baby)

    [Header("Player Controls")]
    public GameObject actionButton;

    [Header("Various World Objects")]
    public GameObject frontdoor;

    // others
    PlayerMovement pm;

    /*
     * Steps in Tutorial
     * 1) Move character using the joystick
     * 2) Leave the room (Interact with objects by using the interact button)
     * 3) Lock the door and prevent player from leaving room until they talk to family member
     * 4) Go to school and talk to the teacher
     * 5) Some cutscene dialogue bit where the teacher talks about stress
     * 6) Fight club
     * 7) End talk with teacher
     * 8) Player is freeee!
     */

    #region Initialization
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Tutorial Manager in the Scene!");
        }
        instance = this;
    }
    public static TutorialManager GetInstance()
    {
        return instance;
    }
    private void Start()
    {
        if(tutorialComplete)
        {
            tutorialPanel.SetActive(false);
            enabled = false;
        } else
        {
            pm = PlayerMovement.GetInstance();
            tutorialPanel.SetActive(true);

            if (!finishedFirstStep)
            {
               SetupForFirstStep();
               return;
            }

            if(!finishedThirdStep)
            {
                SetupForThirdStep();
                return;
            }

            if (!finishedFifthStep)
            {
                SetupForFifthStep();
                return;
            }

            if(!finishedSixthStep)
            {
                SetupForSixthStep();
                return;
            }
        }
    }
    #endregion

    private void Update()
    {
        if(!finishedFirstStep)
        {
            if(pm.IsPlayerMoving())
            {
                finishedFirstStep = true;
                SetupForSecondStep();
            }
            return;
        }

        if(!finishedSecondStep)
        {
            // set to true by SecondStepSignaller.cs
            return;
        }

        if (!finishedThirdStep)
        {
            // set to true by ThirdStepSignaller.cs
            return;
        }

        if (!finishedFourthStep)
        {
            // nothing
            return;
        }

        if (!finishedFifthStep)
        {
            // nothing
            return;
        }
        return;
    }

    #region Setup Functions
    public void SetupForFirstStep()
    {
        // show tutorial text
        Debug.Log("Setting up for first step!");
        tutorialText.SetText("Move the character using the joystick.");
        
        // hide action button
        CanvasGroup cg = actionButton.GetComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.interactable = false;
    }

    public void SetupForSecondStep()
    {
        // show tutorial text
        Debug.Log("Setting up for second step!");
        StartCoroutine(MoveToNextStepVisualCues("Go to the door and press the interaction button on the bottom right."));

        // show action button
        CanvasGroup cg = actionButton.GetComponent<CanvasGroup>();
        cg.alpha = 1;
        cg.interactable = true;
    }

    public void SetupForThirdStep()
    {
        //// show tutorial text
        Debug.Log("Setting up for third step!");
        tutorialText.SetText("Go talk to your brother by the couch with the interaction button.");
    }

    public void SetupForFourthStep()
    {
        // show tutorial text
        Debug.Log("Setting up for fourth step!");
        Interactable i = frontdoor.GetComponent<Interactable>();
        i.enabled = true;
        i.interactable = true;
        StartCoroutine(MoveToNextStepVisualCues("Exit the house by going leaving by the door."));
    }

    public void SetupForFifthStep()
    {
        Debug.Log("Setting up for fifth step!");
        StartCoroutine(MoveToNextStepVisualCues("Keep going right and go to school."));
    }

    public void SetupForSixthStep()
    {
        Debug.Log("Setting up for sxith step!");
        StartCoroutine(MoveToNextStepVisualCues("Talk to the teacher."));
    }

    #endregion

    #region Visual Cues
    IEnumerator MoveToNextStepVisualCues(string nextInstruction)
    {
        animator.SetTrigger("BlackToGreen");
        yield return new WaitForSeconds(1.0f);
        tutorialText.SetText(nextInstruction);
        ping.Play();
        animator.SetTrigger("GreenToBlack"); 
    }

    #endregion
}
