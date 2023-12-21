using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressManager : MonoBehaviour
{
    public Slider stressMeter;

    [Header("Public Variables")]
    public int MAX_STRESS;

    [Header("Internals")]
    [SerializeField] private int currentStressLevel;

    private void Start()
    {
        stressMeter.maxValue = MAX_STRESS;
        currentStressLevel = 0;
    }

    public void AddStress(int amount)
    {
        currentStressLevel = currentStressLevel + amount;
        if (currentStressLevel > MAX_STRESS)
        {
            currentStressLevel = MAX_STRESS;
        }

        stressMeter.value = currentStressLevel;
    }

    public void ReduceStress(int amount)
    {
        currentStressLevel = currentStressLevel - amount;
        if (currentStressLevel < 0)
        {
            currentStressLevel = 0;
        }
        stressMeter.value = currentStressLevel;
    }
}
