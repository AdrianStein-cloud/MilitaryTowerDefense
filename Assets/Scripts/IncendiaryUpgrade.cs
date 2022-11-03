using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "IncendiaryUgprade", menuName = "Upgrades/New Incendiary Upgrade")]
public class IncendiaryUpgrade : Upgrade
{
    private int fireDamage = 1;

    public override void ApplyUpgrade(Turret turret)
    {
        turret.isIncendiary = true;
        turret.fireDamage += fireDamage;
    }
}
