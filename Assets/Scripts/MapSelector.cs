using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelector : MonoBehaviour
{
    public string nameOfScene;
    private MainMenuScript mainMenuScript;

    void Awake(){
        mainMenuScript = gameObject.GetComponentInParent<MainMenuScript>();
    }

    public void LoadScene(){
        mainMenuScript.sceneToLoad = nameOfScene;
        mainMenuScript.SelectDifficulty();
    }
}
