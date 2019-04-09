using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem {

	public enum ItemType { ExtraLife, SpeedIncrease, MoneyMultiplier };
    public ItemType itemType;

    public void UseItem() {
        switch (itemType) {
            case ItemType.ExtraLife:
                break;
            case ItemType.SpeedIncrease:
                break;
            case ItemType.MoneyMultiplier:
                break;
        }

        InventoryManager.instance.inventory.RemoveItem(this);
    }
}
