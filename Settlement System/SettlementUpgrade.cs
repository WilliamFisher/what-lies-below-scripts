using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[CreateAssetMenu(fileName ="New Upgrade", menuName = "Settlement/Upgrade")]
public class SettlementUpgrade : ScriptableObject
{
    [SerializeField]
    private string _name = "Settlement Upgrade";

    /* Unity will not serialize a Dictionary for us.
     * Expose the key and values seperately and build the dictionary as a workaround.
     */
    [SerializeField]
    private SettlementResourceData[] _requiredResource;
    [SerializeField]
    private int[] _amountResourceRequired;
    private Dictionary<SettlementResourceData, int> _requiredResources = null;

    [SerializeField]
    private GameObject _prefab = null;

    public string Name { get { return _name; } }
    public Dictionary<SettlementResourceData, int> RequiredResources { get { return _requiredResources; } }
    public GameObject Prefab { get { return _prefab; } }

    private void OnEnable()
    {
        _requiredResources = new Dictionary<SettlementResourceData, int>();
        for(int i = 0; i < _requiredResource.Length; i++)
        {
            _requiredResources.Add(_requiredResource[i], _amountResourceRequired[i]);
        }
    }
}
