using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
 using UnityEngine.SceneManagement;

public class GameMasterScript : MonoBehaviour
{
    private Turret selectedTurret = null;
    private int money = 500;
    public TextMeshProUGUI moneyTextField;
    public Button skillTreeButton;
    public Canvas shotgunSkillTreeCanvas;
    public Canvas mainCanvas;
    public bool skillTreeOpen = false;
    public bool towerIsBeingPlaced = false;
    public BuyTower buyTowerSelected;
    public GameObject cashInputField;
    public void SetSelectedTurret(Turret turret){
        if(towerIsBeingPlaced){
            return;
        }
        if(selectedTurret != null){
            selectedTurret.RangeVisible(false);
        }
        if(selectedTurret == turret && turret is not null){
            selectedTurret.isSelected = false;
            selectedTurret.RangeVisible(false);
            selectedTurret = null;
            skillTreeButton.gameObject.SetActive(false);
        }
        else{
            if(selectedTurret is not null){
                selectedTurret.RangeVisible(false);
                selectedTurret.isSelected = false;
            }
            selectedTurret = turret;
            if(selectedTurret is not null){
                selectedTurret.RangeVisible(true);
                selectedTurret.isSelected = true;
                skillTreeButton.gameObject.SetActive(true);
            }
            else{
                skillTreeButton.gameObject.SetActive(false);
            }
        }
    }

    void Update(){
        if(towerIsBeingPlaced){
            skillTreeButton.gameObject.SetActive(false);
        }
        if(cashInputField.GetComponent<TMP_InputField>().text != null){
            if(int.TryParse(cashInputField.GetComponent<TMP_InputField>().text, out int result))
            money = result;
        }
    }

    public Turret GetSelectedTurret(){
        return selectedTurret;
    }

    public void AddUpgrade(Upgrade upgrade){
        upgrade.ApplyUpgrade(selectedTurret);
        selectedTurret.UpdateGraphics();
        UpdateMoney(-upgrade.cost);
    }

    public void UpdateMoney(int newMoney){
        this.money += newMoney;
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
            Time.timeScale = 1;
            shotgunSkillTreeCanvas.gameObject.SetActive(false);
        }
        else if(selectedTurret is Shotgun){
            skillTreeOpen = true;
            shotgunSkillTreeCanvas.gameObject.SetActive(true);
            foreach(SkillTreeButtonScript script in shotgunSkillTreeCanvas.gameObject.GetComponentsInChildren<SkillTreeButtonScript>()){
                script.DisableInteractable();
            }
            foreach(SkillTreeButtonScript button in selectedTurret.upgrades.ToArray()){
                button.UnlockButtons();
            }
            Time.timeScale = 0;
        }
    }

    public void RestartLevel(){
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
