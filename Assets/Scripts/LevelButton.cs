using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private bool[] starsCollectedPastLevel = {false, false, false};
    [SerializeField] private bool[] starsCollected = {false, false, false};
    [SerializeField] private GameObject[] starsIcon = {null, null, null}; 
    [SerializeField] private int totalStars = 0;
    [SerializeField] private Sprite starOn;
    [SerializeField] private Sprite starOff;
    [SerializeField] private bool isLocked;
    [SerializeField] private GameObject lockImage;
    private GameController gameController;
    private SceneTransitionManager sceneTransitionManager;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        sceneTransitionManager = GameObject.Find("SceneTransitionManager").GetComponent<SceneTransitionManager>();

        isLocked = true;
        
        switch(gameObject.name)
        {
            case "Level01":
                isLocked = false;
                starsCollected = gameController.getLevelStars("Level01");
                break;
            case "Level02":
                starsCollectedPastLevel = gameController.getLevelStars("Level01");
                starsCollected = gameController.getLevelStars("Level02");
                break;
            case "Level03":
                starsCollectedPastLevel = gameController.getLevelStars("Level02");
                starsCollected = gameController.getLevelStars("Level03");
                break;
            case "Level04":
                starsCollectedPastLevel = gameController.getLevelStars("Level03");
                starsCollected = gameController.getLevelStars("Level04");
                break;
            case "Level05":
                starsCollectedPastLevel = gameController.getLevelStars("Level04");
                starsCollected = gameController.getLevelStars("Level05");
                break;
            case "Level06":
                starsCollectedPastLevel = gameController.getLevelStars("Level05");
                starsCollected = gameController.getLevelStars("Level06");
                break;
            case "Level07":
                starsCollectedPastLevel = gameController.getLevelStars("Level06");
                starsCollected = gameController.getLevelStars("Level07");
                break;
            case "Level08":
                starsCollectedPastLevel = gameController.getLevelStars("Level07");
                starsCollected = gameController.getLevelStars("Level08");
                break;
            case "Level09":
                starsCollectedPastLevel = gameController.getLevelStars("Level08");
                starsCollected = gameController.getLevelStars("Level09");
                break;
        }

        for(int i = 0; i < starsCollected.Length; i++)
        {
            if(starsCollectedPastLevel[i] == true)
            {
                isLocked = false;
            } 
        }

        if(isLocked)
        {
            GetComponent<Button>().enabled = false;
            lockImage.SetActive(true);
        }
        else
        {
            for(int i = 0; i < starsCollected.Length; i++)
            {
                if(starsCollected[i] == true)
                {
                    totalStars++;
                } 
            }

            GetComponent<Button>().enabled = true;
            lockImage.SetActive(false);
        }

    }

    void Update()
    {
        if(!isLocked)
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
    }

    public void LoadLevel()
    {
        gameController.CurrentLevel = gameObject.name;
        sceneTransitionManager.TransitionToScene("LevelRoom");
    }
}
