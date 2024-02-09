using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressManager : MonoBehaviour
{
    public static StressManager instance;
    public Slider stressMeter;

    [Header("Public Variables")]
    public int MAX_STRESS;
    public bool isCombatLevel;

    [Header("Combat Level Properties")]
    public int stressIncreasePerTick;
    public float tickDuration;

    [Header("Internals")]
    public int currentStressLevel;
    public StressLevel stressState;
    [SerializeField] private float stressBonus;
    [SerializeField] private PlayerStatsScript pss;

    public enum StressLevel
    {
        Green, Yellow, Orange, Red, Black
    }

    private void Awake()
    {
        if(instance !=  null)
        {
            Debug.LogWarning("There is more than one instance of StressManager in existence!");
        }
        instance = this;
    }

    public static StressManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        SaveFileManager sfm = SaveFileManager.GetInstance();
        PlayerData saveFile = sfm.saveFile;
        stressMeter.maxValue = MAX_STRESS;
        stressMeter.value = saveFile.currentStressLevel;
        currentStressLevel = (int) saveFile.currentStressLevel;
        if (isCombatLevel)
        {
            pss = gameObject.GetComponent<PlayerStatsScript>();
            StartCoroutine(IncreaseStressGradually(tickDuration));
            StartCoroutine(IncreaseStressBasedOnEnemyCount());
        }
    }

    /* =============================================
     * Core Functions
     * ========================================== */

    public void AddStress(int amount)
    {
        currentStressLevel = currentStressLevel + amount;
        if (currentStressLevel > MAX_STRESS)
        {
            currentStressLevel = MAX_STRESS;
        }

        stressMeter.value = currentStressLevel;
        UpdateCurrentStressLevel();
    }

    public void ReduceStress(int amount)
    {
        currentStressLevel = currentStressLevel - amount;
        if (currentStressLevel < 0)
        {
            currentStressLevel = 0;
        }
        stressMeter.value = currentStressLevel;
        UpdateCurrentStressLevel();
    }

    private void UpdateCurrentStressLevel()
    {
        stressState = GetCurrentStressLevel(currentStressLevel);
        stressBonus = GetStressBonus(stressState);
    }

    /* =============================================
     * Apply Bonuses to the Player's Stats
     * ========================================== */

    private void ApplyPlayerBonuses()
    {
        pss.UpdateCurrentAttackDamage(pss.GetBaseAttackDamage() + 
            (pss.GetBaseAttackDamage() * stressBonus)
            );
        //Debug.Log($"Current player damage is at {pss.GetCurrentAttackDamage()}");
    }

    /* =============================================
     * Coroutines
     * ========================================== */

    IEnumerator IncreaseStressGradually(float tickDuration)
    {
        yield return new WaitForSeconds(tickDuration);
        AddStress(stressIncreasePerTick);
        UpdateCurrentStressLevel();
        ApplyPlayerBonuses();
        StartCoroutine(IncreaseStressGradually(tickDuration));
    }

    IEnumerator IncreaseStressBasedOnEnemyCount()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        yield return new WaitForSeconds(5.0f);
        AddStress(enemies.Length);
        UpdateCurrentStressLevel();
        ApplyPlayerBonuses();
        StartCoroutine(IncreaseStressBasedOnEnemyCount());
    }

    /* =============================================
     * Static Functions
     * ========================================== */

    protected static float GetStressBonus(StressLevel sl)
    {
        switch (sl)
        {
            case StressLevel.Green:
                return 0f;
            case StressLevel.Yellow:
                return 0.50f;
            case StressLevel.Orange:
                return 0.100f;
            case StressLevel.Red:
                return 0.150f;
            case StressLevel.Black:
                return 2f;
            default:
                Debug.LogWarning($"An unexpected value '{sl.ToString()}' was recieved" +
                    $" while getting Stress Bonus. Default value of 0f has been returned.");
                return 0f;
        }
    }

    protected static StressLevel GetCurrentStressLevel(float currentStressLevel)
    {
        // greenzone
        if(currentStressLevel < 20)
        {
            return StressLevel.Green;
        } else if (currentStressLevel < 40)
        {
            return StressLevel.Yellow;
        } else if (currentStressLevel < 60)
        {
            return StressLevel.Orange;
        } else if (currentStressLevel < 80)
        {
            return StressLevel.Red;
        } else
        {
            return StressLevel.Black;
        }
    }

    public float GetCurrentStressValue()
    {
        return stressMeter.value;
    }
}
