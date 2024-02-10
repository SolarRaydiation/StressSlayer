using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCombatTutorialScript2 : MonoBehaviour
{
    [SerializeField] private StressManager sm;
    public DialogueAutoTrigger nextDialogue;
    public bool startTracking = false;
    void Start()
    {
        sm = StressManager.GetInstance();
    }
    void Update()
    {
        if(!startTracking)
        {
            return;
        }

        if(sm.currentStressLevel <= 10)
        {
            Debug.Log("Second part of tutorial completed!");
            if (nextDialogue != null)
            {
                nextDialogue.startDialogue = true;
                nextDialogue.enabled = true;
            }
            enabled = false;
        }
    }

    public void SetStartTrackingToTrue()
    {
        startTracking = true;
    }
}
