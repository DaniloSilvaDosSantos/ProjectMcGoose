using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionController : MonoBehaviour
{
    [SerializeField] private GameObject[] levelsButtons;

    public void DeactivateButtons()
    {
        for(int i = 0; i < levelsButtons.Length; i++)
        {
            levelsButtons[i].GetComponent<Button>().interactable = false;
        }
    }
}
