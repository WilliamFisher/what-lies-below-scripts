using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementMarker : MonoBehaviour, IInteractable
{
    [SerializeField]
    Settlement settlement = null;

    [SerializeField]
    private GameObject settlementUIPrefab;

    private bool _claimable = true;

    void Start()
    {
        if (settlement.SettlementData.CanBeClaimed)
        {
            _claimable = true;
        }
        else
        {
            _claimable = false;
        }
    }

    public string GetInteractText()
    {
        if(_claimable)
            return "Press E to claim this settlement.";

        return "Press E to open your settlement overview!";
    }

    public void Interact(GameObject go)
    {
        if (_claimable)
        {
            CharacterBase characterInstance = go.GetComponent<CharacterBase>();
            CharacterData characterData = characterInstance.Character;

            characterInstance.RecruitToSettlement(settlement);

            settlement.ClaimSettlement(characterData);
            settlement.SpawnUpgradeMarkers();
            _claimable = false;
        }
        else
        {
            //Open the settlement UI
            GameObject settlementUI = Instantiate(settlementUIPrefab);
            ResourcesUIHandler resourcesUIHandler = settlementUI.GetComponentInChildren<ResourcesUIHandler>();
            resourcesUIHandler.settlementData = settlement.SettlementData;

            SettlementInfoUIHandler infoUIHandler = settlementUI.GetComponentInChildren<SettlementInfoUIHandler>();
            infoUIHandler.settlementData = settlement.SettlementData;
            infoUIHandler.UpdateCapacityText();

            //Lock cam state
            Cursor.lockState = CursorLockMode.Confined;
            Camera.main.GetComponent<CharacterLook>().lockCamRotation = true;
        }
    }
}
