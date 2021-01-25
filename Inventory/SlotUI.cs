using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    [SerializeField]
    private Inventory _inventory = null;
    [SerializeField]
    private Text _itemQuantityText = null;
    [SerializeField]
    private Image _itemIconImage = null;

    public int SlotIndex { get; private set; }

    public InventoryItem SlotItem { get { return ItemSlot.item; } }

    public ItemSlot ItemSlot { get { return _inventory.itemContainer.GetSlotAtIndex(SlotIndex);  } }

    public void SetInventory(Inventory inventory)
    {
        _inventory = inventory;
    }

    public void UpdateSlotUI()
    {
        if(ItemSlot.item == null)
        {
            EnableSlotUI(false);
            return;
        }

        EnableSlotUI(true);

        _itemIconImage.sprite = ItemSlot.item.Icon;
        _itemQuantityText.text = ItemSlot.quantity > 1 ? ItemSlot.quantity.ToString() : "";
    }

    private void OnEnable()
    {
        ItemContainer.onItemsUpdated += UpdateSlotUI;
        UpdateSlotUI();
        CharacterDatabase.onCharacterChanged += UpdateSlotUI;
    }

    private void OnDisable()
    {
        ItemContainer.onItemsUpdated -= UpdateSlotUI;
        CharacterDatabase.onCharacterChanged -= UpdateSlotUI;
    }

    private void Start()
    {
        SlotIndex = transform.GetSiblingIndex();
        UpdateSlotUI();
    }

    private void EnableSlotUI(bool shouldEnable)
    {
        _itemIconImage.enabled = shouldEnable;
        _itemQuantityText.enabled = shouldEnable;
    }

    [ContextMenu("Test Update")]
    public void TestUpdate()
    {
        UpdateSlotUI();
    }
}
