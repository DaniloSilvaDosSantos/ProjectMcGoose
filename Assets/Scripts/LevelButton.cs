using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
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

        starsCollected = gameController.getLevelStars(gameObject.name);
        //Debug.Log(string.Join(", ", starsCollected));

        isLocked = true;
        for(int i = 0; i < starsCollected.Length; i++)
        {
            if(starsCollected[i] == true){
                isLocked = false;
                totalStars++;
            } 
        }

        if(gameObject.name == "Level01")
        {
            isLocked = false;
        }
        else
        {
            isLocked = true;
        }

        if(isLocked)
        {
            GetComponent<Button>().enabled = false;
            lockImage.SetActive(true);
        }
        else
        {
            GetComponent<Button>().enabled = true;
            lockImage.SetActive(false);
        }

    }

    // Update is called once per frame
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
