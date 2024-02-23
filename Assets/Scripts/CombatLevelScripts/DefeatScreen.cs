using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatScreen : MonoBehaviour
{
    public TextMeshProUGUI defeatReasonText;
    public GameObject[] gameObjectsToHide;

    private CanvasGroup canvasGroup;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        animator = gameObject.GetComponent<Animator>();
    }

    public void OpenDefeatScreen(string defeatReason)
    {
        defeatReasonText.SetText(defeatReason);

        canvasGroup.blocksRaycasts = true;

        DisableGameObjects();

        animator.SetTrigger("AnimateScreen");
        //StartCoroutine(DelayGameFreeze());
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void ExitLevel()
    {
        Debug.Log("Return to overworld level");
        SceneManager.LoadScene(0);
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
        yield return new WaitForSeconds(1.2f);
        Time.timeScale = 0f;
    }
}
