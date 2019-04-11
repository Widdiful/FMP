using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
[System.Serializable]
public class ShopButton : MonoBehaviour {

    public ShopInventory.ShopItem itemData;
    public enum ButtonTypes { Shop, Inventory, Use };
    public ButtonTypes buttonType;
    public int displayNumber;
    public Text text;

    private void Start() {
        text.text = displayNumber.ToString();
    }


    public void Click() {
        switch (buttonType) {
            case ButtonTypes.Shop:
                if (gameManager.instance.SpendMoney(itemData.price)) {
                    InventoryManager.instance.inventory.AddItem(itemData.type);
                }
                break;

            case ButtonTypes.Inventory:
                if (InventoryManager.instance)
                    InventoryManager.instance.AddItem(itemData.type);
                break;

            case ButtonTypes.Use:
                if (InventoryManager.instance)
                    InventoryManager.instance.RemoveItem(itemData.type);
                break;
        }
    }
}
