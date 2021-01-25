using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Settlement/Resource")]
public class SettlementResourceData : ScriptableObject
{
    [SerializeField]
    private string _resourceName = "New Resource Name";
    [SerializeField]
    private Sprite _resourceIcon = null;

    public string Name { get { return _resourceName; } }
    public Sprite Icon { get { return _resourceIcon; } }
}
