using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class AsyncManager : MonoBehaviour
{
    public static AsyncManager instance;

    [Header("Main Menu Screens")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainScreen;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    public void Awake()
    {
        if (instance != null) 
        {
            Debug.LogWarning("There is more than one instance of AsyncManager in the scene!");
        }
        instance = this;
    }

    public static AsyncManager GetInstance()
    {
        return instance;
    }


    public void LoadLevel(string levelNameToLoad)
    {
        disableScreens();
        StartCoroutines(levelNameToLoad);
    }

    public void disableScreens()
    {
        mainScreen.SetActive(false);
        loadingScreen.SetActive(true);
    }

    public void StartCoroutines(string levelNameToLoad)
    {
        StartCoroutine(LoadLevelAsync(levelNameToLoad));
    }
    
    IEnumerator LoadLevelAsync(string levelNameToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelNameToLoad);
        while(!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }
}
