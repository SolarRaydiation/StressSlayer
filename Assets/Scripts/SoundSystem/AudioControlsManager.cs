using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControlsManager : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    public Image bgmIcon;
    public Image sfxIcon;

    [SerializeField] private bool isBGMOff = false;
    [SerializeField] private bool isSFXOff = false;

    private void Awake()
    {
        SoundSettings soundSettings = SaveSystem.LoadSoundSettings();
        
        if (soundSettings.isSFXMuted)
        {
            Image temp = sfxIcon.gameObject.GetComponent<Image>();
            Color tempcolor = temp.color;
            tempcolor.a = 0.5f;
            temp.color = tempcolor;
        }
        else
        {
            Image temp = sfxIcon.gameObject.GetComponent<Image>();
            Color tempcolor = temp.color;
            tempcolor.a = 255f;
            temp.color = tempcolor;
        }

        if(soundSettings.isBGMMuted)
        {
            Image temp = bgmIcon.gameObject.GetComponent<Image>();
            Color tempcolor = temp.color;
            tempcolor.a = 0.5f;
            temp.color = tempcolor;
        }
        else
        {
            Image temp = bgmIcon.gameObject.GetComponent<Image>();
            Color tempcolor = temp.color;
            tempcolor.a = 1f;
            temp.color = tempcolor;
        }

        musicSlider.value = soundSettings.BGMvolume;
        sfxSlider.value = soundSettings.SFXvolume;
    }

    public void ToggleMusic()
    {
        AudioManager.instance.ToggleBGM();
        isBGMOff = !isBGMOff;
        if(isBGMOff)
        {
            Image temp = bgmIcon.gameObject.GetComponent<Image>();
            Color tempcolor = temp.color;
            tempcolor.a = 0.5f;
            temp.color = tempcolor;
        } else
        {
            Image temp = bgmIcon.gameObject.GetComponent<Image>();
            Color tempcolor = temp.color;
            tempcolor.a = 1f;
            temp.color = tempcolor;
        }
    }

    public void ToggleSFX()
    {
        AudioManager.instance.ToggleSFX();
        isSFXOff = !isSFXOff;
        if (isSFXOff)
        {
            Image temp = sfxIcon.gameObject.GetComponent<Image>();
            Color tempcolor = temp.color;
            tempcolor.a = 0.5f;
            temp.color = tempcolor;
        }
        else
        {
            Image temp = sfxIcon.gameObject.GetComponent<Image>();
            Color tempcolor = temp.color;
            tempcolor.a = 255f;
            temp.color = tempcolor;
        }
    }

    public void ChangeBGMVolume()
    {
        AudioManager.instance.ChangeBGMVolume(musicSlider.value);
    }

    public void ChangeSFXVolume()
    {
        AudioManager.instance.ChangeSFXVolume(sfxSlider.value);
    }
}
