using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Linq;


public class Turret : MonoBehaviour
{
    public Vector3? targetPosition;
    public Enemy target;

    [Header("Turret Skills")]
    public float range = 15f;
    public float fireRate = 1f;
    public float fireRatePercentage = 100;
    public float damage = 20;
    public float fireDamage = 0;
    public bool fireToChildren = false;
    public float explosionRadius = 0;
    public float bulletLifeTime = 20;
    public float bulletSpeed = 70;
    public bool canShoot = true;
    public bool isIncendiary = false;
    public float bulletSpread = 0.2f;
    public int sellPrice;
    public bool canOnlyShootWhenLookingAtEnemy = false;
    public int pierce = 1;
    public bool aimForStrong = false;


    [Header("Unity Setup Fields")]
    public Transform partToRotate;
    public Transform startOfGun;
    public float turnSpeed = 500f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform rangeSprite;
    public GameObject flashEffect;
    public GameObject bulletShellEffect;
    private GameMasterScript gameMaster;
    public Animator animator;
    public float fireCountdown = 0f;
    public bool isSelected = false;
    public bool isBeingPlaced = false;
    public bool canBePlaced = true;
    public float bulletOffset = 0f;

    public List<SkillTreeButtonScript> upgrades = new List<SkillTreeButtonScript>();

    public void UpdatePierce(int change){
        pierce += change;
    }

    public void UpdateDamage(int change){
        damage += change;
    }

    void Awake()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
        UpdateRange();
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMasterScript>();
    }

    public void UpdateRange(){
        if(range < 30){
            rangeSprite.localScale = new Vector3(range * 2.15f, range * 2.15f, 1);
        }
        else{
            rangeSprite.localScale = new Vector3(1, 1, 1);
        }
    }

    public void AddUpgrade(SkillTreeButtonScript upgrade){
        upgrades.Add(upgrade);
        sellPrice += upgrade.upgrade.cost/2;
        gameMaster.UpdateSellPrice();
    }

    void UpdateTarget()
    {
        List<Enemy> enemies = GameObject.FindGameObjectsWithTag("Enemy").Select(x => x.GetComponent<Enemy>()).ToList();
        enemies.Sort();
        if(aimForStrong){
            enemies.Sort(SortByStrong);
        }

        Enemy enemyInRange = null;

        foreach(Enemy enemy in enemies){
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy <= range){
                enemyInRange = enemy;
                break;
            }
        }

        if(enemyInRange != null){
            //0.11f
            targetPosition = enemyInRange.GetPositionOnPath(Vector3.Distance(transform.position, enemyInRange.transform.position) * ((bulletOffset/100f) * enemyInRange.speed));
            target = enemyInRange;
        }
        else{
            targetPosition = null;
        }
    }

    void Update()
    {
        fireCountdown -= Time.deltaTime;
        
        if(target == null || target.dead){
            UpdateTarget();
        }

        if(targetPosition == null)
        {
            return;
        }

        RotateTurret();
    }

    public virtual void RotateTurret(){
        Vector3 dir = (Vector3)targetPosition - transform.position;
        Vector3 rotatedVectorDir = Quaternion.Euler(0, 0, 180) * dir;
        Quaternion lookRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorDir);
        if(partToRotate != null)
        {
            Quaternion rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed);
            partToRotate.rotation = rotation;

            if (fireCountdown <= 0f)
            {
                if(canOnlyShootWhenLookingAtEnemy){
                    if(Quaternion.Angle(partToRotate.rotation, lookRotation) < 20){
                        Shoot();
                    }
                }
                else{
                    Shoot();
                }
                fireCountdown = 1f / (fireRate * (fireRatePercentage / 100));
            }
        }
    }

    public virtual void Shoot()
    {
        if (canShoot)
        {
            GameObject flashEffectInstance = (GameObject)Instantiate(flashEffect, firePoint.position, Quaternion.LookRotation(firePoint.position - startOfGun.position, Vector3.up));
            if(flashEffect == bulletShellEffect) //For rocket launcher
            {
                GameObject shellEffectInstance = (GameObject)Instantiate(bulletShellEffect, startOfGun.position, Quaternion.LookRotation(Quaternion.Euler(0, 0, 180) * (firePoint.position - startOfGun.position), Vector3.down));
                ParticleSystem.ShapeModule shape = shellEffectInstance.GetComponent<ParticleSystem>().shape;
                shape.angle = bulletSpread * 100;
                Destroy(flashEffectInstance, 1);
            }
            else
            {
                GameObject shellEffectInstance = (GameObject)Instantiate(bulletShellEffect, startOfGun.position, Quaternion.LookRotation(Quaternion.Euler(0, 0, -90) * (firePoint.position - startOfGun.position), Vector3.down));
                Destroy(shellEffectInstance, 1);
            }
            ParticleSystem.ShapeModule shapeModule = flashEffectInstance.GetComponent<ParticleSystem>().shape;
            shapeModule.angle = bulletSpread * 100;
            Destroy(flashEffectInstance, 1);

            if(bulletPrefab != null){
                InstantiateBullet();
            }
            else{
                target.TakeDamage(damage);
            }

            

            if(animator != null){
                animator.SetTrigger("Shoot");
            }

        }
    }

    public void InstantiateBullet(){
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.damage = damage;
        bullet.speed = bulletSpeed;
        bullet.lifetime = bulletLifeTime;
        bullet.pierce = pierce;
        bullet.owner = this;
        bullet.incendiary = isIncendiary;
        bullet.fireDamage = fireDamage;

        if (bullet != null)
        {
            bullet.Seek(firePoint.position - startOfGun.position);
        }
    }

    public virtual void UpdateGraphics(){
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
        //Handles.DrawWireDisc(transform.position, new Vector3(0, 0, 0) , range);
    }

    public void RangeVisible(bool isEnabled){
        if(rangeSprite != null){
            rangeSprite.GetComponent<SpriteRenderer>().enabled = isEnabled;
        }
    }

    public void SetSelectedTurret(Turret turret){
        gameMaster.SetSelectedTurret(turret);
    }

    public void OnMouseDown(){
        if(!gameMaster.skillTreeOpen && !gameMaster.towerIsBeingPlaced){
            if(isSelected){
                print("Tower Deselected");
                RangeVisible(false);
                SetSelectedTurret(null);
            }
            else{
                print("Tower selected");
                RangeVisible(true);
                SetSelectedTurret(this);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collider2D){
        if(isBeingPlaced){
            gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color (1, 0, 0, 1);
            canBePlaced = false;
        }
    }

    void OnTriggerExit2D(Collider2D collider2D){
        if(isBeingPlaced){
            gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color (1, 1, 1, 1);
            canBePlaced = true;
        }
    }

    public void IsBeingPlaced(bool isBeingPlaced){
        this.isBeingPlaced = isBeingPlaced;
        gameMaster.towerIsBeingPlaced = isBeingPlaced;
        gameMaster.UpdateTowerButtons(true);
    }

    static int SortByStrong(Enemy e1, Enemy e2)
    {
        if(e2.maxHealth.CompareTo(e1.maxHealth) != 0){
            return e2.maxHealth.CompareTo(e1.maxHealth);
        }
        else{
            return e2.distanceTravelled.CompareTo(e1.distanceTravelled);
        }
    }
}
