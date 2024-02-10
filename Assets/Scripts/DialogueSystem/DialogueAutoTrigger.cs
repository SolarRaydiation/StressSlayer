using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueAutoTrigger : MonoBehaviour
{
    public TextAsset inkJSON;
    public float startDelayTime;
    public float endDelayTime;

    [Header("Autostart with Timer")]
    public bool autostartWithTimer;
    
    [Header("Autostart with Signal")]
    public bool waitForASignal;
    public bool startDialogue;

    [Header("End of Interaction Event")]
    public UnityEvent interactAction;

    private bool gateBool;
    private DialogueManager dm;
    private bool hasDialogueStarted = false;

    void Start()
    {
        gateBool = true;
        dm = DialogueManager.GetDialogueManagerInstance();
        if(autostartWithTimer)
        {
            StartCoroutine(AutoStartDialogue());
            return;
        }
    }

    IEnumerator AutoStartDialogue()
    {
        yield return new WaitForSeconds(startDelayTime);
        dm.EnterDialogueMode(inkJSON);
    }

    private void Update()
    {
        // wait for signal to start dialogue
        if (waitForASignal && startDialogue && !hasDialogueStarted)
        {
            hasDialogueStarted = true;
            StartCoroutine(AutoStartDialogue());
            return;
        }

        // wait for dialogue to complete
        if (dm.IsDialogueComplete() && gateBool)
        {
            gateBool = false;
            StartCoroutine(ExecuteNextEvent());
        }
    }

    IEnumerator ExecuteNextEvent()
    {
        yield return new WaitForSeconds(endDelayTime);
        interactAction.Invoke();
    }
}
