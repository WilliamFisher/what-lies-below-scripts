using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementUpgradeMarker : MonoBehaviour, IInteractable
{
    public int markerIndex = 0;

    public void Interact(GameObject interactingObject)
    {
        Settlement playerSettlement = interactingObject.GetComponent<CharacterBase>().CurrentSettlement;
        SettlementUpgradeUI.Instance.GenerateSettlementUpgradeSlots(playerSettlement);
        SettlementUpgradeUI.Instance.ShowUI(markerIndex);
        //Disable camera movement
    }

    public string GetInteractText()
    {
        return "Press E to open the upgrade menu.";
    }
}
