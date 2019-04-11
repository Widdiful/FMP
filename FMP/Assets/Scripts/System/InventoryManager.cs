using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public Inventory inventory;

    public static InventoryManager instance;

    public Transform inUseContent, inventoryContent;

    public Dictionary<InventoryItem.ItemType, int> inUseItems = new Dictionary<InventoryItem.ItemType, int>();
    public Dictionary<InventoryItem.ItemType, int> inventoryItems = new Dictionary<InventoryItem.ItemType, int>();

    public GameObject buttonPrefab;

    private void Awake() {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    private void Start() {
        inventoryItems = inventory.GetItems();

        UpdateUI();
    }

    public void AddItem(InventoryItem.ItemType item) {
        if (inventoryItems[item] > 0) {
            if (!inUseItems.ContainsKey(item))
                inUseItems.Add(item, 0);
            inUseItems[item]++;
            inventoryItems[item]--;

            UpdateUI();
        }
    }

    public void RemoveItem(InventoryItem.ItemType item) {
        if (inUseItems[item] > 0) {
            inventoryItems[item]++;
            inUseItems[item]--;

            UpdateUI();
        }
    }

    private void UpdateUI() {
        foreach(Transform transform in inventoryContent) {
            Destroy(transform.gameObject);
        }
        foreach (Transform transform in inUseContent) {
            Destroy(transform.gameObject);
        }
        foreach (KeyValuePair<InventoryItem.ItemType, int> item in inventoryItems) {
            ShopButton newButton = Instantiate(buttonPrefab, inventoryContent).GetComponent<ShopButton>();
            newButton.buttonType = ShopButton.ButtonTypes.Inventory;
            newButton.itemData.type = item.Key;
            newButton.displayNumber = item.Value;
        }
        foreach (KeyValuePair<InventoryItem.ItemType, int> item in inUseItems) {
            ShopButton newButton = Instantiate(buttonPrefab, inUseContent).GetComponent<ShopButton>();
            newButton.buttonType = ShopButton.ButtonTypes.Use;
            newButton.itemData.type = item.Key;
            newButton.displayNumber = item.Value;
        }
    }
}
