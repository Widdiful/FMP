using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {

    public ShopInventory inventory;
    public GameObject buttonPrefab;

	void Start () {
        UpdateMenu();
	}

    private void UpdateMenu() {
        // Clears list
        foreach (Transform child in transform) {
            Destroy(child);
        }

        foreach (ShopInventory.ShopItem item in inventory.items) {
            ShopButton newButton = Instantiate(buttonPrefab, transform).GetComponent<ShopButton>();
            newButton.itemData = item;
            newButton.displayNumber = item.price;
            newButton.sprite = item.sprite;
        }
    }
}
