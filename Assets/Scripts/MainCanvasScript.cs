using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainCanvasScript : MonoBehaviour
{
    public TextMeshProUGUI healthTextField;
    public TextMeshProUGUI moneyTextField;
    public Image cashImage;
    public Image heartImage;
    public Button optionsButton;
    public Button skillTreeButton;
    public Sprite whiteCashImage;
    public Sprite blackCashImage;
    public Sprite whiteOptionsImage;
    public Sprite blackOptionsImage;
    public Sprite blackHeartImage;
    public Sprite whiteHeartImage;
    private bool colorsFlipped = false;

    public void FlipColors(){
        if(!colorsFlipped){
            colorsFlipped = true;
            healthTextField.color = Color.white;
            moneyTextField.color = Color.white;
            cashImage.sprite = whiteCashImage;
            heartImage.sprite = whiteHeartImage;
            optionsButton.GetComponent<Image>().sprite = whiteOptionsImage;
            skillTreeButton.GetComponent<Image>().color = Color.white;
            skillTreeButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            skillTreeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Close Skill Tree";
        }
        else{
            colorsFlipped = false;
            healthTextField.color = Color.black;
            moneyTextField.color = Color.black;
            cashImage.sprite = blackCashImage;
            heartImage.sprite = blackHeartImage;
            optionsButton.GetComponent<Image>().sprite = blackOptionsImage;
            skillTreeButton.GetComponent<Image>().color = Color.black;
            skillTreeButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            skillTreeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Skill Tree & Upgrades";
        }
    }
}
