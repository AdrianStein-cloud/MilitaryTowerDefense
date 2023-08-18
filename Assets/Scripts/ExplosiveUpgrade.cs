using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExplosiveUpgrade", menuName = "Upgrades/New Explosive Upgrade")]
public class ExplosiveUpgrade : Upgrade
{
    public int fireratePercentageToRemove = 70;
    public int damageToAdd;
    public float explosionRadiusToAdd = 1;
    public GameObject bulletPrefab;
    public Animation updateIdleAnimation;
    public Animation updateShootAnimation;

    public override void ApplyUpgrade(Turret turret)
    {
        turret.fireRatePercentage -= fireratePercentageToRemove;
        turret.explosionRadius += explosionRadiusToAdd;
        if(bulletPrefab != null) turret.bulletPrefab = bulletPrefab;
        turret.damage += damageToAdd;
    }
}
