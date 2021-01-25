using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character_MutatableStat", menuName = "Character/Mutatable Stat")]
public class MutatableStat : ScriptableObject
{
    public float StatValue { get; set; }
}
