using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettlementUpgradeUI : MonoBehaviour
{
    private static SettlementUpgradeUI _instance;

    public static SettlementUpgradeUI Instance { get { return _instance; } }

    public int upgradeMarkerIndex = 0;
    

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    [SerializeField]
    private Button _cancelButton = null;

    [SerializeField]
    private GameObject _upgradePanel = null;
    [SerializeField]
    private Transform _upgradeSlotHolder = null;

    void Start()
    {
        _cancelButton.onClick.AddListener(HideUI);
        _upgradePanel.SetActive(false);
    }

    public void GenerateSettlementUpgradeSlots(Settlement settlement)
    {
        for (int i = 0; i < _upgradeSlotHolder.childCount; i++)
        {
            SettlementUpgradeSlot slot = _upgradeSlotHolder.transform.GetChild(i).GetComponent<SettlementUpgradeSlot>();
            slot.settlement = settlement;
        }
    }

    public void ClearSettlementUpgradeSlots()
    {
        GameObject[] upgradeSlots = GameObject.FindGameObjectsWithTag("SettlementUpgradeSlot");
        foreach(GameObject slotObj in upgradeSlots)
        {
            Destroy(slotObj);
        }
    }

    public void ShowUI(int markerIndex)
    {
        //Display the UI
        _upgradePanel.SetActive(true);
        upgradeMarkerIndex = markerIndex;
        Cursor.lockState = CursorLockMode.Confined;
        Camera.main.GetComponent<CharacterLook>().lockCamRotation = true;
    }

    public void HideUI()
    {
        _upgradePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Camera.main.GetComponent<CharacterLook>().lockCamRotation = false;
    }
}
