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
    private int money = 600;
    public TextMeshProUGUI moneyTextField;
    public Button skillTreeButton;
    public Button sellTowerButton;
    public Canvas shotgunSkillTreeCanvas;
    public Canvas minigunSkillTreeCanvas;
    public Canvas rifleSkillTreeCanvas;
    public Canvas flameThrowerSkillTreeCanvas;
    public Canvas mainCanvas;
    public bool skillTreeOpen = false;
    public bool towerIsBeingPlaced = false;
    public BuyTower buyTowerSelected;
    public GameObject cashInputField;
    public GameObject pauseMenu;
    public StatsDisplayer statsDisplayer;
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
            UpdateTowerButtons(false);
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
                UpdateTowerButtons(true);
            }
            else{
                UpdateTowerButtons(false);
            }
        }
    }

    public void UpdateTowerButtons(bool isEnabled){
        skillTreeButton.gameObject.SetActive(isEnabled);
        sellTowerButton.gameObject.SetActive(isEnabled);
        if(isEnabled){
            UpdateSellPrice();
        }
    }

    public void UpdateSellPrice(){
        sellTowerButton.GetComponentInChildren<TextMeshProUGUI>().text = "Sell\n(" + selectedTurret.sellPrice + "$)";
    }

    void Update(){
        if(towerIsBeingPlaced){
            UpdateTowerButtons(false);
        }
        if(cashInputField.GetComponent<TMP_InputField>().text != null){
            if(int.TryParse(cashInputField.GetComponent<TMP_InputField>().text, out int result))
                money = result;
        }
        if(skillTreeOpen){
            if (Input.GetKey("escape"))
            {
                OpenSkillTree();
            }
        }
    }

    public Turret GetSelectedTurret(){
        return selectedTurret;
    }

    public void AddUpgrade(Upgrade upgrade){
        upgrade.ApplyUpgrade(selectedTurret);
        selectedTurret.UpdateGraphics();
        statsDisplayer.DisplayStats(selectedTurret);
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
        Time.timeScale = 1;
        statsDisplayer.gameObject.SetActive(false);
    }

    public void OpenSkillTree(){
        if(skillTreeOpen){
            skillTreeOpen = false;
            Time.timeScale = 1;
            shotgunSkillTreeCanvas.gameObject.SetActive(false);
            minigunSkillTreeCanvas.gameObject.SetActive(false);
            rifleSkillTreeCanvas.gameObject.SetActive(false);
            flameThrowerSkillTreeCanvas.gameObject.SetActive(false);
            sellTowerButton.gameObject.SetActive(true);
            statsDisplayer.gameObject.SetActive(false);
        }
        else if(selectedTurret.tag == "Shotgun"){
            LoadSkillTree(shotgunSkillTreeCanvas);
        }
        else if(selectedTurret.tag == "Minigun"){
            LoadSkillTree(minigunSkillTreeCanvas);
        }
        else if(selectedTurret.tag == "Rifle"){
            LoadSkillTree(rifleSkillTreeCanvas);
        }
        else if(selectedTurret.tag == "FlameThrower"){
            LoadSkillTree(flameThrowerSkillTreeCanvas);
        }
    }

    public void LoadSkillTree(Canvas skillTreeCanvas){
        skillTreeOpen = true;
        skillTreeCanvas.gameObject.SetActive(true);
        sellTowerButton.gameObject.SetActive(false);
        foreach(SkillTreeButtonScript script in skillTreeCanvas.gameObject.GetComponentsInChildren<SkillTreeButtonScript>()){
            script.DisableInteractable();
        }
        foreach(SkillTreeButtonScript button in selectedTurret.upgrades.ToArray()){
            button.UnlockButtons();
        }
        Time.timeScale = 0;
        statsDisplayer.gameObject.SetActive(true);
        statsDisplayer.DisplayStats(selectedTurret);
    }

    public void RestartLevel(){
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void PauseGame(){
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void UnpauseGame(){
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void ExitLevel(){
        SceneManager.LoadScene("Main Menu");
    }

    public void SellSelectedTower(){
        UpdateMoney(selectedTurret.sellPrice);
        Destroy(selectedTurret.gameObject);
        SetSelectedTurret(null);
    }
}
