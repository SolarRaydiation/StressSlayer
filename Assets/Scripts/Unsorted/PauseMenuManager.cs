using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    // if you are looking for the pause game option, it can be found
    // in the pause button ~ ray 01-19-2023

    public void ResumeGame()
    {
        PlaySFX();
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    public void RestartLevel()
    {
        PlaySFX();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        PlaySFX();
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        PlaySFX();
        SceneManager.LoadScene(0);
    }

    private void PlaySFX()
    {
        try
        {
            AudioManager.instance.PlaySFX("TapSFX");
        }
        catch
        {

        }
    }
}
