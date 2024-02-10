using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTutorialInteractableController : MonoBehaviour
{
    void Start()
    {
        TutorialManager tm = TutorialManager.GetInstance();
        if (tm != null)
        {
            if (!tm.tutorialComplete)
            {
                InteractableController interactableController = GetComponent<InteractableController>();
                interactableController.enabled = false;
                Interactable interactable = GetComponent<Interactable>();
                interactable.enabled = false;
            }
            enabled = false;
        }
    }
}
