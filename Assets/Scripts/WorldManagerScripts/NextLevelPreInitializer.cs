using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NextLevelPreInitializer : MonoBehaviour
{
    private const float INTIAL_LEVEL_DIFFICULTY = 1.5f;
    private const float MINIMUM_LEVEL_DIFFICULTY = 0.5f;

    [Header("UI Externals")]
    public TextMeshProUGUI nextLevelDifficultyText;

    [Header("Serialized Private Variables - Difficulty Metric")]
    [SerializeField] private float nextLevelDifficulty;

    
    //private const float DIFFICULTY_REDUCTION_PER_HOUR = 0.25f;

    /* =============================================
     * Initialization
     * ========================================== */
    private void Start()
    {
        nextLevelDifficulty = INTIAL_LEVEL_DIFFICULTY;
        UpdateNextLevelDifficultyTextUI();
    }

    /* =============================================
     * Difficulty update functions
     * ========================================== */

    public void ReduceLevelDifficulty(float difficultyReductionRate)
    {
        nextLevelDifficulty = nextLevelDifficulty - difficultyReductionRate;
        //nextLevelDifficulty = Mathf.Max(nextLevelDifficulty, MINIMUM_LEVEL_DIFFICULTY);
        UpdateNextLevelDifficultyTextUI();
    }

    public bool CanReduceLevelDifficulty(float difficultyReductionRate)
    {
        if((nextLevelDifficulty - difficultyReductionRate) >=  MINIMUM_LEVEL_DIFFICULTY)
        {
            Debug.Log($"nextLevelDifficulty: {nextLevelDifficulty} | difficultyReductionRate: {difficultyReductionRate}");
            Debug.Log($"{nextLevelDifficulty - difficultyReductionRate} >= {MINIMUM_LEVEL_DIFFICULTY}");
            return true;
        } else
        {
            Debug.Log($"{nextLevelDifficulty} {difficultyReductionRate}");
            Debug.Log($"{nextLevelDifficulty - difficultyReductionRate} >= {MINIMUM_LEVEL_DIFFICULTY}");
            return false;
        }
    }

    public float GetNextLevelDifficulty()
    {
        return nextLevelDifficulty;
    }

    // note: there is no setter fucntion for nextLevelDifficulty. No need
    // to increase the difficulty

    /* =============================================
     * UI Update Methods
     * ========================================== */

    public void UpdateNextLevelDifficultyTextUI()
    {
        nextLevelDifficultyText.text = nextLevelDifficulty.ToString();
    }
}
