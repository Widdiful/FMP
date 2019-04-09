using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class ShopButton : MonoBehaviour {

    public ShopInventory.ShopItem itemData;

    public void Click() {
        if (gameManager.instance.SpendMoney(itemData.price)) {
            InventoryManager.instance.inventory.AddItem(itemData.type);
        }
    }
}
