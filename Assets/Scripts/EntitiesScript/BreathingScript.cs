using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// This script handles all aspects of the breathing system.
/// </summary>
public class BreathingScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float longPressDuration = 1.0f;
    [SerializeField] private bool isButtonPressed = false;
    [SerializeField] private float pressStartTime = 0f;
    [SerializeField] private bool isButtonEnabled = true;
    [SerializeField] private Button breatheButton;

    [Header("Internals")]
    public Slider breathSlider;
    public StressManager stressManager;
    private const float breathDelta = 0.2f;


    /* ===================================
     * Initialization Functions
    =================================== */
    private void Awake()
    {
        breatheButton = GetComponent<Button>();
    }

    private void Start()
    {
        GetExternalScriptReferences();
    }

    private void GetExternalScriptReferences()
    {
        try
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            stressManager = player.GetComponent<StressManager>();
        } catch (Exception e) 
        {
            Debug.LogError("Could not get StressManager from Player!");
            Debug.LogException(e);
        }
        /**
         
         * 
        try
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            fatigueScript = player.GetComponent<FatigueScript>();
        }
        catch (Exception e)
        {
            Debug.LogError("Could not get StressScript from Player!");
            Debug.LogException(e);
        }*/
    }

    /* ===================================
     * Update Functions
    =================================== */
    void FixedUpdate()
    {
        if (isButtonPressed && (Time.time - pressStartTime >= longPressDuration))
        {
            changeBreathLevel(breathDelta);
        }
        else
        {
            changeBreathLevel(-breathDelta);
        }

        checkMeter();
    }

    /* ===================================
     * Core Functions
    =================================== */
    private void changeBreathLevel(float amount)
    {
        breathSlider.value += amount;
    }

    private void checkMeter()
    {
        if (breathSlider.value == breathSlider.maxValue)
        {
            isButtonPressed = false;
            isButtonEnabled = false;
            breatheButton.interactable = false;
        }

        if (!isButtonEnabled && (breathSlider.value == 0.00f))
        {
            enableButton();
        }
    }

    private void calculateStressReduction()
    {
        float temp = breathSlider.value;
        Debug.Log("Stress reduced is " + temp);
        stressManager.ReduceStress((int)temp);
        //fatigueScript.ChangeFatigueLevel(-10);
    }

    /* ===================================
     * Functions for Checking Input
    =================================== */

    // past ray, how did you get these functions to work properly?

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isButtonEnabled)
        {
            isButtonPressed = true;
            pressStartTime = Time.time;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        disableButton();
    }

    private void disableButton()
    {
        isButtonPressed = false;
        isButtonEnabled = false;
        breatheButton.interactable = false;
        calculateStressReduction();
    }

    private void enableButton()
    {
        isButtonEnabled = true;
        breatheButton.interactable = true;
    }

    


    /* ==================================
     * FUNCTIONS FOR REDUCING STRESS
    =================================== */

    
}