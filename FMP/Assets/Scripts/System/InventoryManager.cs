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

    const float speedIncreaseAmount = 0.1f;
    const float moneyIncreaseAmount = 0.5f;

    private void Awake() {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    private void Start() {
        inventoryItems = inventory.GetItems();
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

            if (inUseItems[item] <= 0) {
                inUseItems.Remove(item);
            }

            UpdateUI();
        }
    }

    public void UpdateUI() {
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

    public void UseItems() {
        float speedIncrease = 0;
        float moneyIncrease = 0;
        int livesIncrease = 0;

        foreach(KeyValuePair<InventoryItem.ItemType, int> item in inUseItems) {
            switch (item.Key) {
                case InventoryItem.ItemType.ExtraLife:
                    livesIncrease += 1 * item.Value;
                    break;
                case InventoryItem.ItemType.SpeedIncrease:
                    speedIncrease += speedIncreaseAmount * item.Value;
                    break;
                case InventoryItem.ItemType.MoneyMultiplier:
                    moneyIncrease += moneyIncreaseAmount * item.Value;
                    break;
            }
        }

        inventory.items = inventoryItems;
        SaveData.instance.Save();

        gameManager.instance.gameSpeed += speedIncrease;
        gameManager.instance.moneyMultiplier += moneyIncrease;
        gameManager.instance.livesLeft += livesIncrease;
    }
}
