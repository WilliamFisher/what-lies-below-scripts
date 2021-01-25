using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settlement : MonoBehaviour
{
    [SerializeField]
    private SettlementData settlementData;

    public Transform[] settlementUpgradeSpawns = null;

    [SerializeField]
    private GameObject _upgradeMarkerPrefab = null;

    private List<GameObject> upgradeMarkerObjects = new List<GameObject>();

    public SettlementData SettlementData { get { return settlementData; } }

    private int index = 0;

    private void Start()
    {
        if (!settlementData.CanBeClaimed)
        {
            SpawnUpgradeMarkers();
        }
    }

    public void SpawnUpgradeMarkers()
    {
        foreach (Transform spawn in settlementUpgradeSpawns)
        {
            GameObject newUpgradeMarker = Instantiate(_upgradeMarkerPrefab, new Vector3(spawn.position.x, spawn.position.y, spawn.position.z), spawn.rotation);
            SettlementUpgradeMarker markerScript = newUpgradeMarker.GetComponent<SettlementUpgradeMarker>();
            markerScript.markerIndex = index;
            index++;
            upgradeMarkerObjects.Add(newUpgradeMarker);
        }
    }

    public void DisableMarkerAtIndex(int index)
    {
        upgradeMarkerObjects[index].SetActive(false);
    }

    public void ClaimSettlement(CharacterData character)
    {
        if (settlementData.CanBeClaimed)
        {
            settlementData.CanBeClaimed = false;
            settlementData.AddCharacter(character);
        }
    }
}
