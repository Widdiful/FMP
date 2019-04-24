using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class ShopInventory : ScriptableObject {

    [System.Serializable]
    public class ShopItem {
        public InventoryItem.ItemType type;
        public int price;
        public Sprite sprite;
        public string displayName;
        public string description;
    }
    public List<ShopItem> items;
}
