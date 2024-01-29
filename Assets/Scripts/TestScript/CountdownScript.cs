using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownScript : MonoBehaviour
{
    public int countdownDuration;
    [SerializeField] private int countdownRemaining;

    public bool startCountdown;
    public bool shouldRestart = false;

    [SerializeField] private bool isSubroutineRunning;

    void Start()
    {
        if(startCountdown)
        {
            countdownRemaining = countdownDuration;
            StartCoroutine(StartCountdown());
        }
    }

    void Update()
    {
        if (startCountdown && !isSubroutineRunning)
        {
            isSubroutineRunning = true;
            countdownRemaining = countdownDuration;
            StartCoroutine(StartCountdown());
        }
    }

    IEnumerator StartCountdown()
    {
        while (countdownRemaining > 0)
        {
            Debug.Log("Countdown: " + countdownRemaining);
            yield return new WaitForSeconds(1f); // Wait for 1 second
            countdownRemaining--;

            // Check if we should restart the countdown
            if (shouldRestart)
            {
                shouldRestart = false;
                isSubroutineRunning = false;
                countdownRemaining = 15;
                Debug.Log("Countdown restarted.");
                yield break; // Exit the coroutine
            }
        }

        Debug.Log("Countdown finished! Ready to proceed.");
    }
}
