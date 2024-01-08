using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChemistryRoomManager : RoomManager
{
    [Header("ChemistryRoomMnager Specific Variables")]
    public ParticleSystem poisonRainParticleSystem;
    public float timeBeforePoisonRainStarts;
    public float poisonRainDuration;
    public WarningText poisonRainManager;
    private float timeRemaining;
    protected override void TriggerStartingAnimation()
    {
        animator.SetTrigger("FadeToChemistry");
    }

    protected override void TriggerEndingAnimation()
    {
        animator.SetTrigger("ChemistryToDefault");
    }

    protected override void ExecuteRoomStartFunctions()
    {
        timeRemaining = timeBeforePoisonRainStarts;
        poisonRainManager.StartWarning($"Poison rain will start in {timeRemaining}");
        StartCoroutine(StartCountdownToPlayPoisonRain());
    }

    protected override void ExecuteRoomEndFunctions()
    {
        StopAllCoroutines();
        StartCoroutine(AsyncDeleteObject(poisonRainParticleSystem.gameObject, 1.0f));
        poisonRainParticleSystem.Stop();
        poisonRainManager.CloseWarning();
    }

    IEnumerator StartCountdownToPlayPoisonRain()
    {
        while (timeRemaining > 0f)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining--;
            poisonRainManager.FlashWarning($"Poison rain will start in {timeRemaining}");
        }

        poisonRainParticleSystem.Play();
        timeRemaining = poisonRainDuration;
        StartCoroutine(StartCountdownToStopPoisonRain());
    }

    IEnumerator StartCountdownToStopPoisonRain()
    {
        poisonRainManager.FlashWarning($"Take cover to protect yourself!");
        while (timeRemaining > 0f)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining--;
        }

        poisonRainParticleSystem.Stop();
        timeRemaining = timeBeforePoisonRainStarts;
        StartCoroutine(StartCountdownToPlayPoisonRain());
    }
}
