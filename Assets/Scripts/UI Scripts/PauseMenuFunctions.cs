using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuFunctions : MonoBehaviour
{
    private CanvasInteractibilityScript cis;

    [Header("Canvases to Hide From Player")]
    public GameObject[] canvasList;

    /* =============================================
     * Initialization Methods
     * ========================================== */

    private void Start()
    {
        try
        {
            cis = GetComponent<CanvasInteractibilityScript>();
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Could not get CanvasInteractibilityScript component of {gameObject.name}!: {e}");
        }
    }

    /* =============================================
     * Pause/Resume Game and Pause Menu Methods
     * ========================================== */

    /// <summary>
    /// Pauses the game by hiding all GameObjects with a Canvas, setting the
    /// timeScale to zero, then showing the pause menu.
    /// </summary>
    public void PauseGame()
    {
        HideCanvases();                     // Hide any UI elements that might get in the way
        Time.timeScale = 0.0f;              // Set Time to zero
        cis.ShowCanvas(true);               // Show Pause Menu
    }

    /// <summary>
    /// A function that iterates through all GameObjects with a Canvas to
    /// hide and disable interactiblity.
    /// </summary>
    private void HideCanvases()
    {
        foreach (GameObject obj in canvasList)
        {
            CanvasInteractibilityScript cis = obj.GetComponent<CanvasInteractibilityScript>();
            cis.HideCanvas(false);
        }
    }

    /// <summary>
    /// Resumes the game by showing all canvases first, setting the timeScale
    /// back to one, then hides the PauseMenu.
    /// </summary>
    public void ResumeGame()
    {
        ShowCanvases();                     // Show the canvases again
        Time.timeScale = 1.0f;              // Set Time to 1 to resume the game again
        cis.HideCanvas(false);              // hide pause menu
    }

    /// <summary>
    /// A function that iterates through all GameObjects with a Canvas to
    /// show them to the player and reenable interactivity
    /// </summary>
    private void ShowCanvases()
    {
        foreach (GameObject obj in canvasList)
        {
            CanvasInteractibilityScript cis = obj.GetComponent<CanvasInteractibilityScript>();
            cis.ShowCanvas(true);
        }
    }

    /// <summary>
    /// Restarts the level
    /// </summary>
    public void RestartGame()
    {
        Debug.Log("Restart the game.");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Returns player to the main menu. Player data is also saved.
    /// </summary>
    public void ReturnToMainMenu()
    {
        Debug.Log("Return to MainMenu function called.");
        //SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Ends the application.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
