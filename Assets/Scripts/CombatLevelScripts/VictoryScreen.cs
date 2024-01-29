using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public GameObject[] gameObjectsToHide;
    private CanvasGroup canvasGroup;
    private Animator animator;
    
    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        animator = gameObject.GetComponent<Animator>();
    }

    public void OpenVictoryScreen()
    {
        canvasGroup.blocksRaycasts = true;
        DisableGameObjects();
        animator.SetTrigger("AnimateScreen");
        StartCoroutine(DelayGameFreeze());
    }

    public void ContinueToNextLevel()
    {
        Debug.Log("Continue to next overworld day");
    }

    private void DisableGameObjects()
    {
        foreach (GameObject gameObject in gameObjectsToHide)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator DelayGameFreeze()
    {
        yield return new WaitForSeconds(5f);
        Time.timeScale = 0f;
    }
}
