using System;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    // stopped at 17:20

    [Header("Canvases To Hide")]
    public GameObject[] canvases;

    [Header("Dialouge Screen")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI speakerNameText;

    [Header("ChoicesUI")]
    [SerializeField] private GameObject[] choices;
    [SerializeField] private TextMeshProUGUI[] choicesText;

    [Header("Internals")]
    private static DialogueManager instance;
    private bool dialogueIsPlaying;
    private Story currentStory;
    private const string SPEAKER_TAG = "speaker";

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is more than one instance of Dialouge Manager in the scene!");
        }
        instance = this;
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }

    private void Start()
    {
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    /* =============================================
     * Static Methods
     * ========================================== */

    public static DialogueManager GetDialogueManagerInstance()
    {
        return instance;
    }

    /* =============================================
     * Core Methods
     * ========================================== */

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        HideCanvases();
        dialoguePanel.SetActive(true);
        ContinueDialogue();
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        ShowCanvases();
        dialogueText.text = "";
        speakerNameText.text = "";
    }

    private void ContinueDialogue()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
            HandleTags(currentStory.currentTags);
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: "
                + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed!: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch(tagKey)
            {
                case SPEAKER_TAG:
                    //Debug.Log("speaker=" + tagValue);
                    DisplaySpeakerName(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag detected but not handled correctly: " + tag);
                    break;
            }
        }
        
    }

    private void DisplaySpeakerName(string speaker)
    {
        speakerNameText.SetText(speaker);
    }

    // for moving the dialogue along
    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }

        // replace with touch anywhere
        if (Input.GetKeyDown(KeyCode.E))
        {
            ContinueDialogue();
        }
    }

    private IEnumerator SelectFirstChoice()
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueDialogue();
    }

    /* =============================================
     * Show/Hide Canvases Methods
     * ========================================== */

    private void HideCanvases()
    {
        foreach(GameObject c in canvases)
        {
            c.SetActive(false);
        }
    }

    private void ShowCanvases()
    {
        foreach (GameObject c in canvases)
        {
            c.SetActive(true);
        }
    }
}
