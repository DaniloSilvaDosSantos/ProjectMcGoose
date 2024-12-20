//using System.Collections;
//using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
//using UnityEngine.UI;
using UnityEngine;

public class StartsHud : MonoBehaviour
{
    //private int currentLevel = -1;
    [SerializeField] GameObject[] stars = {null, null, null}; 
    public bool[] starsAll = {false, false, false}; //0 - conclusão da fase, 1 - tempo, 2 - atirar na estrela da fase
    public int totalStars = 0;
    [SerializeField] private Sprite starOn;
    [SerializeField] private Sprite starOff;

    void Start()
    {
        //
    }

    void Update()
    {
        totalStars = 0;

        for (int i = 0; i < starsAll.Length; i++)
        {
            if (starsAll[i] == true){
                totalStars ++;
            }
        }

        for (int i = 0; i < starsAll.Length; i++)
        {
            UnityEngine.UI.Image starImage = stars[i].GetComponent<UnityEngine.UI.Image>();
            
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
