using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WarningText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private CanvasGroup cs;
    [SerializeField] private bool isSubroutineRunning;

    private void Start()
    {
        isSubroutineRunning = false;
        cs = gameObject.GetComponent<CanvasGroup>();
        text = transform.Find("WarningText").GetComponent<TextMeshProUGUI>();
    }

    public void StartWarning(string warning)
    {
        cs.alpha = 1;
        FlashWarning(warning);
    }

    public void FlashWarning(string warning)
    {
        text.SetText(warning);
    }

    public void CloseWarning()
    {
        cs.alpha = 0;
        text.SetText("");
    }

    public void FlashWarningForNSeconds(float duration, string warning)
    {
        if(isSubroutineRunning)
        {
            StopAllCoroutines();
            isSubroutineRunning = false;
            StartCoroutine(FlashWarningBriefly(duration, warning));

        } else
        {
            StartCoroutine(FlashWarningBriefly(duration, warning));
        }
    }

    public IEnumerator FlashWarningBriefly(float duration, string warning)
    {
        Debug.Log($"FlashWarningBriefly recieved: duration: {duration}, warning: {warning}");
        isSubroutineRunning = true;
        StartWarning(warning);
        yield return new WaitForSeconds(duration);
        CloseWarning();
        isSubroutineRunning = false;
    }
}
