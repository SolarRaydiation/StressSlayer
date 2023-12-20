using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;

    
    [SerializeField] private FixedJoystick joystick;

    [Header("Private Variables")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;

    [Header("Movement Values")]
    [SerializeField] private float horizontalMovement;
    [SerializeField] private float verticalMovement;

    /* =============================================
     * INITIALIZATION
     * ========================================== */
    private void Awake()
    {
        GetPlayerComponents();
    }

    void Start()
    {
        // get references form outside the object
        GetJoystickFromPlayerControls();
    }

    void GetJoystickFromPlayerControls()
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

    void GetPlayerComponents()
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
     * UPDATE METHODS
     * ========================================== */
    void FixedUpdate()
    {
        GetMovementValues();
        MovePlayerHorizontallyWithPhysics();
        MirrorPlayerDirection();
    }

    void GetMovementValues()
    {
        horizontalMovement = joystick.Horizontal * movementSpeed;
        verticalMovement = joystick.Vertical * movementSpeed;
    }

    /* =============================================
     * MOVEMENT METHODS
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
     * OTHERS
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
