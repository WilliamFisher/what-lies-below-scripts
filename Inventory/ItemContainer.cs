using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemContainer
{
    private ItemSlot[] _itemSlots = new ItemSlot[0];

    public delegate void OnItemsUpdated();
    public static event OnItemsUpdated onItemsUpdated;

    public ItemContainer(int size)
    {
        _itemSlots = new ItemSlot[size];
    }

    public ItemSlot GetSlotAtIndex(int index)
    {
        return _itemSlots[index];
    }

    //Add the itemslot only checking if there is space available
    public bool AddItemSlot(ItemSlot itemSlot)
    {
        for(int i = 0; i < _itemSlots.Length; i++)
        {
            //If this slot is empty
            if(_itemSlots[i].item == null)
            {
                _itemSlots[i].item = itemSlot.item;
                _itemSlots[i].quantity = itemSlot.quantity;
                onItemsUpdated?.Invoke();
                return true;
            }
        }
        return false;
    }

    public ItemSlot AddItem(ItemSlot itemSlot)
    {
        //Loop through each slot
        for(int i=0;i < _itemSlots.Length; i++)
        {
            //Check if the current slot already has this item
            if(_itemSlots[i].item == itemSlot.item)
            {
                //Check if we have room to add the entire quantity
                if(itemSlot.quantity < _itemSlots[i].item.MaxStackSize - _itemSlots[i].quantity)
                {
                    _itemSlots[i].quantity += itemSlot.quantity;
                    itemSlot.quantity = 0;
                    onItemsUpdated?.Invoke();
                    return itemSlot;
                }
                else // The item exists but we cant add the entire quantity to this stack, add what we can
                {
                    int amountWeCanAdd = _itemSlots[i].item.MaxStackSize - _itemSlots[i].quantity;
                    if (amountWeCanAdd != 0)
                    {
                        _itemSlots[i].quantity += amountWeCanAdd;
                        itemSlot.quantity -= amountWeCanAdd;
                        
                    }
                }
            }
        }

        //If we get here we did not find a slot with the same item
        // If we have the space, add the item.
        for(int i=0;i < _itemSlots.Length; i++)
        {
            if(_itemSlots[i].item == null)
            {
                _itemSlots[i].item = itemSlot.item;
                _itemSlots[i].quantity = itemSlot.quantity;
                itemSlot.quantity = 0;
                onItemsUpdated?.Invoke();
                return itemSlot;
            }
        }
        onItemsUpdated?.Invoke();
        return itemSlot;
    }

    public void RemoveSlotAtIndex(int index)
    {
        _itemSlots[index] = new ItemSlot();
        onItemsUpdated?.Invoke();
    }

    public void RemoveAll()
    {
        for(int i=0; i < _itemSlots.Length; i++)
        {
            _itemSlots[i] = new ItemSlot();
        }
        onItemsUpdated?.Invoke();
    }

    public void TakeItemFromSlot(int index)
    {
        if (index < 0 || index > _itemSlots.Length) return;

        if(_itemSlots[index].quantity > 1)
        {
            _itemSlots[index].quantity -= 1;
        }
        else
        {
            _itemSlots[index] = new ItemSlot();
        }

        onItemsUpdated?.Invoke();
    }

    public ItemSlot[] GetSlotsWithItems()
    {
        List<ItemSlot> slotsWithItems = new List<ItemSlot>();
        foreach(ItemSlot slot in _itemSlots)
        {
            if(slot.quantity > 0)
            {
                slotsWithItems.Add(slot);
            }
        }
        return slotsWithItems.ToArray();
    }
}
