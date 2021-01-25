using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier
{
    public float Value { get; private set; }

    public StatModifier(float modifierValue)
    {
        Value = modifierValue;
    }
}
