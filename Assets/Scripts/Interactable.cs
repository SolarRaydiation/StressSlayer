using System;
using TMPro;
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
    public AudioSource interactableFailSFX;
    public TextMeshPro textMesh;
    public string interactableText;

    [Header("Unity Events")]
    public UnityEvent interactAction;                           // action(s) to execute when interacted with

    [Header("Flags")]
    [SerializeField] private bool IS_IN_RANGE;                   // if player is close enough to interact with object

    [Header("Internals")]
    private Button button;                      // reference to player's interaction button
    private SpriteRenderer sr;                  // reference to gameObject's SpriteRenderer
    private GameObject interactableBubble;      // prompt to tell player object can be interacted with
    private WarningText uninteractableWarning;  // flash player reason they can't interact if object is not
    [SerializeField] private Color originalColor;

    #region Initialization
    void Awake()
    {
        GetPlayerActionButton();
        SetSelfObjectReferences();
        GetWarningTextScript();
        originalColor = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a);
    }

    private void Start()
    {
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

        if (textMesh != null)
        {
            textMesh.SetText("");
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

    #endregion

    #region Execute Action

    public void StartInteraction()
    {
        if (interactable && IS_IN_RANGE)
        {
            interactAction.Invoke();
            if(interactableClickedSFX != null)
            {
                interactableClickedSFX.Play();
            }
        } else
        {
            uninteractableWarning.FlashWarningForNSeconds(3.0f, notInteractableMessage);
            if (interactableFailSFX != null)
            {
                interactableFailSFX.Play();
            }
        }
    }

    #endregion

    #region Player Detection Methods Methods

    // mainly for signalling to the player object can be interacted with (or not)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // AttachButtonListener();

        if (interactable)
        {
            IS_IN_RANGE = true;
            sr.color = interactableColor;

            if(textMesh != null)
            {
                textMesh.SetText(interactableText);
            }
            
        } else
        {
            if (textMesh != null)
            {
                textMesh.SetText("");
            }
            sr.color = notInteractableColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (textMesh != null)
        {
            textMesh.SetText("");
        }
        IS_IN_RANGE = false;
        sr.color = originalColor;
        // RemoveButtonListener();
    }

    #endregion

    #region Interaction Bubble Methods

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

    #endregion

    #region Interactability Methods

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

    #endregion

    #region Deprecated Button Listeners Methods 
    private void AttachButtonListener()
    {
        try
        {
            button.onClick.AddListener(StartInteraction);
        }
        catch (Exception e)
        {
        }
    }

    private void RemoveButtonListener()
    {
        try
        {
            button.onClick.RemoveAllListeners();
        }
        catch (Exception e)
        {
        }
    }
    #endregion

    #region Other Methods

    public Color GetOriginalColor()
    {
        return originalColor;
    }

    #endregion
}
