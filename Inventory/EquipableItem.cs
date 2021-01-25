using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipable", menuName = "Inventory/Equipable Item")]
public class EquipableItem : InventoryItem
{
    [SerializeField]
    private GameObject _prefab = null;

    public GameObject Prefab { get { return _prefab; } }
}
