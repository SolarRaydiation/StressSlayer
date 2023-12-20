using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [Header("Flags")]
    [SerializeField] private bool IS_INTERACTABLE;
    [SerializeField] private bool IS_IN_RANGE;

    [Header("Player Controls Reference")]
    [SerializeField] private Button button;

    [Header("Interaction Bubble")]
    [SerializeField] private GameObject interactableBubble;
    public Sprite interactionbubble;

    [Header("Interaction Events")]
    public UnityEvent interactAction;

    /* =============================================
     * INITIALIZATION FUNCTIONS
     * ========================================== */
    void Awake()
    {
        if(IS_INTERACTABLE)
        {
            CreateInteractableBubble();
        }
    }

    private void Start()
    {
        GetPlayerActionButton();
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
        IS_IN_RANGE = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IS_IN_RANGE = false;
    }

    /* =============================================
     * INTERACTABLE BUBBLE FUNCTIONS
     * ========================================== */
    private void CreateInteractableBubble()
    {
        interactableBubble = new GameObject("InteractableBubble");
        interactableBubble.transform.parent = transform;
        SpriteRenderer imageComponent = interactableBubble.AddComponent<SpriteRenderer>();
        imageComponent.sprite = interactionbubble;
        interactableBubble.transform.localPosition = new Vector2(0f, 1f);
    }

    private void DestoryInteractableBubble()
    {
        GameObject.Destroy(interactableBubble);
        interactableBubble = null;
    }

    public void SampleMethod()
    {
        Debug.Log("Interacted!");
    }
}
