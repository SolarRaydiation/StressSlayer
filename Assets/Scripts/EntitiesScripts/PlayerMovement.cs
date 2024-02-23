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
    public bool canPlayerMove;

    [Header("Internals")]               
    private float currentMovementSpeed;
    private float horizontalMovement;
    private float verticalMovement;
    private const float EPSILON = 0.1f;
    private const float SPRINT_BOOST = 1.3f;
    private FixedJoystick joystick;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;
    //private ParticleSystem sprintTrail;
    

    #region Initialization
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Warning! More than one PlayerMovement in Scene!");
        }
        instance = this;

        canPlayerMove = true;
        currentMovementSpeed = baseMovementSpeed;
        GetPlayerComponents();
    }

    private void Start()
    {
        // get references form outside the object
        GetJoystickFromPlayerControls();
        //if (sprintTrail != null)
        //{
        //    sprintTrail.Stop();
        //}
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

        //try
        //{
        //    sprintTrail = gameObject.transform.Find("SprintTrail").GetComponent<ParticleSystem>();
        //}
        //catch (Exception e)
        //{
        //    Debug.LogError("Could not get sprintTrail from player!: " + e);
        //}
    }

    public static PlayerMovement GetInstance()
    {
        return instance;
    }
    #endregion

    void FixedUpdate()
    {
        if(!canPlayerMove)
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

    #region Movement Values
    private void GetMovementValues()
    {
        if(IsApproximatelyOne(joystick.Horizontal))
        {
            horizontalMovement = joystick.Horizontal * currentMovementSpeed * SPRINT_BOOST;
            verticalMovement = joystick.Vertical * currentMovementSpeed * SPRINT_BOOST;
            //if(sprintTrail != null)
            //{
            //    sprintTrail.Play();
            //}
            
        } else
        {
            horizontalMovement = joystick.Horizontal * currentMovementSpeed;
            verticalMovement = joystick.Vertical * currentMovementSpeed;
            //if (sprintTrail != null)
            //{
            //    sprintTrail.Stop();
            //}
        }
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
    #endregion

    #region Movement Methods
    void MovePlayerWithTranslate()
    {
        transform.Translate(horizontalMovement, verticalMovement, 0);
    }

    void MovePlayerHorizontallyWithPhysics()
    {
        rb.velocity = new Vector2 (horizontalMovement, rb.velocity.y);
        animator.SetFloat("speed", Mathf.Abs(horizontalMovement));
    }
    #endregion

    #region Other Methods
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

    private bool IsApproximatelyOne(float value)
    {
        if(value < 0)
        {
            return Mathf.Abs(-value - 1.0f) < EPSILON;
        }
        return Mathf.Abs(value - 1.0f) < EPSILON;
    }
    #endregion

    #region Enable/Disable PlayerMovement
    public void DisablePlayerMovement()
    {
        canPlayerMove = false;
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
        canPlayerMove = true;
    }
    #endregion
}
