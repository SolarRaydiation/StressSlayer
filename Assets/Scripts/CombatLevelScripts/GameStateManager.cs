using UnityEngine;
using System;
using System.Collections;

public class GameStateManager : MonoBehaviour
{
    #region Variables
    public static GameStateManager instance;
    
    public DefeatScreen ds;                     // script reference to manage the defeat screen
    public VictoryScreen vs;                    // script reference for managing the victory screen

    [Header("Game State Internals")]
    [SerializeField] private bool hasGameStarted;
    [SerializeField] private bool hasGameEnded;

    [Header("Script References")]
    private TimerCountdown timerCountdown;
    private VictoryFlag victoryFlag;
    private StressManager sm;

    [Header("Support for Stress Defeat Condition")]
    public GameObject overstressedWarning;            // game object for the warning
    private WarningText warningTextScript;            // text display of the warning
    public bool startCountdown;
    public int countdownDuration;                     // how long before a stress-defeat game over is forced
    private int countdownRemaining;                   // how long the countdown left
    [SerializeField] private bool isSubroutineRunning;
    [SerializeField] private bool shouldRestart;

    enum DefeatType
    { PlayerDeath, NoTimeLeft, Overstress }

    #endregion

    #region Initialization
    public void Awake()
    {
        if(instance != null)
        {
            Debug.Log("There is more than one instance of GameStateManager!");
        }
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        try
        {
            timerCountdown = gameObject.GetComponent<TimerCountdown>();
        } catch (Exception e)
        { 
            Debug.LogError($"Could not find TimerCountdown script: {e.Message}");
        }

        try
        {
            victoryFlag = GameObject.Find("VictoryFlag").GetComponent<VictoryFlag>();
        }
        catch (Exception e)
        {
            Debug.LogError($"Could not VictoryFlag script :{e.Message}");
        }

        try
        {
            sm = StressManager.GetInstance();
            warningTextScript = overstressedWarning.GetComponent<WarningText>();
        }
        catch (Exception e)
        {
            Debug.LogError($"Could not StressManager script or get WarningTextScript :{e.Message}");
        }
    }

    #endregion
    private void FixedUpdate()
    {
        hasGameStarted = timerCountdown.IsCountDownFinished();
        if (hasGameStarted && !hasGameEnded)
        {
            CheckIfPlayerHasWon();
            CheckIfPlayerHasLost();
        }
    }

    #region Win/Loss Checker Functions

    private void CheckIfPlayerHasWon()
    {
        if (victoryFlag.HasPlayerReachedLevelEnd())
        {
            PlayerVictory();
        }
    }

    private void CheckIfPlayerHasLost()
    {
        // check if player is dead.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            PlayerDefeat(DefeatType.PlayerDeath);
        }

        // check if player is stressed
        if(sm.GetCurrentStressValue() >= 80 && !isSubroutineRunning)
        {
            countdownRemaining = countdownDuration;
            shouldRestart = false;
            isSubroutineRunning = true;
            StartCoroutine(StartCountdown());
        } else if (sm.GetCurrentStressValue() < 80)
        {
            shouldRestart = true;
        }

        // check if player has not finished the level in time
        if (timerCountdown.IsTimeAtZero())
        {
            PlayerDefeat(DefeatType.NoTimeLeft);
        }
    }

    #endregion

    #region Victory/Defeat Methods

    private void PlayerVictory()
    {
        hasGameEnded = true;
        Debug.Log("Player has won");
        vs.OpenVictoryScreen();
        DisableControls();
    }

    private void PlayerDefeat(DefeatType defeatType)
    {
        Debug.Log("Player is dead!");
        hasGameEnded = true;
        ds.OpenDefeatScreen(GetDefeatString(defeatType));
        DisableControls();
    }

    private void DisableControls()
    {
        try
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PlayerMovement.GetInstance().DisablePlayerMovement();
                StressManager.GetInstance().StopStressManager();
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }

        try
        {
            GameObject playerControls = GameObject.Find("PlayerControls");
            if (playerControls != null)
            {
                playerControls.SetActive(false);
            }
        } catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    #endregion

    #region Supporting Methods
    private static string GetDefeatString(DefeatType defeatType)
    {
        switch(defeatType)
        {
            case (DefeatType.PlayerDeath):
                return "You are dead!";
            case (DefeatType.NoTimeLeft):
                return "You've run out of time!";
            case (DefeatType.Overstress):
                return "You've gained too much stress!";
            default:
                return "ERROR IN GETTING PLAYER DEFEAT TYPE";
        }
    }

    #endregion

    #region Stress Coroutine

    IEnumerator StartCountdown()
    {
        while (countdownRemaining > 0)
        {
            warningTextScript.StartWarning("You are too stressed! Destress now!");
            Debug.Log("Countdown: " + countdownRemaining);
            yield return new WaitForSeconds(1f); // Wait for 1 second
            countdownRemaining--;

            // Check if we should restart the countdown
            if (shouldRestart)
            {
                shouldRestart = false;
                isSubroutineRunning = false;
                warningTextScript.CloseWarning();
                Debug.Log("Countdown restarted.");
                yield break; // Exit the coroutine
            }
        }

        warningTextScript.CloseWarning();
        PlayerDefeat(DefeatType.Overstress);
    }

    #endregion
}
