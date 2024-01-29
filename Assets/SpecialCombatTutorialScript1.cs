using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCombatTutorialScript1 : MonoBehaviour
{
    public bool isFirstDialogueComplete = false;
    public GameObject dummy;
    public DialogueAutoTrigger nextDialogue;

    private bool AreThereEnemiesLeft()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length != 0)
        {
            return true;
        }
        return false;
    }

    public void SetIsFirstDialgoueCompleteToTrue()
    {
        isFirstDialogueComplete = true;
        Destroy(dummy);
    }

    private void Update()
    {
        if (!AreThereEnemiesLeft() && isFirstDialogueComplete)
        {
            nextDialogue.startDialogue = true;
            enabled = false;
        }
    }
}
