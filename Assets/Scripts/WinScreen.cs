//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private bool[] starsCollected = {false, false, false};
    [SerializeField] private GameObject[] starsIcon = {null, null, null}; 
    [SerializeField] private int totalStars = 0;
    [SerializeField] private Sprite starOn;
    [SerializeField] private Sprite starOff;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private GameObject number;
    [SerializeField] private Sprite[] numberSprites;
    private GameController gameController;
    private SceneTransitionManager sceneTransitionManager;
    private Radio radio;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        sceneTransitionManager = GameObject.Find("SceneTransitionManager").GetComponent<SceneTransitionManager>();
        radio = GameObject.Find("Radio").GetComponent<Radio>();

        starsCollected = gameController.getLevelStars(gameController.CurrentLevel);
        //Debug.Log(string.Join(", ", starsCollected));

        for(int i = 0; i < starsCollected.Length; i++)
        {
            if(starsCollected[i] == true)
            {
                totalStars++;
            } 
        }

        UnityEngine.UI.Image numberImage = number.GetComponent<UnityEngine.UI.Image>();

        switch (gameController.CurrentLevel)
        {
            case "Level01":
                numberImage.sprite = numberSprites[0];
                break;
            case "Level02":
                numberImage.sprite = numberSprites[1];
                break;
            case "Level03":
                numberImage.sprite = numberSprites[2];
                break;
            case "Level04":
                numberImage.sprite = numberSprites[3];
                break;
            case "Level05":
                numberImage.sprite = numberSprites[4];
                break;
            case "Level06":
                numberImage.sprite = numberSprites[5];
                break;
            case "Level07":
                numberImage.sprite = numberSprites[6];
                break;
            case "Level08":
                numberImage.sprite = numberSprites[7];
                break;
            case "Level09":
                numberImage.sprite = numberSprites[8];
                break;
        }
    }

    void Update()
    {
        for (int i = 0; i < starsCollected.Length; i++)
        {
            UnityEngine.UI.Image starImage = starsIcon[i].GetComponent<UnityEngine.UI.Image>();
                
            if(i < totalStars)
            {
                starImage.sprite = starOn;
            }
            else
            {
                starImage.sprite = starOff;
            }
        }
    }

    public void GoToMainMenu()
    {
        radio.StopMusic();
        gameController.CurrentLevel = null;
        sceneTransitionManager.TransitionToScene("MainMenu");
    }

    public void LoadNextLevel()
    {
        switch(gameController.CurrentLevel)
        {
            case "Level01":
                gameController.CurrentLevel = "Level02";
                sceneTransitionManager.TransitionToScene("LevelRoom");
                break;
            case "Level02":
                gameController.CurrentLevel = "Level03";
                sceneTransitionManager.TransitionToScene("LevelRoom");
                break;
            case "Level03":
                gameController.CurrentLevel = "Level04";
                radio.StopMusic();
                gameController.CurrentCutscene = 1;
                sceneTransitionManager.TransitionToScene("Cutscene");
                break;
            case "Level04":
                gameController.CurrentLevel = "Level05";
                sceneTransitionManager.TransitionToScene("LevelRoom");
                break;
            case "Level05":
                gameController.CurrentLevel = "Level06";
                sceneTransitionManager.TransitionToScene("LevelRoom");
                break;
            case "Level06":
                gameController.CurrentLevel = "Level07";
                radio.StopMusic();
                gameController.CurrentCutscene = 2;
                sceneTransitionManager.TransitionToScene("Cutscene");
                break;
            case "Level07":
                gameController.CurrentLevel = "Level08";
                sceneTransitionManager.TransitionToScene("LevelRoom");
                break;
            case "Level08":
                gameController.CurrentLevel = "Level09";
                sceneTransitionManager.TransitionToScene("LevelRoom");
                break;
            case "Level09":
                gameController.CurrentLevel = null;
                radio.StopMusic();
                gameController.CurrentCutscene = 3;
                sceneTransitionManager.TransitionToScene("Cutscene");
                break;
        }
    }
}

