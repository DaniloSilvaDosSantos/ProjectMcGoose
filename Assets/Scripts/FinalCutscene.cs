using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCutscene : MonoBehaviour
{
    private SceneTransitionManager sceneTransitionManager;
    private string sceneToLoad;
    void Start()
    {
        sceneTransitionManager = GameObject.Find("SceneTransitionManager").GetComponent<SceneTransitionManager>();
        sceneToLoad = "MainMenu";

        Invoke("GoToMainMenu", 4f);
        
    }

    void GoToMainMenu()
    {
        sceneTransitionManager.TransitionToScene(sceneToLoad);
    }

}
