using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        // for Madam Lusog
        FruitsAndVegetables,
        // for drug user
        AssortedDrugs, CortisolInjector
    }

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
                return "This is a fruit and vegetable.";
            case ItemType.AssortedDrugs:
                return "These are assorted drugs.";
            case ItemType.CortisolInjector:
                return "This is a cortisol injector.";
            default:
                return "Default";
        }
    }

    public static void GetSprite(ItemType itemType)
    {
        // to be filled at a later date
    }
}
