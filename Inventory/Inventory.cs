using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Inventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    public ItemContainer itemContainer = new ItemContainer(8);

    [SerializeField]
    private ItemSlot testItem = new ItemSlot();

    [ContextMenu("Add Item")]
    public void TestAdd()
    {
        itemContainer.AddItem(testItem);
    }

    [ContextMenu("Remove All")]
    public void RemoveAll()
    {
        itemContainer.RemoveAll();
    }

}
