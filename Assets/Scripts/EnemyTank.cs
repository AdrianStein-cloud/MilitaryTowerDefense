using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : Enemy
{
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
