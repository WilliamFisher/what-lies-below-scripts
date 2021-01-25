using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemSlot
{
    public int quantity;
    public InventoryItem item;

    public ItemSlot(InventoryItem item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}
