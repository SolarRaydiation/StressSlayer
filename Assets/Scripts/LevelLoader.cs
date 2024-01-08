using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator animator;
    public float transitionTime = 2.0f;

    private string triggerAnimationName = "ChangeScene";
    // Update is called once per frame

    public void LoadNextLevel(string levelName)
    {
        StartCoroutine(LoadLevelWithName(levelName));
    }

    IEnumerator LoadLevelWithIndex(int levelIndex)
    {
        animator.SetTrigger(triggerAnimationName);
        yield return new WaitForSeconds(transitionTime);
        try
        {
            SceneManager.LoadScene(levelIndex);
        }
        catch (Exception e)
        {
            Debug.LogError($"Could not move to scene with index '{levelIndex}' because: " + e);
        }
    }

    IEnumerator LoadLevelWithName(string levelName)
    {
        animator.SetTrigger(triggerAnimationName);
        SaveFileManager sfm = SaveFileManager.GetInstance();
        sfm.SavePlayerData();
        yield return new WaitForSeconds(transitionTime);

        try
        {
            SceneManager.LoadScene(levelName);
        } catch (Exception e)
        {
            Debug.LogError($"Could not move to '{levelName}' because: " + e);
        }
    }


}
