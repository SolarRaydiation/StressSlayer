using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCombatTutorialScript2 : MonoBehaviour
{
    [SerializeField] private StressManager sm;
    public DialogueAutoTrigger nextDialogue;
    void Start()
    {
        sm = StressManager.GetInstance();
    }
    void Update()
    {
        if(sm.currentStressLevel <= 10)
        {
            nextDialogue.startDialogue = true;
            enabled = false;
        }
    }
}
