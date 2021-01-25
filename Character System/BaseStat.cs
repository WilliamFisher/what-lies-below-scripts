using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Base Stat", menuName = "Character/BaseStat")]
public class BaseStat : ScriptableObject
{
    [SerializeField]
    private float _initialValue;
    private List<StatModifier> _statModifiers = new List<StatModifier>();
    private float _calculatedValue;

    public float CalculatedValue { get { return CalculateCurrentValue(); } }

    public void AddModifier(StatModifier modifier)
    {
        _statModifiers.Add(modifier);
    }

    public void RemoveModifier(StatModifier modifier)
    {
        _statModifiers.Remove(modifier);
    }

    private float CalculateCurrentValue()
    {
        float currentValue = _initialValue;
        foreach(StatModifier modifier in _statModifiers)
        {
            currentValue += modifier.Value;
        }

        return currentValue;
    }
}
