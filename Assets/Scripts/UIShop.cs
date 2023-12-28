using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    // WorldManager references
    private PlayerStatsManager playerStatsManager;

    // variables for containers holding UI elements
    private Transform container;
    private Transform display;

    // item template
    private Transform itemTemplate; // for displaying the available options for money

    // Item Display
    private GameObject itemName;
    private GameObject itemDescription;
    private TextMeshProUGUI itemNameText;
    private TextMeshProUGUI itemDescriptionText;
    private GameObject buyButton;
    
    // public variables
    public GameObject ErrorScreenWhenBuyingItems;

    /* =============================================
     * Initialization
     * ========================================== */

    void Awake()
    {
        GetReferences();
    }

    private void Start()
    {
        PaintShopItems();
    }

    /* =============================================
     * Core Methods
     * ========================================== */

    // for refreshing the screen's buyable items, erasing the display UI text elements, and updating values
    private void RefreshScreen()
    {
        GetReferences();
        PaintShopItems();
    }
    // get references from the various UI elements of ShopScreen
    private void GetReferences()
    {
        container = transform.Find("Container");

        // Item Template
        itemTemplate = container.Find("ItemTemplate");
        itemTemplate.gameObject.SetActive(false);

        // Item Display
        display = container.Find("ItemDisplay");
        itemName = display.Find("ItemName").gameObject;
        itemDescription = display.Find("ItemDescription").gameObject;
        buyButton = display.Find("BuyButton").gameObject;
        itemNameText = itemName.GetComponent<TextMeshProUGUI>();
        itemDescriptionText = itemDescription.GetComponent<TextMeshProUGUI>();

        // PlayerStatsManager
        GetPlayerStatsManagerFromWorldManager();
    }
    // self-explanatory
    private void GetPlayerStatsManagerFromWorldManager()
    {
        try
        {
            GameObject worldManager = GameObject.FindGameObjectWithTag("WorldManager");
            playerStatsManager = worldManager.GetComponent<PlayerStatsManager>();
        }
        catch (Exception e)
        {
            Debug.LogError($"{name} could not locate WorldManager and the PlayerStatsManager script!: " + e);
        }
    }
    // abstract method for setting what items can be bought in the shop
    private void PaintShopItems()
    {
        CreateItemButton(Item.ItemType.AssortedDrugs, Item.GetItemName(Item.ItemType.AssortedDrugs),
                Item.GetItemCost(Item.ItemType.AssortedDrugs), -1);
        CreateItemButton(Item.ItemType.CortisolInjector, Item.GetItemName(Item.ItemType.CortisolInjector),
                Item.GetItemCost(Item.ItemType.CortisolInjector), 0);
    }
    // for creating the items that can be bought in the shop
    private void CreateItemButton(Item.ItemType itemType, string itemName, int itemCost, int positionIndex)
    {
        Transform itemTemplateTransform = Instantiate(itemTemplate, container);
        itemTemplateTransform.gameObject.SetActive(true);
        RectTransform itemTemplateRectTransform = itemTemplateTransform.GetComponent<RectTransform>();

        float shopItemDistance = 100.0f;
        RectTransform itemTemplateOriginal = itemTemplate.GetComponent<RectTransform>();
        itemTemplateRectTransform.anchoredPosition = new Vector2(itemTemplateOriginal.anchoredPosition.x, -shopItemDistance * positionIndex);
        // itemTemplateRectTransform.transform.position.x
        itemTemplateTransform.Find("ItemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        itemTemplateTransform.Find("ItemPrice").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());

        Button button = itemTemplateTransform.gameObject.GetComponent<Button>();
        Debug.Log(button.gameObject.name);
        button.onClick.AddListener(
            () =>
            {
                GetItemDetails(itemType);
            });
    }
    // for getting the item details in Item.cs
    private void GetItemDetails(Item.ItemType itemType)
    {
        itemNameText.SetText(Item.GetItemName(itemType));
        itemDescriptionText.SetText(Item.GetItemDescription(itemType));
        Button button = buyButton.gameObject.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            if(playerStatsManager.GetPlayerCashStat() >= Item.GetItemCost(itemType))
            {
                playerStatsManager.DecreasePlayerCashStat(Item.GetItemCost(itemType));
                playerStatsManager.AddItem(itemType);
            } else
            {
                Instantiate(ErrorScreenWhenBuyingItems);
            }
        });
    }

    /* =============================================
     * Methods for Turning On/Off Shop Screen
     * ========================================== */

    public void OpenShopScreen()
    {
        RefreshScreen();
    }

    public void CloseShopScreen()
    {
       // delete all duplicates
       gameObject.SetActive(false);
    }
}
                                   