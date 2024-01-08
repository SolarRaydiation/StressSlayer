using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public bool interactable;                                   // set object to be interacted with
    // public Sprite interactionBubbleSprite;                   // deprecated.
    // public bool showInteractionBubble;                       // deprecated.
    public Color interactableColor;                             // #65E565. Tells user that object can be interacted with
    public Color notInteractableColor;                          // #EA3030. Tells user that object CANNOT be interacted with
    public string notInteractableMessage;                       // message to send if not interactable
    public AudioSource interactableClickedSFX;

    [Header("Unity Events")]
    public UnityEvent interactAction;                           // action(s) to execute when interacted with

    [Header("Flags")]
    [SerializeField] private bool IS_IN_RANGE;                   // if player is close enough to interact with object

    [Header("Internals")]
    [SerializeField] private Button button;                      // reference to player's interaction button
    [SerializeField] private SpriteRenderer sr;                  // reference to gameObject's SpriteRenderer
    [SerializeField] private GameObject interactableBubble;      // prompt to tell player object can be interacted with
    [SerializeField] private WarningText uninteractableWarning;  // flash player reason they can't interact if object is not
                                                                 // interactable

    /* =============================================
     * Initialization Methods
     * ========================================== */
    void Awake()
    {
        if(interactable)
        {
            /* if(showInteractionBubble)
            {
                CreateInteractableBubble();
            }*/
        }
    }

    private void Start()
    {
        GetPlayerActionButton();
        SetSelfObjectReferences();
        GetWarningTextScript();

        // check if a BoxCollider2D is present or not. If not, then create one and set it as trigger
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            if (!collider.isTrigger)
            {
                collider.isTrigger = true;
            }
        }
        else
        {
            BoxCollider2D newCollider = gameObject.AddComponent<BoxCollider2D>();
            newCollider.isTrigger = true;
        }
    }

    private void GetPlayerActionButton()
    {
        try
        {
            GameObject playerControls = GameObject.Find("PlayerControls");
            Transform actionButtonTransform = playerControls.transform.Find("ActionButton");
            GameObject actionButton = actionButtonTransform.gameObject;
            button = actionButton.GetComponent<Button>();
        }
        catch (Exception e)
        {
            Debug.LogError("Could not get Button component reference!: " + e);
        }
    }

    private void SetSelfObjectReferences()
    {
        try
        {
            sr = gameObject.GetComponent<SpriteRenderer>();
        } catch (Exception e)
        {
            Debug.LogError($"Could not get {name}'s SpriteRenderer component!: " + e);
        }
    }

    private void GetWarningTextScript()
    {
        try
        {
            uninteractableWarning = GameObject.Find("OverworldScreen").transform.Find("InteractableWarning").GetComponent<WarningText>();
        } catch (Exception e)
        {
            Debug.LogError("Could not get up WarningText script!: " + e);
        }
    }

    /* =============================================
     * Action to Execute When Interacted With
     * ========================================== */

    void StartInteraction()
    {
        if (interactable && IS_IN_RANGE)
        {
            Debug.Log($"{name} interacted with! Executing commands.");
            interactAction.Invoke();
            if(interactableClickedSFX != null)
            {
                interactableClickedSFX.Play();
            }
        } else
        {
            uninteractableWarning.FlashWarningForNSeconds(3.0f, notInteractableMessage);
        }
    }

    /* =============================================
     * Player Detection Functions
     * ========================================== */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AttachButtonListener();

        if (interactable)
        {
            IS_IN_RANGE = true;
            sr.color = interactableColor;
        } else
        {
            sr.color = notInteractableColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IS_IN_RANGE = false;
        sr.color = Color.white;
        RemoveButtonListener();
    }

    /* =============================================
     * Interaction Bubble Methods
     * ========================================== */

    /*
    private void CreateInteractableBubble()
    {
        interactableBubble = new GameObject("InteractableBubble");
        interactableBubble.transform.parent = transform;
        SpriteRenderer imageComponent = interactableBubble.AddComponent<SpriteRenderer>();
        imageComponent.sprite = interactionBubbleSprite;
        interactableBubble.transform.localPosition = new Vector2(0f, 1f);
    }

    private void DestoryInteractableBubble()
    {
        GameObject.Destroy(interactableBubble);
        interactableBubble = null;
    }

    public void ShowInteractableBubble(bool b)
    {
        showInteractionBubble = b;
    }*/

    /* =============================================
     * Set Interactability Methods
     * ========================================== */

    public void SetAsInteractable()
    {
        interactable = true;
        /* if(interactableBubble == null && showInteractionBubble)
        {
            CreateInteractableBubble();
        } */
    }

    public void SetAsUninteractable()
    {
        interactable = false;
        /*if(interactableBubble != null)
        {
            DestoryInteractableBubble();
        }*/
        sr.color = Color.white;
    }

    /* =============================================
     * Button Listeners
     * ========================================== */
    private void AttachButtonListener()
    {
        try
        {
            button.onClick.AddListener(StartInteraction);
            Debug.Log($"{name} attached a button listener to {button.gameObject.name}.");
        }
        catch (Exception e)
        {
            Debug.LogError($"{name} could not attach button listener to {button.gameObject.name}!: " + e);
        }
    }

    private void RemoveButtonListener()
    {
        try
        {
            button.onClick.RemoveAllListeners();
            Debug.Log($"{name} removed a button listener from {button.gameObject.name}.");
        }
        catch (Exception e)
        {
            Debug.LogError($"{name} could not remove button listener from {button.gameObject.name}!: " + e);
        }
    }
}
