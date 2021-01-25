using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Tier", menuName = "Item Tier")]
public class LootItemTier : ScriptableObject
{
    [SerializeField]
    private ItemSlot[] _possibleItems = null;

    public ItemSlot[] PossibleItems { get { return _possibleItems; } }
}
