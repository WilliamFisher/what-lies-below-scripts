using UnityEngine;
using UnityEngine.EventSystems;

public class ItemClickHandler : MonoBehaviour, IPointerClickHandler
{
    private float timesClicked = 0;
    private float clickTime = 0;

    private Inventory _inventory = null;
    [SerializeField]
    private SlotUI slotUI = null;

    ItemSlot slot;

    public void SetInventory(Inventory inventory)
    {
        _inventory = inventory;
        slot = _inventory.itemContainer.GetSlotAtIndex(slotUI.SlotIndex);
    }

    void Start()
    {
        
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            timesClicked++;
            if (timesClicked == 1)
                clickTime = Time.time;

            if(timesClicked >= 2)
            {
                if(Time.time - clickTime < 0.5f)
                {
                    //double click
                    _inventory.itemContainer.TakeItemFromSlot(slotUI.SlotIndex);

                    timesClicked = 0;
                    clickTime = 0;
                }
                else
                {
                    //reset
                    timesClicked = 1;
                    clickTime = Time.time;
                }
                
            }
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            // Check if the item is an EquipableItem
            if (slot.item.GetType() == typeof(EquipableItem))
            {
                EquipableItem equipable = slot.item as EquipableItem;
                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterEquipmentHandler>().Equip(equipable);
            }
            // Check if the item is a ConsumableItem
            if (slot.item.GetType() == typeof(ConsumableItem))
            {
                ConsumableItem consumable = slot.item as ConsumableItem;
                consumable.UseItem(_inventory, slotUI.SlotIndex);
            }
        }
    }
}
