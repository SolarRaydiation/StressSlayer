using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatTutorialScript : MonoBehaviour
{
    public TextAsset conversationOne;
    public TextAsset conversationTwo;
    public TextAsset conversationThree;
    public DialogueTrigger trigger;

    [Header("Waiting For Conversation to End")]
    public bool isFirstDialogueComplete = false;

    [Header("Events For Each Step")]
    public UnityEvent firstTutorialStep;

    [Header("Internals")]
    public DialogueManager manager;

    void Start()
    {
        StartCoroutine(StartConversationOne());
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.IsDialogueComplete() && !isFirstDialogueComplete)
        { 
            isFirstDialogueComplete = true;
            Debug.Log("Starting first part of tutorial...");
            firstTutorialStep.Invoke();
        }
    }
    
    IEnumerator StartConversationOne()
    {
        trigger.inkJSON = conversationOne;
        yield return new WaitForSeconds(1.5f);
        trigger.StartDialogue();
    }
}
