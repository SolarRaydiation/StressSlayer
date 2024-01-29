using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCombatTutorialStressReducer : MonoBehaviour
{
    [SerializeField] private StressManager sm;
    public int stressReductionPerTick = 1;
    public float secondsPerTick = 3;
    void Start()
    {
        sm = StressManager.GetInstance();
        StartCoroutine(ReduceStressGradually());
    }

    IEnumerator ReduceStressGradually()
    {
        yield return new WaitForSeconds(secondsPerTick);
        sm.ReduceStress(stressReductionPerTick);
        StartCoroutine(ReduceStressGradually());
    }
}
