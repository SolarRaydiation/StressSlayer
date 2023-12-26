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
    private Transform container;

    // item template
    private Transform itemTemplate;

    // Item Display
    private Transform display;
    private GameObject itemName;
    private GameObject itemDescription;
    private GameObject buyButton;
    private TextMeshProUGUI itemNameText;
    private TextMeshProUGUI itemDescriptionText;
    private PlayerStatsManager playerStatsManager;

    // public variables
    public GameObject ErrorScreenWhenBuyingItems;

    void Awake()
    {
        GetReferences();
    }

    private void Start()
    {
        PaintShopItems();
    }

    public void RefreshScreen()
    {
        GetReferences();
        PaintShopItems();
    }

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

    private void PaintShopItems()
    {
        CreateItemButton(Item.ItemType.FruitsAndVegetables, "Fruits and Vegetables", Item.GetItemCost(Item.ItemType.FruitsAndVegetables), -1);
        CreateItemButton(Item.ItemType.AssortedDrugs, "Assorted Drugs", Item.GetItemCost(Item.ItemType.AssortedDrugs), 0);
    }

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

    public void GetItemDetails(Item.ItemType itemType)
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
}
                                   