using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public Inventory inventory;

    public static InventoryManager instance;

    private void Awake() {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }
}
