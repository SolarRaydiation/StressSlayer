using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class HourBasedDialogueSupplier : MonoBehaviour
{
    public DialogueDetails[] dialogueDetails;
    public void Start()
    {
        UpdateDialougeTrigger();
    }

    public void UpdateDialougeTrigger()
    {
        DialogueTrigger dt = gameObject.GetComponent<DialogueTrigger>();
        if(dt != null && dialogueDetails.Length != 0)
        {
            ClockManager cm = ClockManager.GetInstance();
            foreach(DialogueDetails dd in dialogueDetails)
            {
                if(cm.GetCurrentHour() >= dd.timeStart && cm.GetCurrentHour() <= dd.timeEnd)
                {
                    dt.inkJSON = dd.inkJSON;
                    return;
                }
            }
        } else
        {
            Debug.LogWarning($"DialougeTrigger component not available in {gameObject.name} or" +
                $"HourBasedDialogueSupplier component in {gameObject.name} has no DialougeDetails attached!");
        }
    }
}
