using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezedScreen : MonoBehaviour
{
    [SerializeField] private GameObject retryButton;
    [SerializeField] private GameObject mainMenuButton;
    private GameController gameController;
    private SceneTransitionManager sceneTransitionManager;
    
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        sceneTransitionManager = GameObject.Find("SceneTransitionManager").GetComponent<SceneTransitionManager>();
    }

    public void GoToMainMenu()
    {
        gameController.CurrentLevel = null;
        sceneTransitionManager.TransitionToScene("MainMenu");
    }

    public void reloadLevel()
    {
        sceneTransitionManager.TransitionToScene("LevelRoom");
    }
}
