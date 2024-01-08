using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonHelper : MonoBehaviour
{
    public Color canInteractColor;     // 65E565
    [SerializeField] private Image actionButtonImage;

    void Start()
    {
        try
        {
            GameObject playerControls = GameObject.Find("PlayerControls");
            Transform actionButtonTransform = playerControls.transform.Find("ActionButton");
            GameObject actionButtonObject = actionButtonTransform.gameObject;
            actionButtonImage = actionButtonObject.GetComponent<Image>();
        }
        catch (Exception e)
        {
            Debug.LogError("Could not get SpriteRenderer component reference!: " + e);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Interactable") || other.CompareTag("ActivityInteractable"))
        {
            actionButtonImage.color = canInteractColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        actionButtonImage.color = Color.white;
    }
}
