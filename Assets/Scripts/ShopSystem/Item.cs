using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item
{
    private const string FOLDER_PATH = "GameItems/";
    public enum ItemType
    {
        // for Madam Lusog
        FruitsAndVegetables,
        // for drug user
        AssortedDrugs, CortisolInjector
    }

    public Image[] imageArray;

    public static string GetItemName(ItemType type)
    {
        switch(type)
        {
            case ItemType.FruitsAndVegetables:
                return "Fruits And Vegetables";
            case ItemType.AssortedDrugs:
                return "Assorted Drugs";
            case ItemType.CortisolInjector:
                return "Cortisol Injector";
            default:
                return "Default";
        }
    }

    public static int GetItemCost(ItemType itemType)
    {
        switch(itemType)
        {
            case ItemType.FruitsAndVegetables:  return 1;
            case ItemType.AssortedDrugs:        return 2;
            case ItemType.CortisolInjector:     return 3;
            default:                            return 0;
        }
    }

    public static string GetItemDescription(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.FruitsAndVegetables:
                return "Provides a small but healthy reduction to stress when used.";
            case ItemType.AssortedDrugs:
                return "These are assorted drugs.";
            case ItemType.CortisolInjector:
                return "This is a cortisol injector.";
            default:
                return "Default";
        }
    }

    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.FruitsAndVegetables:
                return Resources.Load<Sprite>(string.Concat(FOLDER_PATH, "FruitsAndVegetables"));
            case ItemType.AssortedDrugs:
                return Resources.Load<Sprite>(string.Concat(FOLDER_PATH, "AssortedDrugs"));
            case ItemType.CortisolInjector:
                return Resources.Load<Sprite>(string.Concat(FOLDER_PATH, "CortisolInjector"));
            default:
                Debug.LogWarning($"Recieved abnormal ItemType {itemType} while retrieving Sprite");
                return Resources.Load<Sprite>(string.Concat(FOLDER_PATH, "FruitsAndVegetables"));
        }
    }
}
