using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : Enemy
{
    private float _health = 40;
    public override int damagePoint => 30;
    public override float speed => 2;
    public override float health{
        get { return _health; }
        set { _health = value; }
    }
    public override float maxHealth => 40;
    public GameObject soldierPrefab;

    public override void TakeDamage(float damagePoint){
        health -= damagePoint;
        if(health <= 0 && !dead){
            dead = true;
            gameMaster.UpdateMoney(worth);
            Instantiate(soldierPrefab).GetComponent<Enemy>().setDistanceTravelled(this.getDistanceTravelled());
            Instantiate(soldierPrefab).GetComponent<Enemy>().setDistanceTravelled(this.getDistanceTravelled() - 0.5f);
            Instantiate(soldierPrefab).GetComponent<Enemy>().setDistanceTravelled(this.getDistanceTravelled() - 1f);
            Instantiate(soldierPrefab).GetComponent<Enemy>().setDistanceTravelled(this.getDistanceTravelled() - 1.5f);
            Destroy(this.gameObject);
        }
        healthBar.UpdateHealthBar(health, maxHealth);
    }
}
