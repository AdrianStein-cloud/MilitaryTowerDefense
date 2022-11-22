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

            var direction = firePoint.position - startOfGun.position;

            float increase = bulletSpread / numberOfBulletsPerShot;

            for(int i = 0; i < numberOfBulletsPerShot; i++){
                GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Bullet bullet = bulletGO.GetComponent<Bullet>();
                bullet.damage = damage;
                bullet.speed = bulletSpeed;
                bullet.lifetime = bulletLifeTime;
                bullet.incendiary = isIncendiary;
                bullet.fireDamage = fireDamage;
                bullet.owner = this;
                
                if (bullet != null)
                {
                    bullet.Seek(Quaternion.Euler(0, 0, (i % 2 == 0 ? 1 : -1) * increase * Mathf.Ceil(i/2f) * 180) * direction);
                }
            }
        }
    }

    public override void RotateTurret(){
        Vector3 dir = (Vector3)target - transform.position;
        Vector3 rotatedVectorDir = Quaternion.Euler(0, 0, 180) * dir;
        Quaternion lookRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorDir);
        if(partToRotate != null)
        {
            Quaternion rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed);
            partToRotate.rotation = rotation;

            if (fireCountdown <= 0f && Quaternion.Angle(partToRotate.rotation, lookRotation) < 10)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }
    }
}
