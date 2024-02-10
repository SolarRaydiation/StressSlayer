using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Item;

public class ShopManager : MonoBehaviour
{
    public GameObject shopScreen;
    public GameObject ErrorScreenWhenBuyingItems;

    [Header("Canvases To Hide")]
    public GameObject[] canvases;

    [Header("World Manager Script References")]
    private static PlayerInventoryManager pim;
    private static ShopManager instance;

    [Header("UI Elements")]
    // variables for containers holding UI elements
    private Transform container;
    private Transform display;

    // cash and shop name
    [SerializeField] private TextMeshProUGUI shopNameText;
    [SerializeField] private TextMeshProUGUI cashRemainingText;

    // item template
    private Transform itemTemplate; // for displaying the available options for money

    // Item Display
    private GameObject itemName;
    private GameObject itemDescription;
    private TextMeshProUGUI itemNameText;
    private TextMeshProUGUI itemDescriptionText;
    private GameObject buyButton;

    // public variables
    

    /* =============================================
     * Initialization
     * ========================================== */

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is more than one instance of ShopManager in the scene!");
        }
        instance = this;
        
    }

    private void Start()
    {
        GetReferences();
        pim = PlayerInventoryManager.GetInstance();
    }

    public static ShopManager GetShopManagerInstance()
    {
        return instance;
    }

    /* =============================================
     * Methods for Turning On/Off Shop Screen
     * ========================================== */

    public void OpenShopScreen(string shopName, Item.ItemType[] itemType)
    {
        HideCanvases();
        shopScreen.SetActive(true);
        RefreshScreen(shopName, itemType);
    }

    public void CloseShopScreen()
    {
        // delete all duplicates
        shopScreen.SetActive(false);
        ShowCanvases();
        StartCoroutine(RemoveDuplicatesAsync());
    }

    private void RemoveDuplicates()
    {
        string targetNameSubstring = "ItemTemplate(Clone)";
        for (int i = container.childCount - 1; i >= 0; i--)
        {
            Transform child = container.GetChild(i);

            if (child.name.Contains(targetNameSubstring))
            {
                Destroy(child.gameObject);
            }
        }
    }

    IEnumerator RemoveDuplicatesAsync()
    {
        yield return null;
        RemoveDuplicates();
    }

    /* =============================================
     * Core Methods
     * ========================================== */

    // for refreshing the screen's buyable items, erasing the display UI text elements, and updating values
    private void RefreshScreen(string shopName,Item.ItemType[] itemType)
    {
        GetReferences();
        PaintShopItems(itemType);
        HandleOtherShopAspects(shopName);
    }
    // get references from the various UI elements of ShopScreen
    private void GetReferences()
    {
        try
        {
            container = shopScreen.transform.Find("Container");

            // money and shopname
            Transform shopName = container.Find("ShopName");
            shopNameText = shopName.gameObject.GetComponent<TextMeshProUGUI>();
            Transform money = container.Find("CashText");
            cashRemainingText = money.gameObject.GetComponent<TextMeshProUGUI>();

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
        } catch
        {
            
        }
    }

    private void HandleOtherShopAspects(string shopName)
    {
        UpdateCashRemaining();
        SetShopName(shopName);
    }

    // sets what items can be bought at the shop
    private void PaintShopItems(Item.ItemType[] itemType)
    {
        int index = -1;
        foreach (Item.ItemType type in itemType)
        {
            CreateItemButton(type, Item.GetItemName(type), Item.GetItemCost(type), index);
            Debug.Log(Item.GetItemName(type));
            index++;
        }

        /*CreateItemButton(Item.ItemType.AssortedDrugs, Item.GetItemName(Item.ItemType.AssortedDrugs),
                Item.GetItemCost(Item.ItemType.AssortedDrugs), -1);
        CreateItemButton(Item.ItemType.CortisolInjector, Item.GetItemName(Item.ItemType.CortisolInjector),
                Item.GetItemCost(Item.ItemType.CortisolInjector), 0);*/
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
        itemTemplateTransform.Find("ItemImage").GetComponent<Image>().sprite = Item.GetSprite(itemType);

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
            if(pim.GetCashStat() >= Item.GetItemCost(itemType))
            {
                pim.DecreaseCash(Item.GetItemCost(itemType));
                pim.AddItem(itemType);
                UpdateCashRemaining();
            } else
            {
                Instantiate(ErrorScreenWhenBuyingItems);
            }
        });
    }

    /* =============================================
     * Show/Hide Canvases Methods
     * ========================================== */
    private void HideCanvases()
    {
        foreach (GameObject c in canvases)
        {
            c.SetActive(false);
        }
    }

    private void ShowCanvases()
    {
        foreach (GameObject c in canvases)
        {
            c.SetActive(true);
        }
    }

    /* =============================================
     * Supporting Methods
     * ========================================== */

    private void UpdateCashRemaining()
    {
        try
        {
            cashRemainingText.SetText(pim.GetCashStat().ToString());
        } catch (Exception ex)
        {
            if(cashRemainingText == null)
            {
                Debug.LogWarning("cashRemainingText reference not present!");
            }

            if(pim == null)
            {
                Debug.LogWarning("pim reference not present!");
            }
        }
    }

    private void SetShopName(string shopName)
    {
        shopNameText.SetText(shopName);
    }
}
