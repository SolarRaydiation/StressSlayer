using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueAutoTrigger : MonoBehaviour
{
    public TextAsset inkJSON;
    public float startWaitTime;
    public float endWaitTime;
    public UnityEvent interactAction;
    private bool gateBool;
    private DialogueManager dm;

    void Start()
    {
        gateBool = true;
        dm = DialogueManager.GetDialogueManagerInstance();
        StartCoroutine(AutoStartDialogue());
    }

    IEnumerator AutoStartDialogue()
    {
        yield return new WaitForSeconds(startWaitTime);
        dm.EnterDialogueMode(inkJSON);
    }

    private void Update()
    {
        if (dm.IsDialogueComplete() && gateBool)
        {
            gateBool = false;
            StartCoroutine(ExecuteNextEvent());
        }
    }

    IEnumerator ExecuteNextEvent()
    {
        yield return new WaitForSeconds(endWaitTime);
        interactAction.Invoke();

    }
}
