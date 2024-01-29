using System;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonHelper : MonoBehaviour
{
    public Color canInteractColor;
    [SerializeField] private Image actionButtonImage;
    [SerializeField] private Button actionButton;
    [SerializeField] private Interactable attachedInteractable;

    void Start()
    {
        try
        {
            GameObject playerControls = GameObject.Find("PlayerControls");
            Transform actionButtonTransform = playerControls.transform.Find("ActionButton");
            GameObject actionButtonObject = actionButtonTransform.gameObject;
            actionButton = actionButtonObject.GetComponent<Button>();
            actionButtonImage = actionButtonObject.GetComponent<Image>();
        }
        catch (Exception e)
        {
            Debug.LogError("Could not get SpriteRenderer component reference!: " + e);
        }
    }

    #region Trigger Enter and Exit Methods
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Interactable") || other.CompareTag("ActivityInteractable"))
        {
            if (actionButtonImage != null)
            {
                actionButtonImage.color = canInteractColor;
            }
            
            GetAttachedInteractable(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(actionButtonImage != null)
        {
            actionButtonImage.color = Color.white;
        }
        
        try
        {
            RemoveAttachedInteractable();
        } catch {
            // intentionally left blank
        }
       
    }

    #endregion

    #region attachedInteractable Methods
    private void GetAttachedInteractable(GameObject obj)
    {
        attachedInteractable = obj.GetComponent<Interactable>();
        if( attachedInteractable != null )
        {
            actionButton.onClick.AddListener(attachedInteractable.StartInteraction);
        } else
        {
            Debug.LogWarning($"{obj.name} does not have an Interactable script!");
        }
    }

    private void RemoveAttachedInteractable()
    {
        attachedInteractable = null;
        actionButton.onClick.RemoveAllListeners();
    }

    #endregion
}
