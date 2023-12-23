using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    // just copy paste [SerializeField] if you need to view the internals

    [Header("Flags")]
    private bool IS_INTERACTABLE; // if object can be interacted with 
    private bool IS_IN_RANGE; // if player is close enough to interact with object

    [Header("Player Controls Reference")]
    private Button button;

    [Header("Object References")]
    private SpriteRenderer sr;
    private Material defaultMaterial;

    [Header("Interaction Bubble")]
    private GameObject interactableBubble;

    [Header("Public Variables")]
    public bool initiallyInteractable;
    public Material visualPromptMaterial;
    public Sprite interactionBubbleSprite;

    [Header("Unity Events")]
    public UnityEvent interactAction;

    /* =============================================
     * INITIALIZATION FUNCTIONS
     * ========================================== */
    void Awake()
    {
        if(initiallyInteractable)
        {
            IS_INTERACTABLE = true;
            CreateInteractableBubble();
        } else
        {
            IS_INTERACTABLE = false;
        }
    }

    private void Start()
    {
        GetPlayerActionButton();
        SetSelfObjectReferences();
    }

    private void GetPlayerActionButton()
    {
        try
        {
            GameObject playerControls = GameObject.Find("PlayerControls");
            Transform actionButtonTransform = playerControls.transform.Find("ActionButton");
            GameObject actionButton = actionButtonTransform.gameObject;
            button = actionButton.GetComponent<Button>();
            SetActionButtonListener();
        }
        catch (Exception e)
        {
            Debug.LogError("Could not get Button component reference!: " + e);
        }
    }

    private void SetActionButtonListener()
    {
        try
        {
            button.onClick.AddListener(StartInteraction);
        } catch (Exception e)
        {
            Debug.LogError("Could not set up ButtonListener!: " + e);
        }
    }

    private void SetSelfObjectReferences()
    {
        try
        {
            sr = gameObject.GetComponent<SpriteRenderer>();
            defaultMaterial = sr.material;
        } catch (Exception e)
        {
            Debug.LogError($"Could not get {name}'s SpriteRenderer component!: " + e);
        }
    }

    /* =============================================
     * INTERACTION FUNCTIONS
     * ========================================== */

    void StartInteraction()
    {
        if (IS_INTERACTABLE && IS_IN_RANGE)
        {
            interactAction.Invoke();
        }
    }

    /* =============================================
     * PLAYER DETECTION FUNCTIONS
     * ========================================== */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(IS_INTERACTABLE)
        {
            IS_IN_RANGE = true;
            sr.material = visualPromptMaterial;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IS_INTERACTABLE)
        {
            IS_IN_RANGE = false;
            sr.material = defaultMaterial;
        }
    }

    /* =============================================
     * INTERACTABLE BUBBLE FUNCTIONS
     * ========================================== */
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

    /* =============================================
     * SET INTERACTIBILITY FUNCTIONS
     * ========================================== */

    public void SetAsInteractable()
    {
        IS_INTERACTABLE = true;
        if(interactableBubble == null)
        {
            CreateInteractableBubble();
        }
    }

    public void SetAsUninteractable()
    {
        IS_INTERACTABLE = false;
        DestoryInteractableBubble();
        sr.material = defaultMaterial;
    }
}
