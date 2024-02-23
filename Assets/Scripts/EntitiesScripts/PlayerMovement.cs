using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public float baseMovementSpeed;
    public AudioSource walkSound;
    public bool isInDialogueMode;

    [Header("Internals")]               
    private float currentMovementSpeed;
    private FixedJoystick joystick;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float horizontalMovement;
    private float verticalMovement;
    private Animator animator;

    #region Initialization
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Warning! More than one PlayerMovement in Scene!");
        }
        instance = this;

        isInDialogueMode = false;
        currentMovementSpeed = baseMovementSpeed;
        GetPlayerComponents();
    }

    private void Start()
    {
        // get references form outside the object
        GetJoystickFromPlayerControls();
        LoadData();
    }

    private void LoadData()
    {
        SaveFileManager sfm = SaveFileManager.GetInstance();
        PlayerData saveFile = sfm.saveFile;
        if (saveFile != null)
        {
            baseMovementSpeed = saveFile.baseMovementSpeed;
        }
        else
        {
            baseMovementSpeed = 12;
            Debug.LogError($"No save file found. PlayerMovement will default to fall-back.");
        }
    }
    
    private void GetJoystickFromPlayerControls()
    {
        try
        {
            GameObject playerControls = GameObject.Find("PlayerControls");
            Transform fixedJoystickTransform = playerControls.transform.Find("Fixed Joystick");
            GameObject fixedJoystick = fixedJoystickTransform.gameObject;
            joystick = fixedJoystick.GetComponent<FixedJoystick>();
        }
        catch (Exception e)
        {
            Debug.LogError("Could not get FixedJoystick reference!: " + e);
        }
    }

    private void GetPlayerComponents()
    {
        try
        {
            rb = gameObject.GetComponent<Rigidbody2D>();

        } catch (Exception e)
        {
            Debug.LogError("Could not get Rigidbody2D from player!: " + e);
        }

        try
        {
            animator = gameObject.GetComponent<Animator>();
        }
        catch (Exception e)
        {
            Debug.LogError("Could not get Animator from player!: " + e);
        }
    }

    public static PlayerMovement GetInstance()
    {
        return instance;
    }
    #endregion

    /* =============================================
     * Update Methods
     * ========================================== */
    void FixedUpdate()
    {
        if(isInDialogueMode)
        {
            return;
        }

        GetMovementValues();

        if (rb != null)
        {
            MovePlayerHorizontallyWithPhysics();
        }
        else
        {
            MovePlayerWithTranslate();
        }
       
        MirrorPlayerDirection();

        if (Mathf.Abs(horizontalMovement) > 0)
        {
            walkSound.enabled = true;
        }
        else
        {
            walkSound.enabled = false;
        }
    }

    /* =============================================
     * Movement Values Methods
     * ========================================== */

    private void GetMovementValues()
    {
        horizontalMovement = joystick.Horizontal * currentMovementSpeed;
        verticalMovement = joystick.Vertical * currentMovementSpeed;
    }

    public float GetCurrentMovementSpeed()
    {
        return currentMovementSpeed;
    }

    public void IncreaseCurrentMovementSpeedTemporarily(float newCurrentMovementSpeed, float seconds)
    {
       currentMovementSpeed = newCurrentMovementSpeed;
       StartCoroutine(MovementBonusDuration(seconds));
    }

    IEnumerator MovementBonusDuration(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        currentMovementSpeed = baseMovementSpeed;
    }

    /* =============================================
     * Movement Methods
     * ========================================== */
    void MovePlayerWithTranslate()
    {
        transform.Translate(horizontalMovement, verticalMovement, 0);
    }

    void MovePlayerHorizontallyWithPhysics()
    {
        rb.velocity = new Vector2 (horizontalMovement, rb.velocity.y);
        animator.SetFloat("speed", Mathf.Abs(horizontalMovement));
    }

    /* =============================================
     * Other Methods
     * ========================================== */

    private void MirrorPlayerDirection()
    {
        if(horizontalMovement > 0)
        {
            Vector3 currentScale = transform.localScale;
            currentScale.x = 2;
            //currentScale.x = 1;
            transform.localScale = currentScale;
        } if(horizontalMovement < 0)
        {
            Vector3 currentScale = transform.localScale;
            currentScale.x = -2;
            //currentScale.x = -1;
            transform.localScale = currentScale;
        }
    }

    public bool IsPlayerMoving()
    {
        if(Mathf.Abs(horizontalMovement) > 0.01)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void DisablePlayerMovement()
    {
        isInDialogueMode = true;
        horizontalMovement = 0;
        verticalMovement = 0;
        rb.velocity = new Vector2(0, 0);
    }

    public void EnablePlayerMovement()
    {
        joystick.SetInputToZero();
        horizontalMovement = 0;
        verticalMovement = 0;
        rb.velocity = new Vector2(0, 0);
        isInDialogueMode = false;
    }
}
