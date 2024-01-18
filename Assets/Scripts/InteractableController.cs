using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : ApperanceControlManager
{
    Interactable interactable;
    SpriteRenderer sr;

    public bool setInactiveIfNotAvailable;
    public string notInteractableMessage;

    protected override void ExecuteOutsideAvailableHours()
    {
        interactable.interactable = false;
        interactable.notInteractableMessage = notInteractableMessage;

        if(setInactiveIfNotAvailable)
        {
            interactable.enabled = false;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
            BoxCollider2D temp = gameObject.GetComponent<BoxCollider2D>();
            temp.enabled = false;
        }
    }

    protected override void ExecuteStartFunctions()
    {
        interactable = gameObject.GetComponent<Interactable>();
        sr = gameObject.GetComponent <SpriteRenderer>();
    }

    protected override void ExecuteWithinAvailableHours()
    {
        interactable.interactable = true;
        interactable.enabled = true;
        sr.color = interactable.GetOriginalColor();
        BoxCollider2D temp = gameObject.GetComponent<BoxCollider2D>();
        temp.enabled = true;
    }

    public void RecheckAvailability()
    {
        CheckAvailability();
    }
}
