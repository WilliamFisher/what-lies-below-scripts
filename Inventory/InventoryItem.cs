using UnityEngine;

[CreateAssetMenu (fileName="New Item", menuName = "Item")]
public class InventoryItem : ScriptableObject
{
    [SerializeField]
    private string itemName = "New Item Name";
    [SerializeField]
    private Sprite icon = null;
    [SerializeField]
    private int maxStack = 1;

    public string Name { get { return itemName; } }

    public Sprite Icon { get {return icon; } }

    public int MaxStackSize { get {return maxStack; } }

    public string GetInfoDisplayText()
    {
        return string.Empty;
    }
}