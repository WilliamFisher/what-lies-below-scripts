using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettlementInfoUIHandler : MonoBehaviour
{
    [SerializeField]
    private Text _capacityText;

    public SettlementData settlementData;

    public void UpdateCapacityText()
    {
        _capacityText.text = "Capacity: " + settlementData.Capacity;
    }
}
