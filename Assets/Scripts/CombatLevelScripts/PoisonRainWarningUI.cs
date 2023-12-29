using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PoisonRainWarningUI : MonoBehaviour
{
    private TextMeshProUGUI text;
    private CanvasGroup cs;

    private void Start()
    {
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
}
