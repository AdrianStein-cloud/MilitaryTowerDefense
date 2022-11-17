using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Base : MonoBehaviour
{
    public int health = 100;

    public void DamageBase(int damagePoint){
        health -= damagePoint;
        healthDisplay.text = "" + health;
    }

    void Start(){
        DamageBase(0);
    }

    [SerializeField]
    private TextMeshProUGUI healthDisplay;

    void OnTriggerEnter2D(Collider2D enemyCollider){
        if(enemyCollider.tag == "Enemy"){
            DamageBase(enemyCollider.GetComponent<Enemy>().GetEnemyDamagePoint());
            Destroy(enemyCollider.gameObject);
        }
    }
}
