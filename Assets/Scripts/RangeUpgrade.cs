using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IncendiaryUgprade", menuName = "Upgrades/New Range Upgrade")]
public class RangeUpgrade : Upgrade
{
    public override void ApplyUpgrade(Turret turret)
    {
        turret.range++;
        turret.UpdateRange();
    }
}
