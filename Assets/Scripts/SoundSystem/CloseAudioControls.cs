using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAudioControls : MonoBehaviour
{
    public GameObject audioControls;
    public void DestroyAudioControls()
    {
        SaveSystem.SaveSoundSettings();
        audioControls.SetActive(false);
    }
}
