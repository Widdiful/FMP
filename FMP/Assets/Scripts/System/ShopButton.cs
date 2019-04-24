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
    public Image spriteRenderer;
    public Sprite sprite;

    private void Start() {
        text.text = displayNumber.ToString();
        spriteRenderer.sprite = sprite;
    }


    public void Click() {
        switch (buttonType) {
            case ButtonTypes.Shop:
                BuyItemMenu.instance.OpenShop(itemData);
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
