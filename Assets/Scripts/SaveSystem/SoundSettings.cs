using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundSettings
{
    public float SFXvolume;
    public float BGMvolume;
    public bool isBGMMuted;
    public bool isSFXMuted;

    public SoundSettings(float _BGMVolume, float _SFXVolume, bool _isBGMMuted, bool _isSFXMuted)
    {
        SFXvolume = _SFXVolume;
        BGMvolume = _BGMVolume;
        isBGMMuted = _isBGMMuted;
        isSFXMuted = _isSFXMuted;
    }

    public SoundSettings()
    {
        SFXvolume = 100f;
        BGMvolume = 100f;
        isBGMMuted = false;
        isSFXMuted = false;
    }
}
