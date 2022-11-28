using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Base : MonoBehaviour
{
    public int health = 100;
    public Canvas gameOverCanvas;
    public TextMeshProUGUI roundTextField;
    public WaveSpawner waveSpawner;

    public void DamageBase(int damagePoint){
        health -= damagePoint;
        healthDisplay.text = "" + health;
        if(health <= 0){
            gameOverCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            roundTextField.text = "Round " + waveSpawner.NextRound;
        }
    }

    void Start(){
        health = StaticSceneClass.CrossSceneStartLives;
        DamageBase(0);
        gameOverCanvas.gameObject.SetActive(false);
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
