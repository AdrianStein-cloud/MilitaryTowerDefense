using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DamageUpgrade", menuName = "Upgrades/New Damage Upgrade")]

public class DamageUpgrade : Upgrade
{
    public int damageToAdd;
    public override void ApplyUpgrade(Turret turret)
    {
        turret.UpdateDamage(damageToAdd);
    }
}
