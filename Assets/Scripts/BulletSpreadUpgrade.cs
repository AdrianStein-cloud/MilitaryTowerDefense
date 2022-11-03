using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IncendiaryUgprade", menuName = "Upgrades/New Bullet Spread Upgrade")]
public class BulletSpreadUpgrade : Upgrade
{
    public override void ApplyUpgrade(Turret turret)
    {
        ((Shotgun)turret).bulletSpread -= 0.1f;
    }
}
