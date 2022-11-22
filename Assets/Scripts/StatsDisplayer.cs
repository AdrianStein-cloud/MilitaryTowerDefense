using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsDisplayer : MonoBehaviour
{
    public TextMeshProUGUI statsTextField;

    public void DisplayStats(Turret turret){
        string result = $"<b><u>Turret Stats</u></b> \nRange: {turret.range} \nRate of fire: {turret.fireRate} \nBullet Damage: {turret.damage} \nBullet Pierce: {turret.pierce} \n";
        if(turret.isIncendiary){
            result += $"Fire Intensity: {turret.fireDamage} \n";
        }
        if(turret.tag == "Shotgun"){
            result += $"Bulletspread: {turret.bulletSpread} \nBullets per shot: {((Shotgun)turret).numberOfBulletsPerShot} \n";
        }
        statsTextField.text = result;
    }
}
