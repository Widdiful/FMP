using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Inventory : ScriptableObject {

    public Dictionary<InventoryItem.ItemType, int> items = new Dictionary<InventoryItem.ItemType, int>();

    public Dictionary<InventoryItem.ItemType, int> GetItems() {
        SaveData.instance.Load();
        //Dictionary<InventoryItem.ItemType, int> dic = new Dictionary<InventoryItem.ItemType, int>();

        //foreach(InventoryItem item in items) {
        //    if (!dic.ContainsKey(item.itemType)) {
        //        dic.Add(item.itemType, 1);
        //    }
        //    else {
        //        dic[item.itemType]++;
        //    }
        //}

        return items;
    }

    //public void AddItem(InventoryItem item) {
    //    items.Add(item);
    //}

    public void AddItem(InventoryItem.ItemType type) {
        //InventoryItem item = new InventoryItem();
        //item.itemType = type;
        //AddItem(item);

        if (!items.ContainsKey(type)) {
            items[type] = 0;
        }
        items[type]++;

        SaveData.instance.Save();
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
        //items.Remove(item);
        items[item.itemType]--;
    }
    public void RemoveItem(InventoryItem.ItemType item) {
        items[item]--;
        SaveData.instance.Save();
    }

    public void RemoveItem(InventoryItem.ItemType item, int amount) {
        items[item] -= amount;
        SaveData.instance.Save();
    }
}
