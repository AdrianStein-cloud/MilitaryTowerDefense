using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IncendiaryUgprade", menuName = "Upgrades/New Fire Rate Upgrade")]
public class FireRateUpgrade : Upgrade
{
    public override void ApplyUpgrade(Turret turret)
    {
        turret.fireRate++;
    }
}
