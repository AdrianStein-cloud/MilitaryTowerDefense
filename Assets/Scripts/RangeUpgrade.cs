using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IncendiaryUgprade", menuName = "Upgrades/New Range Upgrade")]
public class RangeUpgrade : Upgrade
{
    public int rangeToAdd = 1;
    public int bulletLifeTimeToAdd = 0;

    public override void ApplyUpgrade(Turret turret)
    {
        turret.range += rangeToAdd;
        turret.UpdateRange();
        turret.bulletLifeTime += bulletLifeTimeToAdd;
    }
}
