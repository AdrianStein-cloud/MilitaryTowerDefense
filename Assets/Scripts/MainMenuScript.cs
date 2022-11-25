using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject DifficultySelector;
    public GameObject MapSelector;

    public void GoToMapSelector(){
        MapSelector.SetActive(true);
    }

    public void BackToMainMenu(){
        DifficultySelector.SetActive(false);
        MapSelector.SetActive(false);
    }

    public void LoadEasy(){
        StaticSceneClass.CrossSceneDifficulty = 10f;
        StaticSceneClass.CrossSceneStartLives = 100;
        LoadScene();
    }
    
    public void LoadNormal(){
        StaticSceneClass.CrossSceneDifficulty = 8f;
        StaticSceneClass.CrossSceneStartLives = 75;
        LoadScene();
    }

    public void LoadHard(){
        StaticSceneClass.CrossSceneDifficulty = 6f;
        StaticSceneClass.CrossSceneStartLives = 50;
        LoadScene();
    }

    public void LoadExtreme(){
        StaticSceneClass.CrossSceneDifficulty = 5f;
        StaticSceneClass.CrossSceneStartLives = 1;
        LoadScene();
    }

    private void LoadScene(){
        SceneManager.LoadScene(sceneToLoad);
    }

    public void SelectDifficulty(){
        DifficultySelector.SetActive(true);
    }

    void Awake(){
        DifficultySelector.SetActive(false);
        MapSelector.SetActive(false);
    }

    public void CloseDifficultySelector(){
        DifficultySelector.SetActive(false);
    }
}
