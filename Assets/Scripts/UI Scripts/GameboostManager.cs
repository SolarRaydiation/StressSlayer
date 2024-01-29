using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameboostManager : MonoBehaviour
{
    public static GameboostManager instance;

    [Header("UI")]
    [SerializeField] private Button fruitsAndVegtableButton;
    [SerializeField] private Button cortisolButton;
    [SerializeField] private Button drugsButton;

    [Header("Item Boost Inventory")]
    public int cashRemaining;
    public int fruitsAndVegetablesOwned;
    public int cortisolInjectorsOwned;
    public int assortedDrugsOwned;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PlayerStatsScript in the scene!");
        }
        instance = this;
    }

    public static GameboostManager GetInstance()
    {
        return instance;
    }

    void Start()
    {
        LoadData();
        CheckInjectorInventory();
        CheckSADInventory();
        CheckFruitsAndVegetableInventory();
    }

    private void LoadData()
    {
        SaveFileManager sfm = SaveFileManager.GetInstance();
        PlayerData saveFile = sfm.saveFile;
        if (saveFile != null)
        {
            fruitsAndVegetablesOwned = saveFile.fruitsAndVegetablesOwned;
            cortisolInjectorsOwned = saveFile.cortisolInjectorsOwned;
            assortedDrugsOwned = saveFile.assortedDrugsOwned;
            cashRemaining = saveFile.cashRemaining;
        }
        else
        {
            fruitsAndVegetablesOwned = 0;
            cortisolInjectorsOwned = 0;
            assortedDrugsOwned = 0;
            Debug.LogError($"No save file found. PlayerInventoryManager will default to fall-back.");
        }
    }

    #region Checker if Inventory is Present
    private void CheckInjectorInventory()
    {
        if(cortisolInjectorsOwned != 0)
        {
            Debug.Log($"Found {cortisolInjectorsOwned} cortisol injectors");
            cortisolButton.interactable = true;
            cortisolButton.gameObject.transform.Find("Number").GetComponent<TextMeshProUGUI>().SetText(cortisolInjectorsOwned.ToString());
        } else
        {
            Debug.Log($"Found {cortisolInjectorsOwned} cortisol injectors");
            cortisolButton.interactable = false;
            cortisolButton.gameObject.transform.Find("Number").GetComponent<TextMeshProUGUI>().SetText("0");
        }
    }

    private void CheckSADInventory()
    {
        if (assortedDrugsOwned != 0)
        {
            Debug.Log($"Found {assortedDrugsOwned} SAD injectors");
            drugsButton.interactable = true;
            drugsButton.gameObject.transform.Find("Number").GetComponent<TextMeshProUGUI>().SetText(assortedDrugsOwned.ToString());
        }
        else
        {
            Debug.Log($"Found {assortedDrugsOwned} SAD injectors");
            drugsButton.interactable = false;
            drugsButton.gameObject.transform.Find("Number").GetComponent<TextMeshProUGUI>().SetText("0");
        }
    }

    private void CheckFruitsAndVegetableInventory()
    {
        if (fruitsAndVegetablesOwned != 0)
        {
            Debug.Log($"Found {fruitsAndVegetablesOwned} fruitsAndVegetables");
            fruitsAndVegtableButton.interactable = true;
            fruitsAndVegtableButton.gameObject.transform.Find("Number").GetComponent<TextMeshProUGUI>().SetText(fruitsAndVegetablesOwned.ToString());
        }
        else
        {
            Debug.Log($"Found {fruitsAndVegetablesOwned} fruitsAndVegetables");
            fruitsAndVegtableButton.interactable = false;
            fruitsAndVegtableButton.gameObject.transform.Find("Number").GetComponent<TextMeshProUGUI>().SetText("0");
        }
    }

    #endregion

    #region Inflict Effects
    public void UseInjector()
    {
        StressManager instance = StressManager.GetInstance();
        instance.AddStress(20);
        cortisolInjectorsOwned--;

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsScript>().SimulateEffectsOfDrugUse();

        CheckInjectorInventory();
    }

    public void UseFruitsAndVegetables()
    {
        StressManager instance = StressManager.GetInstance();
        instance.ReduceStress(10);
        fruitsAndVegetablesOwned--;

        CheckFruitsAndVegetableInventory();
    }

    public void UseAssortedDrugs()
    {
        StressManager instance = StressManager.GetInstance();
        instance.ReduceStress(10);
        assortedDrugsOwned--;

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsScript>().SimulateEffectsOfDrugUse();
        CheckSADInventory();
    }

    #endregion
}
