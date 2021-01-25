using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesUIHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _resourcesInfoHolder;
    [SerializeField]
    private GameObject _resourceInfoPrefab;
    [SerializeField]
    private GameObject settlementUIObject;

    public SettlementData settlementData;

    void Start()
    {
        SettlementResourceData[] settlementResources = Resources.LoadAll<SettlementResourceData>("Settlement/SettlementResources");
        foreach(SettlementResourceData resource in settlementResources)
        {
            //Create the resource info obj
            GameObject resourceInfoObject = Instantiate(_resourceInfoPrefab, _resourcesInfoHolder.transform);
            ResourceInfoUI ui = resourceInfoObject.GetComponent<ResourceInfoUI>();
            ui.resource = resource;
            ui.settlementData = settlementData;
            ui.UpdateUI();
        }
    }

    public void DestorySettlementUI()
    {
        Destroy(settlementUIObject);

        Cursor.lockState = CursorLockMode.Locked;
        Camera.main.GetComponent<CharacterLook>().lockCamRotation = false;
    }
}
