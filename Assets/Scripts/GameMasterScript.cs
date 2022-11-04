using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class GameMasterScript : MonoBehaviour
{
    private Turret selectedTurret = null;
    private int money = 0;
    public TextMeshProUGUI moneyTextField;
    public Button skillTreeButton;
    public Canvas shotgunSkillTreeCanvas;
    public Canvas mainCanvas;
    public bool skillTreeOpen = false;
    public void SetSelectedTurret(Turret turret){
        if(selectedTurret == turret && turret is not null){
            selectedTurret = null;
            turret.rangeSprite.gameObject.SetActive(false);
        }
        else{
            if(selectedTurret is not null){
            selectedTurret.rangeSprite.gameObject.SetActive(false);
            }
            selectedTurret = turret;
            if(selectedTurret is not null){
            selectedTurret.rangeSprite.gameObject.SetActive(true);
            }
        }
        if(selectedTurret is not null){
            skillTreeButton.gameObject.SetActive(true);
        }
        else{
            skillTreeButton.gameObject.SetActive(false);
        }
    }

    public Turret GetSelectedTurret(){
        return selectedTurret;
    }

    public void AddUpgrade(Upgrade upgrade){
        upgrade.ApplyUpgrade(selectedTurret);
        TextMeshProUGUI buttonTextField = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>();
        buttonTextField.text = upgrade.title + " " + upgrade.cost + "$";
        selectedTurret.UpdateGraphics();
        UpdateMoney(-upgrade.cost);
    }

    public void UpdateMoney(int newMoney){
        this.money += newMoney;
        print("Money updated");
        moneyTextField.text = "" + money;
    }

    public int GetMoney() => money;
    
    void Start(){
        SetSelectedTurret(null);
        moneyTextField.text = "" + money;
        shotgunSkillTreeCanvas.gameObject.SetActive(false);
    }

    public void OpenSkillTree(){
        if(skillTreeOpen){
            skillTreeOpen = false;
            shotgunSkillTreeCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else if(selectedTurret is Shotgun){
            skillTreeOpen = true;
            shotgunSkillTreeCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
