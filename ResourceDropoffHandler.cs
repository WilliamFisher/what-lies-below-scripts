using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDropoffHandler : MonoBehaviour
{
    public SettlementData settlementData = null;

    [SerializeField]
    private GameObject _boxHolder = null;
    private int _currentBoxIndex = 0;
    private int _boxCapacity;

    private void Start()
    {
        _boxCapacity = _boxHolder.transform.childCount;
    }

    public void AddResource(SettlementResourceData resource)
    {
        if (_currentBoxIndex >= _boxCapacity) return;

        GameObject box = _boxHolder.transform.GetChild(_currentBoxIndex).gameObject;
        box.SetActive(true);
        _currentBoxIndex++;
        settlementData.AddResource(resource);
    }
}
