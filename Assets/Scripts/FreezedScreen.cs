//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class FreezedScreen : MonoBehaviour
{
    [SerializeField] private GameObject retryButton;
    [SerializeField] private GameObject mainMenuButton;
    private GameController gameController;
    private SceneTransitionManager sceneTransitionManager;
    private Radio radio;
    
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        sceneTransitionManager = GameObject.Find("SceneTransitionManager").GetComponent<SceneTransitionManager>();
        radio = GameObject.Find("Radio").GetComponent<Radio>();
    }

    public void GoToMainMenu()
    {
        radio.StopMusic();
        gameController.CurrentLevel = null;
        sceneTransitionManager.TransitionToScene("MainMenu");
    }

    public void reloadLevel()
    {
        sceneTransitionManager.TransitionToScene("LevelRoom");
    }
}
