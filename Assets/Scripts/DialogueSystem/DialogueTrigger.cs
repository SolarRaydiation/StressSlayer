using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public TextAsset inkJSON;

    public void StartDialogue()
    {
        DialogueManager dm = DialogueManager.GetDialogueManagerInstance();
        PlayerMovement.GetInstance().DisablePlayerMovement();
        dm.EnterDialogueMode(inkJSON);
    }
}
