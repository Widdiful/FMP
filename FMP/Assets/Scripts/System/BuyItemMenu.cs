using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemMenu : MonoBehaviour {

    public Text titleText;
    public Text costText;
    public Text descText;
    public Button buyButton;
    public Canvas thisCanvas;

    ShopInventory.ShopItem item;

    public static BuyItemMenu instance;

    private void Awake() {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    public void OpenShop(ShopInventory.ShopItem newItem) {
        thisCanvas.enabled = true;
        item = newItem;
        titleText.text = item.displayName;
        costText.text = item.price.ToString();
        descText.text = item.description;

        if (gameManager.instance.money < item.price) {
            buyButton.interactable = false;
        }
        else {
            buyButton.interactable = true;
        }
    }

    public void BuyItem() {
        if (gameManager.instance.SpendMoney(item.price)) {
            InventoryManager.instance.inventory.AddItem(item.type);
            thisCanvas.enabled = false;
            SaveData.instance.Save();
        }
    }
}
