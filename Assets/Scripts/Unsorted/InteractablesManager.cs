using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesManager : MonoBehaviour
{
    public static InteractablesManager instance;

    private void Awake()
    {
        instance = this; 
    }

    public void Start()
    {
        StartCoroutine(ResetAvailabilityOfInteractables());
    }

    public void RecheckAvailability()
    {
        StartCoroutine(ResetAvailabilityOfInteractables());
    }

    IEnumerator ResetAvailabilityOfInteractables()
    {
        yield return new WaitForSeconds(1.0f);
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
}
