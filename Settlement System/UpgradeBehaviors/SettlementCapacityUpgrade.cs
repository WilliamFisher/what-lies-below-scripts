using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementCapacityUpgrade : SettlementUpgradeSlot
{
    [SerializeField]
    private int increaseAmount;

    public override void ApplyUpgrade()
    {
        base.ApplyUpgrade();
        settlement.SettlementData.Capacity += increaseAmount;
    }
}
