using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceInfoUI : MonoBehaviour
{
    [SerializeField]private Text _nameText;
    [SerializeField]private Text _quantityText;

    public SettlementResourceData resource;
    public SettlementData settlementData;

    private void OnEnable()
    {
        SettlementData.OnSettlementResourceChange += UpdateUI;
    }

    private void OnDisable()
    {
        SettlementData.OnSettlementResourceChange -= UpdateUI;
    }

    public void UpdateUI()
    {
        _nameText.text = resource.Name;
        int resourceQuantity = settlementData.GetResourceQuantity(resource);
        _quantityText.text = resourceQuantity.ToString();
    }

}
