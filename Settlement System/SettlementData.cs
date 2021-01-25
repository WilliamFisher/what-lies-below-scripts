using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settlement", menuName = "Settlement/New Settlement")]
public class SettlementData : ScriptableObject
{
    public static Action OnSettlementResourceChange;
    [SerializeField]
    private string _name = "New Settlement";

    private Dictionary<SettlementResourceData, int> _resources = new Dictionary<SettlementResourceData, int>();

    [SerializeField]
    private SettlementUpgrade[] _upgrades = null;

    [SerializeField]
    private int _characterCapacity = 1;

    [SerializeField]
    private SettlementResourceData _resourceToAdd = null;

    public string SettlementName { get { return _name; } }
    public int Capacity { get { return _characterCapacity; } set { _characterCapacity = value; } }
    public bool CanBeClaimed { get; set; } = true;
    public List<CharacterData> SettlementCharacters { get; private set; }
    public Dictionary<SettlementResourceData, int> Resources { get { return _resources; } }
    public SettlementUpgrade[] Upgrades { get { return _upgrades; } }

    private void OnEnable()
    {
        SettlementCharacters = new List<CharacterData>();
    }

    public bool AddCharacter(CharacterData character)
    {
        if(SettlementCharacters.Count < _characterCapacity)
        {
            SettlementCharacters.Add(character);
            return true;
        }

        return false;
    }

    // For testing in the editor
    [ContextMenu("Toggle Claimed")]
    public void ToggleCanBeClaimed()
    {
        CanBeClaimed = !CanBeClaimed;
    }

    [ContextMenu("Add Resource")]
    public void TestAddResource()
    {
        AddResource(_resourceToAdd);
    }

    public void AddResource(SettlementResourceData resource)
    {
        if (_resources.ContainsKey(resource))
        {
            _resources[resource]++;
        }
        else
        {
            _resources.Add(resource, 1);
        }
        OnSettlementResourceChange?.Invoke();
    }

    public bool RemoveResource(SettlementResourceData resource, int quanity)
    {
        if (!_resources.ContainsKey(resource)) return false;

        if (_resources[resource] < quanity) return false;

        _resources[resource] = _resources[resource] - quanity;

        if(_resources[resource] == 0)
        {
            _resources.Remove(resource);
        }
        OnSettlementResourceChange?.Invoke();
        return true;
    }

    public int GetResourceQuantity(SettlementResourceData resource)
    {
        if (!_resources.ContainsKey(resource)) return 0;

        return _resources[resource];
    }
}
