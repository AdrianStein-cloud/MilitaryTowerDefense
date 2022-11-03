using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Turret
{
    public int numberOfBulletsPerShot = 7;
    public SpriteRenderer spriteRenderer;

    [SerializeField]
    public Sprite shotgunnerDefault;
    [SerializeField]
    public Sprite shotgunnerIncendiary;

    public float bulletSpread = 0.5f;

    public override void UpdateGraphics(){
        if(isIncendiary){
            spriteRenderer.sprite = shotgunnerIncendiary;
        }
        else{
            spriteRenderer.sprite = shotgunnerDefault;
        }
    }

    public override void Shoot()
    {
        if (canShoot)
        {
            for(int i = 0; i < numberOfBulletsPerShot; i++){
                GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Bullet bullet = bulletGO.GetComponent<Bullet>();
                bullet.damage = damage;
                bullet.speed = bulletSpeed;
                bullet.lifetime = bulletLifeTime;
                bullet.incendiary = isIncendiary;
                bullet.fireDamage = fireDamage;
                if (bullet != null)
                {
                bullet.Seek((firePoint.position - startOfGun.position) + new Vector3(Random.Range(-bulletSpread, bulletSpread), 0, 0));
                }
            }
        }
    }
}
