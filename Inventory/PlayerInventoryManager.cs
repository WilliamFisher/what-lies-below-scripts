using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryManager : MonoBehaviour
{
    private static PlayerInventoryManager _instance;

    public static PlayerInventoryManager Instance { get { return _instance; } }


    [SerializeField] private GameObject playerSlotHolder = null;
    [SerializeField] private CharacterDatabase characterDatabase = null;
    [SerializeField] private GameObject _inventoryPanel = null;
    [SerializeField] private Text lootInfoText = null;
    [SerializeField] private float timeToDisplayLootText = 3;

    private SlotUI[] slots = new SlotUI[8];

    private bool _inventoryDisplayed = true;

    private Inventory currentPlayerInventory = null;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        slots = playerSlotHolder.GetComponentsInChildren<SlotUI>();
        UpdateSlotInventory();
        UpdateClickHandlers();
        ToggleUI();
    }

    void Init()
    {
        UpdateSlotInventory();
        UpdateClickHandlers();
    }

    void UpdateSlotInventory()
    {
        CharacterData character = characterDatabase.currentCharacter;
        currentPlayerInventory = character.GetInventory();
        foreach (SlotUI slot in slots)
        {
            slot.SetInventory(currentPlayerInventory);
        }
    }

    void UpdateClickHandlers()
    {
        ItemClickHandler[] itemClickHandlers = playerSlotHolder.GetComponentsInChildren<ItemClickHandler>();
        foreach(ItemClickHandler handler in itemClickHandlers)
        {
            handler.SetInventory(currentPlayerInventory);
        }
    }

    private void OnEnable()
    {
        CharacterDatabase.onCharacterChanged += Init;
        ItemContainer.onItemsUpdated += Init;
    }

    private void OnDisable()
    {
        CharacterDatabase.onCharacterChanged -= Init;
        ItemContainer.onItemsUpdated -= Init;
    }

    public void ToggleUI()
    {
        _inventoryPanel.SetActive(!_inventoryDisplayed);
        _inventoryDisplayed = !_inventoryDisplayed;
        if (_inventoryDisplayed)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Camera.main.GetComponent<CharacterLook>().lockCamRotation = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Camera.main.GetComponent<CharacterLook>().lockCamRotation = false;
        }
    }

    public Inventory GetCharacterInventory()
    {
        return currentPlayerInventory;
    }

    public void DisplayPickedUpItems(ItemSlot[] slots)
    {
        string textToDisplay = "Added Items:\n";
        foreach(ItemSlot slot in slots)
        {
            textToDisplay += slot.quantity.ToString();
            textToDisplay += "x ";
            textToDisplay += slot.item.Name;
            textToDisplay += "\n";
        }

        lootInfoText.text = textToDisplay;
        Invoke("ClearLootInfoText", timeToDisplayLootText);
    }

    private void ClearLootInfoText()
    {
        lootInfoText.text = "";
    }
}
