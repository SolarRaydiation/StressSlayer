using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionManager : MonoBehaviour
{
    [Header("Controls")]
    public bool enableMoodles;
    public float disabledAlpha = 99f;

    [Header("Moodle UI")]
    public GameObject moodlePanel;
    public GameObject happyMoodle;
    public GameObject sadMoodle;
    public GameObject fearfulMoodle;
    public GameObject angryMoodle;

    private Image happyMoodleImage;
    private Image sadMoodleImage;
    private Image fearfulMoodleImage;
    private Image angryMoodleImage;

    [Header("Public Moodle Flags")]
    public bool happyFlag;
    public bool sadFlag;
    public bool fearfulFlag;
    public bool angryFlag;

    [Header("Subroutine Flags")]
    [SerializeField] protected bool angrySubroutineRunning = false;
    [SerializeField] protected bool fearSubroutineRunning = false;

    [Header("Others")]
    private StressManager sm;
    private TimerCountdown tc;
    private PlayerStatsScript pss;

    // Start is called before the first frame update
    void Start()
    {
        if (enableMoodles)
        {
            happyMoodleImage = happyMoodle.GetComponent<Image>();
            sadMoodleImage = sadMoodle.GetComponent<Image>();
            fearfulMoodleImage = fearfulMoodle.GetComponent<Image>();
            angryMoodleImage = angryMoodle.GetComponent<Image>();

            if (happyMoodleImage == null || sadMoodleImage == null ||
                fearfulMoodleImage == null || angryMoodleImage == null)
            {
                Debug.LogWarning("Image components of moodle UI not found! Disabling moodle system.");
                moodlePanel.SetActive(false);
                enabled = false;
            }

            sm = StressManager.GetInstance();
            tc = TimerCountdown.GetInstance();
            pss = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsScript>();

            DisableMoodleUI(sadMoodleImage);
            DisableMoodleUI(angryMoodleImage);
            DisableMoodleUI(fearfulMoodleImage);
            DisableMoodleUI(happyMoodleImage);
        }
        else
        {
            moodlePanel.SetActive(false);
            enabled = false;
        }
    }

    void Update()
    {
        StressManager.StressLevel stressState = sm.stressState;

        // if player is in a happy state, disable all other states
        if (stressState == StressManager.StressLevel.Green)
        {
            DisableMoodleUI(sadMoodleImage);
            DisableMoodleUI(angryMoodleImage);
            DisableMoodleUI(fearfulMoodleImage);

            sadFlag = false;
            angryFlag = false;
            fearfulFlag = false;

            EnableMoodleUI(happyMoodleImage);
            happyFlag = true;
            return;
        }

        // detect if player is in a sad state
        if(stressState > StressManager.StressLevel.Green)
        {
            DisableMoodleUI(happyMoodleImage);
            happyFlag = false;
            EnableMoodleUI(sadMoodleImage);
            sadFlag = true;
        }

        // detect if player is in an fearful state
        if(tc.TimeRemaining < 60 && stressState != StressManager.StressLevel.Green)
        {
            EnableMoodleUI(fearfulMoodleImage);
            fearfulFlag = true;
            if (!fearSubroutineRunning)
            {
                fearSubroutineRunning = true;
                StartCoroutine(IncreaseStressInducedByFear());
            }

        } else
        {
            DisableMoodleUI(fearfulMoodleImage);
            fearfulFlag = false;
        }

        // detect if player is in an angry state
        bool isHealthHalved = pss.CurrentHealth < (pss.MaxHealth / 2);
        if (isHealthHalved && stressState > StressManager.StressLevel.Green && AreThereEnemiesLeft())
        {
            EnableMoodleUI(angryMoodleImage);
            angryFlag = true;
            if (!angrySubroutineRunning)
            {
                angrySubroutineRunning = true;
                StartCoroutine(IncreaseStressInducedByAnger());
            }
        }
        else
        {
            DisableMoodleUI(angryMoodleImage);
            angryFlag = false;
        }
    }


    private void DisableMoodleUI(Image moodleImage)
    {
        moodleImage.color = new Color(moodleImage.color.r, moodleImage.color.g,
            moodleImage.color.b, 0);
    }

    private void EnableMoodleUI(Image moodleImage)
    {
        moodleImage.color = new Color(moodleImage.color.r, moodleImage.color.g,
            moodleImage.color.b, 255);
    }

    private bool AreThereEnemiesLeft()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length != 0)
        {
            return true;
        }
        return false;
    }

    IEnumerator IncreaseStressInducedByFear()
    {
        Debug.Log("Started to apply fear stress bonuses");
        while(fearfulFlag)
        {
            sm.AddStress(1);
            yield return new WaitForSeconds(3.0f);
        }
        Debug.Log("Stopped applying fear stress bonuses");
        fearSubroutineRunning = false;
        yield break;
    }

    IEnumerator IncreaseStressInducedByAnger()
    {
        Debug.Log("Started to apply anger stress bonuses");
        while (angryFlag)
        {
            sm.AddStress(1);
            yield return new WaitForSeconds(3.0f);
        }
        Debug.Log("Stopped applying anger stress bonuses");
        angrySubroutineRunning = false;
        yield break;
    }
}