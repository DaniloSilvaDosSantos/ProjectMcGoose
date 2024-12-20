using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private MenuController menuController;

    void Start()
    {
        menuController = GameObject.Find("CanvasMenu").GetComponent<MenuController>();
    }
    public void OpenOptions()
    {
        menuController.OpenOptionsLevel();
    }
}
