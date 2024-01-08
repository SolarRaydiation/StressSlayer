using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    private ShopManager shopManager;
    public Item.ItemType[] itemTypes;
    public string shopName;

    private void Start()
    {
        shopManager = ShopManager.GetShopManagerInstance();
    }

    public void OpenShop()
    {
        shopManager.OpenShopScreen(shopName, itemTypes);
    }
}
