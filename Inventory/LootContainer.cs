using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainer : MonoBehaviour, IInteractable
{
    [SerializeField] private Inventory _inventory = null;
    [SerializeField] private LootItemTier lootTier = null;
    [SerializeField] private int numberOfItemsToGenerate = 1;

    void GenerateRandomLoot()
    {
        _inventory.itemContainer.RemoveAll();
        for(int i=0; i < numberOfItemsToGenerate; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, lootTier.PossibleItems.Length - 1);
            _inventory.itemContainer.AddItem(lootTier.PossibleItems[randomIndex]);
        }
    }

    public void Interact(GameObject go)
    {
        //Add object to inventory
        Inventory playerInventory = PlayerInventoryManager.Instance.GetCharacterInventory();
        GenerateRandomLoot();
        try
        {
            ItemSlot[] itemContainerLootSlots = _inventory.itemContainer.GetSlotsWithItems();
            foreach(ItemSlot slot in itemContainerLootSlots)
            {
                playerInventory.itemContainer.AddItem(slot);
            }
            PlayerInventoryManager.Instance.DisplayPickedUpItems(itemContainerLootSlots);
            _inventory.itemContainer.RemoveAll();
        } catch(Exception e)
        {
            Debug.LogError("An error occured attempting to loot this itemContainer" + e);
        }
        
        Destroy(gameObject);
    }

    public string GetInteractText()
    {
        return "Press E to loot.";
    }
}
