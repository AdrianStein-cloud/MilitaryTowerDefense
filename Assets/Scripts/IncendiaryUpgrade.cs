using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "IncendiaryUpgrade", menuName = "Upgrades/New Incendiary Upgrade")]
public class IncendiaryUpgrade : Upgrade
{
    public int fireDamage = 1;
    public Sprite updateSprite;
    public bool fireToChildren = false;

    public override void ApplyUpgrade(Turret turret)
    {
        turret.isIncendiary = true;
        turret.fireDamage += fireDamage;
        if(updateSprite != null) turret.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = updateSprite;
        if(fireToChildren) turret.fireToChildren = true;
    }
}
