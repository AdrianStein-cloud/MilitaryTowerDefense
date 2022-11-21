using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;

public class Enemy : MonoBehaviour, IComparable<Enemy>
{
    public float health;
    public float maxHealth;
    public int damagePoint;
    public float speed;
    public HealthBar healthBar;

    private PathCreator pathCreator;
    public int worth = 10;
    
    float distanceTravelled;
    private bool burning = false;
    private float fireIntensity = 0;

    private ParticleSystem ps;
    public GameMasterScript gameMaster;

    public bool dead = false;
    public List<Turret> listOfBurningTurrets = new List<Turret>();

    public void setDistanceTravelled(float distance){
        distanceTravelled = distance;
    }

    public float getDistanceTravelled(){
        return distanceTravelled;
    }


    void Awake(){
        pathCreator = GameObject.Find("Path").GetComponent<PathCreator>();
        ps = GetComponentInChildren<ParticleSystem>();
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMasterScript>();
    }

    private void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = Quaternion.LookRotation(pathCreator.path.GetNormalAtDistance(distanceTravelled));
        
        transform.eulerAngles = new Vector3(
            0,
            0,
            transform.eulerAngles.x
        );

        healthBar.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
        healthBar.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.x * -1);

        if(burning){
            TakeDamage(Time.deltaTime * fireIntensity);
            ps.transform.eulerAngles = new Vector3(transform.eulerAngles.x - 90, 90, -90);
            ps.transform.position = new Vector3(transform.position.x,transform.position.y - 0.2f,transform.position.z);
        }
    }

    public int GetEnemyDamagePoint()
    {
        return damagePoint;
    }

    public virtual void TakeDamage(float damagePoint){
        health -= damagePoint;
        if(health <= 0 && !dead){
            dead = true;
            gameMaster.UpdateMoney(worth);
            Destroy(this.gameObject);
        }
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    public void Burn(float fireDamage, Turret turret){
        burning = true;
        if(!listOfBurningTurrets.Contains(turret)){
            fireIntensity += fireDamage;
            print("FireIntensity: " + fireIntensity);
            listOfBurningTurrets.Add(turret);
        }
        ps.Play();
    }

    public int CompareTo(Enemy other)
    {
        if(this.distanceTravelled == other.distanceTravelled)
            return 0;
        else if(this.distanceTravelled > other.distanceTravelled)
            return -1;
        else return 1;
    }
}
