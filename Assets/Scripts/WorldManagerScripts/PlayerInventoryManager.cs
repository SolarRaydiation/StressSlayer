using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;

public class PlayerInventoryManager : MonoBehaviour
{
    [SerializeField] private static PlayerInventoryManager instance;


    [Header("Cash")]
    [SerializeField] private int cashRemaining;

    [Header("Item Boost Inventory")]
    [SerializeField] private int fruitsAndVegetablesOwned;
    [SerializeField] private int cortisolInjectorsOwned;
    [SerializeField] private int assortedDrugsOwned;

    #region Initialization

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Warning! More than one PlayerInventoryManager in Scene!");
        }
        instance = this;
    }

    private void Start()
    {
        LoadData();
    }

    public static PlayerInventoryManager GetInstance()
    {
        return instance;
    }


    private void LoadData()
    {
        SaveFileManager sfm = SaveFileManager.GetInstance();
        PlayerData saveFile = sfm.saveFile;
        Debug.Log("Savefile looks like this: " + saveFile);
        if (saveFile != null)
        {
            cashRemaining = saveFile.cashRemaining;
            fruitsAndVegetablesOwned = saveFile.fruitsAndVegetablesOwned;
            cortisolInjectorsOwned = saveFile.cortisolInjectorsOwned;
            assortedDrugsOwned = saveFile.assortedDrugsOwned;
            Debug.Log("Savefile loaded to PlayerInventoryManager");
        }
        else
        {
            cashRemaining = 0;
            fruitsAndVegetablesOwned = 0;
            cortisolInjectorsOwned = 0;
            assortedDrugsOwned = 0;
            Debug.LogError($"No save file found. PlayerInventoryManager will default to fall-back.");
        }
    }

    #endregion

    #region Cash Manageemnt Methods

    public int GetCashStat()
    {
        return cashRemaining;
    }

    public void IncreaseCash(int changeInCash)
    {
        cashRemaining = cashRemaining + changeInCash;
    }

    public void DecreaseCash(int changeInCash)
    {
        cashRemaining = cashRemaining - changeInCash;
    }

    #endregion

    #region Item Management Methods

    public void AddItem(Item.ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.FruitsAndVegetables:
                fruitsAndVegetablesOwned++;
                break;
            case ItemType.AssortedDrugs:
                assortedDrugsOwned++;
                break;
            case ItemType.CortisolInjector:
                cortisolInjectorsOwned++;
                break;
            default:
                Debug.LogError($"ItemType {itemType} could not be added to inventory.");
                break;
        }
    }

    public void RemoveItem(Item.ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.FruitsAndVegetables:
                fruitsAndVegetablesOwned--;
                break;
            case ItemType.AssortedDrugs:
                assortedDrugsOwned--;
                break;
            case ItemType.CortisolInjector:
                cortisolInjectorsOwned--;
                break;
            default:
                Debug.LogError($"ItemType {itemType} could not be removed to inventory.");
                break;
        }
    }

    public int GetFruitsAndVegetablesOwned()
    {
        return fruitsAndVegetablesOwned;
    }

    public int GetCortisolInjectorsOwned()
    {
        return cortisolInjectorsOwned;
    }

    public int GetAssortedDrugsOwned()
    {
        return assortedDrugsOwned;
    }

    #endregion
}
