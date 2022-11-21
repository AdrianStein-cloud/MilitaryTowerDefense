using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyTower : MonoBehaviour
{
    public GameObject towerPrefab;
    private GameObject tower;
    public bool placingTower = false;
    private Turret towerScript;
    public GameMasterScript gameMasterScript;
    public int towerCost;
    public TextMeshProUGUI costTextField;
    public string shortcut;

    public void InstantiateTower(){
        if(gameMasterScript.GetMoney() >= towerCost){
            if(gameMasterScript.buyTowerSelected is not null && gameMasterScript.buyTowerSelected.placingTower){
                gameMasterScript.buyTowerSelected.DestroyTower();
            }
            gameMasterScript.buyTowerSelected = this;
            tower = (GameObject)Instantiate(towerPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.Euler(0, 0, 0));
            towerScript = tower.GetComponent<Turret>();
            towerScript.canShoot = false;
            towerScript.RangeVisible(true);
            towerScript.SetSelectedTurret(towerScript);
            towerScript.IsBeingPlaced(true);
            placingTower = true;
        }
    }

    public void DestroyTower(){
        placingTower = false;
        Destroy(tower);
    }

    void Update(){
        if(placingTower){
            if (Input.GetKey("escape") || Input.GetMouseButtonDown(1))
            {
                DestroyTower();
            }
            Vector3 mouseCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tower.gameObject.transform.position = new Vector3(mouseCoords.x, mouseCoords.y, 0);
            if (Input.GetMouseButtonDown(0) && towerScript.canBePlaced){
                gameMasterScript.UpdateMoney(-towerCost);
                placingTower = false;
                towerScript.canShoot = true;
                towerScript.IsBeingPlaced(false);
                towerScript.SetSelectedTurret(towerScript);
            }
        }
        if (Input.GetKey(shortcut))
        {
            InstantiateTower();
        }
    }

    void Start(){
        costTextField.text = towerCost + "$";
    }
}
