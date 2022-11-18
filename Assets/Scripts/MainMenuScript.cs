using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public string sceneToLoad;

    public void LoadEasy(){
        StaticSceneClass.CrossSceneDifficulty = 10f;
        SceneManager.LoadScene(sceneToLoad);
    }
    
    public void LoadNormal(){
        StaticSceneClass.CrossSceneDifficulty = 7.5f;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadHard(){
        StaticSceneClass.CrossSceneDifficulty = 5f;
        SceneManager.LoadScene(sceneToLoad);
    }
}
