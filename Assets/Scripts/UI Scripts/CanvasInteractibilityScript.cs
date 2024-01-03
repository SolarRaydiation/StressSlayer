using System;
using UnityEngine;

public class CanvasInteractibilityScript : MonoBehaviour
{
    private CanvasGroup cg;

    private void Start()
    {
        try
        {
            cg = gameObject.GetComponent<CanvasGroup>();
        } catch (Exception e)
        {
            Debug.LogWarning($"Could not get CanvasGroup component of {gameObject.name}!: {e}");
        }
    }
    public void HideCanvas(bool blockRaycasts)
    {
        cg.interactable = false;
        cg.alpha = 0;
        cg.blocksRaycasts = blockRaycasts;
    }

    public void ShowCanvas(bool blockRaycasts)
    {
        cg.interactable = true;
        cg.alpha = 1;
        cg.blocksRaycasts = blockRaycasts;
    }
}

