using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    public float baseMovementSpeed;                 
    private float currentMovementSpeed;             
    private FixedJoystick joystick;

    [Header("GameObject Component References")]
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [Header("Movement Values")]
    private float horizontalMovement;
    private float verticalMovement;

    /* =============================================
     * INITIALIZATION
     * ========================================== */
    private void Awake()
    {
        currentMovementSpeed = baseMovementSpeed;
        GetPlayerComponents();
    }

    private void Start()
    {
        // get references form outside the object
        GetJoystickFromPlayerControls();
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
            sr = gameObject.GetComponent<SpriteRenderer>();
        }
        catch (Exception e)
        {
            Debug.LogError("Could not get SpriteRenderer from player!: " + e);
        }
    }

    /* =============================================
     * Update Methods
     * ========================================== */
    void FixedUpdate()
    {
        GetMovementValues();
        MovePlayerHorizontallyWithPhysics();
        MirrorPlayerDirection();
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
    }

    /* =============================================
     * Other Methods
     * ========================================== */

    void MirrorPlayerDirection()
    {
        if(horizontalMovement > 0)
        {
            sr.flipX = false;
        } if(horizontalMovement < 0)
        {
            sr.flipX = true;
        }
    }
}
