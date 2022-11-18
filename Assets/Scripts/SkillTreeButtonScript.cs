using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTreeButtonScript : MonoBehaviour
{
    public Button[] buttonsToUnlock;
    private Button thisButton;
    public GameMasterScript gameMaster;
    public bool isStarter = false;
    private TextMeshProUGUI textField;
    public Upgrade upgrade;

    void Awake(){
        thisButton = this.gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(AddUpgrade);
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMasterScript>();
        textField = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        textField.text = upgrade.title + " \n(" + upgrade.cost + "$)";
    }

    public void UnlockButtons(){
        ChangeColorOfDisabledButton(Color.green);
        thisButton.interactable = false;
        foreach(Button button in buttonsToUnlock){
            button.interactable = true;
        }
    }

    public void AddUpgrade(){
        if(upgrade.cost <= gameMaster.GetMoney()){
            gameMaster.AddUpgrade(upgrade);
            Turret turret = gameMaster.GetSelectedTurret();
            turret.AddUpgrade(this);
            UnlockButtons();
        }
    }

    private void ChangeColorOfDisabledButton(Color color){
        var colors = thisButton.colors;
        colors.disabledColor = color;
        thisButton.colors = colors;
    }

    public void DisableInteractable(){
        if(!isStarter){
            thisButton.interactable = false;
            ChangeColorOfDisabledButton(Color.gray);
        }
        else
            thisButton.interactable = true;
    }
}
