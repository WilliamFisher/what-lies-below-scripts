using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettlementUpgradeSlot : MonoBehaviour
{
    public SettlementUpgrade upgrade;
    [SerializeField]private Text upgradeNameText = null;
    [SerializeField] private Text upgradeRequirementsText = null;
    private Button _button = null;

    public Settlement settlement = null;

    void Start()
    {
        upgradeNameText.text = upgrade.Name;
        string resourcesRequired = "";
        foreach (KeyValuePair<SettlementResourceData, int> kvp in upgrade.RequiredResources)
        {
            resourcesRequired += kvp.Value.ToString();
            resourcesRequired += " ";
            resourcesRequired += kvp.Key.Name;
            resourcesRequired += "\n";
        }
        upgradeRequirementsText.text = resourcesRequired;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ApplyUpgrade);

        settlement = GameObject.Find("Office Settlement").GetComponent<Settlement>();
    }

    public virtual void ApplyUpgrade()
    {
        if (!HasRequiredResources())
        {
            Debug.Log("You do not have the required resources");
            return;
        }

        foreach (KeyValuePair<SettlementResourceData, int> kvp in upgrade.RequiredResources)
        {
            SettlementResourceData resourceRequired = kvp.Key;
            int amountRequired = kvp.Value;

            settlement.SettlementData.RemoveResource(resourceRequired, amountRequired);
        }
        int markerIndex = SettlementUpgradeUI.Instance.upgradeMarkerIndex;
        GameObject upgradeObject = Instantiate(upgrade.Prefab, settlement.settlementUpgradeSpawns[markerIndex]);
        upgradeObject.transform.localPosition = new Vector3(0, 0, 0);
        settlement.DisableMarkerAtIndex(markerIndex);

        Destroy(gameObject);
    }

    private bool HasRequiredResources()
    {
        foreach(KeyValuePair<SettlementResourceData, int> kvp in upgrade.RequiredResources)
        {
            SettlementResourceData resourceRequired = kvp.Key;
            int amountRequired = kvp.Value;

            int amountInSettlement = settlement.SettlementData.GetResourceQuantity(resourceRequired);
            if(amountInSettlement < amountRequired)
            {
                return false;
            }
        }
        return true;
    }
}
