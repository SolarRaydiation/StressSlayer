using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManagerHelper : MonoBehaviour
{
    public GameObject pauseMenu;
   public void PauseGame()
    {
        AudioManager.instance.PlaySFX("TapSFX");
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
}
