using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public AudioClip walkSound;
    public AudioSource soundSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlaySound();
        }
    }

    private void PlaySound()
    {
        if (walkSound != null && soundSource != null)
        {
            // Play the sound effect
            soundSource.clip = walkSound;
            soundSource.Play();
        }
    }
}
