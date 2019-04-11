﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem {

	public enum ItemType { ExtraLife, SpeedIncrease, MoneyMultiplier };
    public ItemType itemType;
}
