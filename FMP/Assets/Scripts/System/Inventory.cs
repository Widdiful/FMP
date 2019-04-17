﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Inventory : ScriptableObject {

    public List<InventoryItem> items;

    public Dictionary<InventoryItem.ItemType, int> GetItems() {
        Dictionary<InventoryItem.ItemType, int> dic = new Dictionary<InventoryItem.ItemType, int>();

        foreach(InventoryItem item in items) {
            if (!dic.ContainsKey(item.itemType)) {
                dic.Add(item.itemType, 1);
            }
            else {
                dic[item.itemType]++;
            }
        }

        return dic;
    }

    public void AddItem(InventoryItem item) {
        items.Add(item);
    }

    public void AddItem(InventoryItem.ItemType type) {
        InventoryItem item = new InventoryItem();
        item.itemType = type;
        AddItem(item);
    }

    public void AddExtraLife() {
        AddItem(InventoryItem.ItemType.ExtraLife);
    }

    public void AddSpeedIncrease() {
        AddItem(InventoryItem.ItemType.SpeedIncrease);
    }

    public void AddMoneyMultiplier() {
        AddItem(InventoryItem.ItemType.MoneyMultiplier);
    }

    public void RemoveItem(InventoryItem item) {
        items.Remove(item);
    }
}