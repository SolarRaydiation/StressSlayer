using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Controls")]
    public string bgmToPlay;

    [Header("Manager Variables")]
    public Sound[] bgm, sfx;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Internals")]
    public float currentBGMVolume;
    public float currentSFXVolume;
    public bool isBGMMuted;
    public bool isSFXMuted;

    #region Initialization Methods

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of AudioManager in scene!");
        }
        instance = this;
    }

    public static AudioManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        try
        {
            if(SaveSystem.CheckIfSoundSettingsExist())
            {
                Debug.Log("Found existing Sound Settings...");
                LoadSoundSettings();
            } else
            {
                Debug.Log("Did not find existing Sound Settings...");
                SaveSystem.CreateNewSoundSettings();
                LoadSoundSettings();
            }
        } catch (Exception e)
        {
            Debug.LogWarning($"Something went wrong while getting sound settings. Defaulting to back up... {e}");
            ChangeBGMVolume(100f);
            ChangeSFXVolume(100f);
            SetInitialToggle(false, false);
        }

        PlayMusic(bgmToPlay);
    }

    private void LoadSoundSettings()
    {
        SoundSettings soundSettings = SaveSystem.LoadSoundSettings();
        currentBGMVolume = soundSettings.BGMvolume;
        currentSFXVolume = soundSettings.SFXvolume;
        ChangeBGMVolume(currentBGMVolume);
        ChangeSFXVolume(currentSFXVolume);
        SetInitialToggle(soundSettings.isBGMMuted, soundSettings.isSFXMuted);
    }

    private void SetInitialToggle(bool isBGMMuted, bool isSFXMuted)
    {
        musicSource.mute = isBGMMuted;
        sfxSource.mute = isSFXMuted;
    }
    #endregion

    #region Play Sounds
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(bgm, x => x.name == name);
        if(s == null)
        {
            Debug.LogWarning($"BGM '{name}' not found!");
            return;
        } else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfx, x => x.name == name);
        if (s == null)
        {
            Debug.LogWarning($"SFX {name} not found!");
            return;
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    #endregion

    #region Player Controls

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
        isSFXMuted = sfxSource.mute;

        // change player SFX
        try
        {
            AudioSource walkSFX = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
            walkSFX.mute = sfxSource.mute;
        }
        catch (Exception e)
        {
            Debug.LogWarning("Could not toggle player WalkSFX volume!" + e);
        }
    }

    public void ToggleBGM()
    {
        musicSource.mute = !musicSource.mute;
        isBGMMuted = musicSource.mute;
    }

    public void ChangeSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        currentSFXVolume = volume;

        // note: because of difficulties in the implementation, its easier to just do it
        // this way

        // change player SFX
        try
        {
            AudioSource walkSFX = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
            walkSFX.volume = volume;
        } catch (Exception e)
        {
            Debug.LogWarning("Could not change player WalkSFX volume!" + e);
        }
    }
    public void ChangeBGMVolume(float volume)
    {
        musicSource.volume = volume;
        currentBGMVolume = volume;
    }

    #endregion

}
