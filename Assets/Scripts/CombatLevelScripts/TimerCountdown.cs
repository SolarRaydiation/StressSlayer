using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerCountdown : MonoBehaviour
{
    #region Variables
    public static TimerCountdown instance;

    [Header("Countdown and Timer Values")]
    public float secondsBeforeGameStart;        // how many seconds before the level starts in seconds
    public float levelDurationInSeconds;        // the max number of seconds the game will last ins econds
    public TextMeshProUGUI timerText;           // UI for displaying how much time is left
    [SerializeField] private float timeRemaining;
    [SerializeField] private PlayerMovement pm;

    public float TimeRemaining
    {
        get { return timeRemaining; }
    }

    [Header("GameObjects To Reveal After Countdown")]
    public GameObject[] gameObjectsToShow;      // an array for holding all the GameObjects that are hidden
                                                // initially via setting alpha to zero but must be shown later

    [Header("Flags")]
    private bool isTimeAtZero;
    private bool isCountdownFinished;
    #endregion

    #region Initialization
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of TimerCountdown in scene!");
        }
        instance = this;

        
    }

    public static TimerCountdown GetInstance()
    {
        return instance;
    }
    
    void Start()
    {
        foreach (GameObject gameObject in gameObjectsToShow)
        {
            CanvasGroup cg = gameObject.GetComponent<CanvasGroup>();
            cg.alpha = 0;
            cg.interactable = false;
        }

        isCountdownFinished = false;
        isTimeAtZero = false;
        StartCountdown();
        pm = PlayerMovement.instance;
        pm.DisablePlayerMovement();
    }
    #endregion

    #region Timer Methods
    public void StartTimer()
    {
        timeRemaining = levelDurationInSeconds;
        StartCoroutine(Timer());
    }

    public bool IsTimeAtZero()
    {
        return isTimeAtZero;
    }

    IEnumerator Timer()
    {
        while (timeRemaining > 0f)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining--;
            string timeString = FormatTime(timeRemaining);
            timerText.SetText(timeString);
        }
        
        HideTimerUI();
        isTimeAtZero = true;
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        return string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }

    private void HideTimerUI()
    {
        timerText.gameObject.transform.parent.gameObject.SetActive(false);
    }

    #endregion

    #region Countdown Methods
    public void StartCountdown()
    {
        timeRemaining = secondsBeforeGameStart;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while (timeRemaining > 0f)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining--;
            timerText.SetText(timeRemaining.ToString());
        }

        ShowGameObjects();
        isCountdownFinished = true;
        StartTimer();
        pm.EnablePlayerMovement();
    }

    private void ShowGameObjects()
    {
        foreach (GameObject gameObject in gameObjectsToShow)
        {
            CanvasGroup cg = gameObject.GetComponent<CanvasGroup>();
            cg.alpha = 1;
            cg.interactable = true;
        }
    }

    public bool IsCountDownFinished()
    {
        return isCountdownFinished;
    }

    #endregion
}
