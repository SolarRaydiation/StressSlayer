using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueScript : MonoBehaviour
{
    [Header("Public Variables")]
    public TextMeshProUGUI message;
    public TextMeshProUGUI speakerName;
    public string filename;

    [Header("Internals")]
    [SerializeField] private string[] lines;
    [SerializeField] private string[] speakers;
    [SerializeField] private float textSpeed;

    private int index;
    private const string FOLDER_PATH = "DialogueFiles/";
    private const string EXTENSION = ".txt";

    /* =============================================
     * Dialogue System Core Components
     * ========================================== */

    void Start()
    {
        message.text = string.Empty;
        speakerName.text = string.Empty;
        LoadDialouge(filename);
        StartDialouge();
    }

    // Update is called once per frame
    void Update()
    {
        // will need to replace this with Input.Touch later ~ 12/20/2023 ray
        if (Input.GetMouseButtonDown(0))
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
    public void StartDialouge()
    {
        index = 0;
        message.text = string.Empty;
        speakerName.text = string.Empty;
        StartCoroutine(TypeLine());
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
            gameObject.SetActive(false);
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
