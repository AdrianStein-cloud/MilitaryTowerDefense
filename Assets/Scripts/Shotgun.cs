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
            GameObject effectInstance = (GameObject)Instantiate(flashEffect, firePoint.position, Quaternion.LookRotation(firePoint.position - startOfGun.position, Vector3.up));
            ParticleSystem.ShapeModule shapeModule = effectInstance.GetComponent<ParticleSystem>().shape;
            shapeModule.angle = bulletSpread * 100;
            Destroy(effectInstance, 1);

            GameObject shellEffectInstance = (GameObject)Instantiate(bulletShellEffect, startOfGun.position, Quaternion.LookRotation(Quaternion.Euler(0, 0, -90) * (firePoint.position - startOfGun.position), Vector3.down));
            Destroy(shellEffectInstance, 1);
            
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
