using UnityEngine;
using System;

public class GameStateManager : MonoBehaviour
{
    public DefeatScreen ds;                     // script reference to manage the defeat screen
    public VictoryScreen vs;                    // script reference for managing the victory screen

    [Header("Game State Internals")]
    [SerializeField] private bool hasGameStarted;
    [SerializeField] private bool hasGameEnded;

    [Header("Script References")]
    private TimerCountdown timerCountdown;
    private VictoryFlag victoryFlag;

    enum DefeatType
    { PlayerDeath, NoTimeLeft }

    private void Start()
    {
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

    }

    private void FixedUpdate()
    {
        hasGameStarted = timerCountdown.IsCountDownFinished();
        if (hasGameStarted && !hasGameEnded)
        {
            hasGameStarted = true;
            CheckIfPlayerHasWon();
            CheckIfPlayerHasLost();
        }
    }

    /* ===========================================
     * Checker Functions
     * ========================================== */

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

        // check if player has not finished the level in time
        if (timerCountdown.IsTimeAtZero())
        {
            PlayerDefeat(DefeatType.NoTimeLeft);
        }
    }

    /* ===========================================
     * Victory Defeat Functions
     * ========================================== */

    private void PlayerVictory()
    {
        hasGameEnded = true;
        Debug.Log("Player has won");
        vs.OpenVictoryScreen();
    }

    private void PlayerDefeat(DefeatType defeatType)
    {
        hasGameEnded = true;
        ds.OpenDefeatScreen(GetDefeatString(defeatType));
    }

    /* ===========================================
     * Supporting Functions
     * ========================================== */

    private static string GetDefeatString(DefeatType defeatType)
    {
        switch(defeatType)
        {
            case (DefeatType.PlayerDeath):
                return "You are dead!";
            case (DefeatType.NoTimeLeft):
                return "You've run out of time!";
            default:
                return "ERROR IN GETTING PLAYER DEFEAT TYPE";
        }
    }
}
