using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IncendiaryUgprade", menuName = "Upgrades/New More Bullets Upgrade")]
public class MoreBulletsUpgrade : Upgrade
{
    public int numberOfBulletsToAdd;

    public override void ApplyUpgrade(Turret turret)
    {
        ((Shotgun)turret).numberOfBulletsPerShot += numberOfBulletsToAdd;
    }
}
