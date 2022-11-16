using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeButtonScript : MonoBehaviour
{
    public Button[] buttonsToUnlock;
    private Button thisButton;
    public GameMasterScript gameMaster;
    public bool isStarter = false;

    void Awake(){
        thisButton = this.gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(UnlockButtons);
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMasterScript>();
    }

    public void UnlockButtons(){
        thisButton.interactable = false;
        foreach(Button button in buttonsToUnlock){
            button.interactable = true;
        }
        Turret turret = gameMaster.GetSelectedTurret();
        turret.upgrades.Add(this);
    }

    public void DisableInteractable(){
        if(!isStarter)
            thisButton.interactable = false;
    }
}
