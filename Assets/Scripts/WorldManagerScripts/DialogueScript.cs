using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueScript : MonoBehaviour
{
    [Header("Public Variables")]
    public TextMeshProUGUI message;
    public TextMeshProUGUI speakerName;

    [Header("Canvases")]
    // References to Canvases are needed to ensure that all other Cavnases are
    // disabled only dialogueScreen is enabled
    public GameObject dialogueScreen;
    public GameObject playerControls;

    [Header("Text Speed")]
    public float textSpeed;

    [Header("Internals")]
    [SerializeField] private string[] lines;
    [SerializeField] private string[] speakers;
    [SerializeField] private string filename;

    private int index;
    private const string FOLDER_PATH = "DialogueFiles/";

    /* =============================================
     * Initialization
     * ========================================== */
    void Start()
    {
        message.text = string.Empty;
        speakerName.text = string.Empty;
    }

    /* =============================================
     * Start/Stop Dialogue System
     * ========================================== */
    public void StartDialouge(string filename)
    {
        index = 0;
        dialogueScreen.SetActive(true);
        playerControls.SetActive(false);
        message.text = string.Empty;
        speakerName.text = string.Empty;
        LoadDialouge(filename);
        StartCoroutine(TypeLine());
    }
    private void EndDialogue()
    {
        dialogueScreen.SetActive(false);
        playerControls.SetActive(true);
    }

    /* =============================================
     * Dialogue System Core Components
     * ========================================== */
    private void FixedUpdate()
    {
        // will need to replace this with Input.Touch later ~ 12/20/2023 ray
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (message.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                message.text = lines[index];
            }
        }
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            message.text = string.Empty;
            speakerName.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        speakerName.text = speakers[index];
        foreach (char c in lines[index].ToCharArray())
        {
            message.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    /* =============================================
     * Dialouge Loader
     * ========================================== */
    private void LoadDialouge(string filename)
    {
        string filepath = string.Concat(FOLDER_PATH, filename);
        TextAsset textfile = Resources.Load<TextAsset>(filepath);
        if (textfile != null)
        {
            string[] dialogueSet = textfile.text.Split('\n');
            Debug.Log(lines.Length);
            List<string> _speakers = new List<string>();
            List<string> _lines = new List<string>();
            foreach (string entry in dialogueSet)
            {
                string[] parts = entry.Split(':');
                if (parts.Length == 2)
                {
                    _speakers.Add(parts[0]);
                    _lines.Add(parts[1]);
                }
            }

            speakers = _speakers.ToArray();
            lines = _lines.ToArray();
        }
        else
        {
            Debug.LogError("Failed to load the dialogue file: " + filename);
        }
    }

}
