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
     * Setter Methods
     * ========================================== */
    /// <summary>
    /// First checks if the reduction in difficulty will not casue the next level's
    /// difficulty to drop below the minimum. If yes, the difficulty will be reduced.
    /// The calling function may also proceed with their execution. Otherwise, false.
    /// </summary>
    /// <param name="difficultyReduction"></param>
    /// <returns></returns>
    public bool ReduceNextLevelDifficulty(float difficultyReduction)
    {
        if((nextLevelDifficulty - difficultyReduction) >= MINIMUM_LEVEL_DIFFICULTY)
        {
            nextLevelDifficulty = nextLevelDifficulty - difficultyReduction;
            UpdateNextLevelDifficultyTextUI();
            return true;
        } else
        {
            return false;
        }
    }

    public void DebugReduceNextLevelDifficulty(float difficultyReduction)
    {
        Debug.Log(ReduceNextLevelDifficulty(difficultyReduction));
    }

    public float GetNextLevelDifficult()
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
