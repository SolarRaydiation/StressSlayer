using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndDaySystem : MonoBehaviour
{
    [Header("Canvases To Hide")]
    public GameObject[] canvases;

    public Animator animator;
    public AudioSource newDayChime;
    public LevelLoader levelLoader;
    public Button startButton;
    public Button endButton;

    public void InitializeEndDayMode()
    {
        HideCanvases();
        gameObject.SetActive(true);
    }

    public void StartEndDayMode()
    {
        Debug.Log("Starting end day mode");
        startButton.enabled = false;
        endButton.enabled = false;
        StartCoroutine(EndDay());
        
    }

    IEnumerator EndDay()
    {
        ClockManager cm = ClockManager.GetInstance();
        cm.MoveDayForward();
        Debug.Log("Saving player data");
        newDayChime.Play();
        yield return new WaitForSeconds(5.0f);
        levelLoader.LoadNextLevel("BedroomScene");
    }

    public void ShortcircuitEndDayMode()
    {
        ShowCanvases();
        gameObject.SetActive(false);
    }

    #region Canvases Methods
    private void HideCanvases()
    {
        foreach (GameObject c in canvases)
        {
            c.SetActive(false);
        }
    }

    private void ShowCanvases()
    {
        foreach (GameObject c in canvases)
        {
            c.SetActive(true);
        }
    }

    #endregion
}
