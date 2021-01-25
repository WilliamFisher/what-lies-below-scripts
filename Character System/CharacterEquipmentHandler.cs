using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipmentHandler : MonoBehaviour
{
    private GameObject currentEquipmentObject = null;
    private EquipableItem _currentEquipableItem = null;

    public void Equip(EquipableItem equipable)
    {
        if(_currentEquipableItem == null)
        {
            currentEquipmentObject = Instantiate(equipable.Prefab, Camera.main.transform);
            _currentEquipableItem = equipable;
            return;
        }

        //We have an item already equiped
        if(_currentEquipableItem.Equals(equipable))
        {
            Destroy(currentEquipmentObject);
            currentEquipmentObject = null;
            _currentEquipableItem = null;
            return;
        } else
        {
            Destroy(currentEquipmentObject);
            currentEquipmentObject = null;
            _currentEquipableItem = null;
            currentEquipmentObject = Instantiate(equipable.Prefab, Camera.main.transform);
            _currentEquipableItem = equipable;
        }
    }
}
