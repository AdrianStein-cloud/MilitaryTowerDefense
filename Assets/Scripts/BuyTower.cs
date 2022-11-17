using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTower : MonoBehaviour
{
    public GameObject towerPrefab;
    private GameObject tower;
    public bool placingTower = false;
    private Turret towerScript;
    public GameMasterScript gameMasterScript;

    public void InstantiateTower(){
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

    public void DestroyTower(){
        placingTower = false;
        Destroy(tower);
    }

    void Update(){
        if(placingTower){
            Vector3 mouseCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tower.gameObject.transform.position = new Vector3(mouseCoords.x, mouseCoords.y, 0);
            if (Input.GetMouseButtonDown(0) && towerScript.canBePlaced){
                placingTower = false;
                towerScript.canShoot = true;
                towerScript.IsBeingPlaced(false);
            }
        }
    }
}
