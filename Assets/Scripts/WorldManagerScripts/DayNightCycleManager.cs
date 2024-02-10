using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering; // used to access the volume component
using UnityEngine.Rendering.Universal;

public class DayNightCycleManager : MonoBehaviour
{
    public static DayNightCycleManager instance;
    private ClockManager clockManager;
    // increment 0.166666

    // night sky color: 2e4482

    [Header("Day Night Cycle Controls")]
    private Volume ppv; // this is the post processing volume
    public int hourToStartShifting = 15;
    public float weightShift = 0.16667f;
    public int hourToActivateLightSources = 18;

    [Header("Internals")]
    private GameObject[] lightSources;

    #region Initialization
    private void Start()
    {
        clockManager = ClockManager.GetInstance();
        ppv = GameObject.FindGameObjectWithTag("GlobalVolume").GetComponent<Volume>();
        if(ppv != null)
        {
            Debug.Log("PPV found!");
        } else
        {
            Debug.LogWarning("PPV not found! DayNightCycleManager will be activated.");
            enabled = false;
            return;
        }

        if(instance != null)
        {
            Debug.LogWarning("More than one instance of DayNightCycleManager present in scene!");
        }
        instance = this;

        GetAllLightSources();
        PaintDayNightCycle();
    }

    private void GetAllLightSources()
    {
        lightSources = GameObject.FindGameObjectsWithTag("Light");
    }

    #endregion

    #region Day Night Cycle Control

    private void ChangePPVWeight()
    {
        // if morning, set PPV weight to zero
        if(clockManager.currentDaySection == ClockManager.DaySection.Morning)
        {
            ppv.weight = 0;
            return;
        }

        if(clockManager.currentHour > hourToStartShifting)
        {
            ppv.weight = weightShift * (clockManager.currentHour - hourToStartShifting);
        } else
        {
            return;
        }
    }

    private void ActivateLightSources()
    {
        if(clockManager.currentHour >= hourToActivateLightSources)
        {
            Debug.Log("Activating light sources");
            foreach(GameObject lightSource in lightSources)
            {
                Light2D light2d = lightSource.GetComponent<Light2D>();
                light2d.enabled = true;
            }
            return;
        } else
        {
            foreach (GameObject lightSource in lightSources)
            {
                Light2D light2d = lightSource.GetComponent<Light2D>();
                light2d.enabled = false;
            }
            Debug.Log("Not yet actvating light sources");
        }
    }

    // Implnote: because currentHour is retrieved from the save file at the same time
    // the ActivateLightSources() is called, ActivateLightSources() will not activate
    // at night. Hence, ActivateLightsAsync() provides a short delay to allow the currenthour
    // to be loaded first before ActivateLightSources() is called; allowing the function to 
    // work properly

    IEnumerator ActivateLightsAsync()
    {
        yield return new WaitForSeconds(1.0f);
        ActivateLightSources();
    }

    public void PaintDayNightCycle()
    {
        StartCoroutine(DelayDayNightCyclePaint());
    }

    IEnumerator DelayDayNightCyclePaint()
    {
        yield return new WaitForSeconds(3.0f);
        try
        {
            ChangePPVWeight();
            StartCoroutine(ActivateLightsAsync());
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Could not paint light sources!: {e}");
        }
    }

    #endregion

    #region Debug Methods
    public void RepaintDayNightScene()
    {
        StartCoroutine(ResetAvailabilityOfInteractables());
        clockManager.MoveForwardTimeByNHours(1);
        ChangePPVWeight();
        ActivateLightSources();
    }

    IEnumerator ResetAvailabilityOfInteractables()
    {
        yield return null;
        GameObject[] interactableList = GameObject.FindGameObjectsWithTag("Interactable");
        foreach (GameObject interactable in interactableList)
        {
            InteractableController ic = interactable.GetComponent<InteractableController>();
            if (ic != null)
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
}
