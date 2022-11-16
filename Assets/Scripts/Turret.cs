using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Linq;


public class Turret : MonoBehaviour
{
    public Transform target;

    [Header("Turret Skills")]
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float damage = 20;
    public float fireDamage = 0;
    public float bulletLifeTime = 20;
    public float bulletSpeed = 70;
    public bool canShoot = true;
    public bool isIncendiary = false;

    [Header("Unity Setup Fields")]
    public Transform partToRotate;
    public Transform startOfGun;
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform rangeSprite;
    public GameObject flashEffect;
    private GameMasterScript gameMaster;

    public bool isSelected = false;

    public List<SkillTreeButtonScript> upgrades = new List<SkillTreeButtonScript>();

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
        UpdateRange();
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMasterScript>();
    }

    public void UpdateRange(){
        rangeSprite.localScale = new Vector3(range * 2.15f, range * 2.15f, 1);
    }

    void UpdateTarget()
    {
        List<Enemy> enemies = GameObject.FindGameObjectsWithTag("Enemy").Select(x => x.GetComponent<Enemy>()).ToList();
        enemies.Sort();

        Enemy enemyInRange = null;

        foreach(Enemy enemy in enemies){
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy <= range){
                enemyInRange = enemy;
                break;
            }
        }

        if(enemyInRange != null){
            target = enemyInRange.gameObject.transform;
        }
        else{
            target = null;
        }
    }

    void Update()
    {
        if(target == null)
        {
            return;
        }

        Vector3 dir = target.position - transform.position;
        Vector3 rotatedVectorDir = Quaternion.Euler(0, 0, 180) * dir;
        Quaternion lookRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorDir);
        if(partToRotate != null)
        {
            Quaternion rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed);
            partToRotate.rotation = rotation;
        }

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    public virtual void Shoot()
    {
        if (canShoot)
        {
            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            bullet.damage = damage;
            bullet.speed = bulletSpeed;
            bullet.lifetime = bulletLifeTime;

            if (bullet != null)
            {
                bullet.Seek(firePoint.position - startOfGun.position);
            }
        }
    }

    public virtual void UpdateGraphics(){
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
        Handles.DrawWireDisc(transform.position, new Vector3(0, 0, 0) , range);
    }

    public void OnMouseDown(){
        SpriteRenderer spriteRenderer = rangeSprite.GetComponent<SpriteRenderer>();
        if(!gameMaster.skillTreeOpen){
            if(isSelected){
                spriteRenderer.enabled = false;
                isSelected = false;
                gameMaster.SetSelectedTurret(null);
            }
            else{
                spriteRenderer.enabled = true;
                isSelected = true;
                gameMaster.SetSelectedTurret(this);
            }
        }
        
    }
}
