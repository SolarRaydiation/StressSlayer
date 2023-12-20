using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerControls;
    public GameObject dialogueScreen;

    public void StartDialogue(string filename)
    {
        playerControls.SetActive(false);
        dialogueScreen.SetActive(true);
        DialogueScript ds = dialogueScreen.GetComponent<DialogueScript>();
        ds.StartDialouge(filename);
    }
}
