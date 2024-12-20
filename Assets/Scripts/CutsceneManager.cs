using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cutscenes;
    private GameController gameController;
    void Start()
    {
        gameController =  GameObject.Find("GameController").GetComponent<GameController>();

        cutscenes[gameController.CurrentCutscene].SetActive(true);
    }
}
