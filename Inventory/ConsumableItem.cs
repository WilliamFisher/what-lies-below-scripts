using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Consumable Item", menuName ="Consumable Item")]
public class ConsumableItem : InventoryItem
{
    [SerializeField]
    private float healAmount;

    public void UseItem(Inventory inventory, int slotIndex)
    {
        CharacterStats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        playerStats.Heal(healAmount);
        inventory.itemContainer.TakeItemFromSlot(slotIndex);
    }
}
