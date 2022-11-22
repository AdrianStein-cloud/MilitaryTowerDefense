using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "IncendiaryUgprade", menuName = "Upgrades/New Pierce Upgrade")]

public class PierceUpgrade : Upgrade
{
    public int amountOfPierceToAdd;
    public override void ApplyUpgrade(Turret turret)
    {
        turret.UpdatePierce(amountOfPierceToAdd);
    }
}
