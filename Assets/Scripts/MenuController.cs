using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject thuthuluPanel;
    [SerializeField] private GameObject GerPanel;
    [SerializeField] private GameObject BlackStarPanel;
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject OpcoesPanel;
    [SerializeField] private GameObject LevelSelectionPanel;
    [SerializeField] private GameObject winScreenPanel;
    [SerializeField] private GameObject freezedScreenPanel;

    [HideInInspector] public enum MainMenuState
    {
        SplashScreenThuthulu,
        SplashScreenGer,
        SplashScreenBlackStar,
        MainMenu,
        Options,
        LevelSelection,
        WinScreen,
        GameOver
    }
    [HideInInspector] public MainMenuState currentMainMenuState;
    private string fadeAnimation;
    private Color tempColor;
    [SerializeField] float fadeAnimationSpeed;
    private Radio radio;

    void Start()
    {
        radio = GameObject.Find("Radio").GetComponent<Radio>();

        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            ShowThuthuluPanel();
        }
        else if(SceneManager.GetActiveScene().name == "LevelRoom")
        {
            GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();
            gameController.CreateLevel(gameController.CurrentLevel);
        }
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            switch (currentMainMenuState)
            {
                case MainMenuState.SplashScreenThuthulu:
                    HandleFade(thuthuluPanel, "fadeOut", ShowGerPanel);
                    break;

                case MainMenuState.SplashScreenGer:
                    HandleFade(GerPanel, "fadeOut", ShowBlackStarPanel);
                    break;
                
                case MainMenuState.SplashScreenBlackStar:
                    HandleFade(BlackStarPanel, "fadeOut", ShowMainMenuPanel);
                    break;
            }
        }
        else if (SceneManager.GetActiveScene().name == "LevelRoom")
        {
            //Faz algo?
        }
    }

    private void HandleFade(GameObject panel, string nextFadeState, System.Action onComplete)
    {
        var image = panel.GetComponentInChildren<UnityEngine.UI.Image>();
        if (image == null) return;

        tempColor = image.color;

        if (fadeAnimation == "fadeIn")
        {
            //Debug.Log($"Fade Animation: {fadeAnimation}, Alpha: {tempColor.a}");
            tempColor.a += 1.0f / fadeAnimationSpeed * Time.deltaTime;
            if (tempColor.a >= 1f)
            {
                tempColor.a = 1f;
                fadeAnimation = nextFadeState;
            }
        }
        else if (fadeAnimation == "fadeOut")
        {
            //Debug.Log($"Fade Animation: {fadeAnimation}, Alpha: {tempColor.a}");
            tempColor.a -= 1.0f / fadeAnimationSpeed * Time.deltaTime;
            if (tempColor.a <= 0f)
            {
                tempColor.a = 0f;
                onComplete.Invoke();
            }
        }

        image.color = tempColor;
    }

    public void ShowThuthuluPanel()
    {
        thuthuluPanel.SetActive(true);
        GerPanel.SetActive(false);
        BlackStarPanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        OpcoesPanel.SetActive(false);
        LevelSelectionPanel.SetActive(false);
        winScreenPanel.SetActive(false);
        freezedScreenPanel.SetActive(false);

        currentMainMenuState = MainMenuState.SplashScreenThuthulu;

        fadeAnimation = "fadeIn";

        var image = thuthuluPanel.GetComponentInChildren<UnityEngine.UI.Image>();
        tempColor = image.color;
        tempColor.a = 0f;
        image.color = tempColor;
    }

    public void ShowGerPanel()
    {
        thuthuluPanel.SetActive(false);
        GerPanel.SetActive(true);
        BlackStarPanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        OpcoesPanel.SetActive(false);
        LevelSelectionPanel.SetActive(false);
        winScreenPanel.SetActive(false);
        freezedScreenPanel.SetActive(false);

        currentMainMenuState = MainMenuState.SplashScreenGer;

        fadeAnimation = "fadeIn";

        var image = GerPanel.GetComponentInChildren<UnityEngine.UI.Image>();
        tempColor = image.color;
        tempColor.a = 0f;
        image.color = tempColor;
    }

    public void ShowBlackStarPanel()
    {
        thuthuluPanel.SetActive(false);
        GerPanel.SetActive(false);
        BlackStarPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
        OpcoesPanel.SetActive(false);
        LevelSelectionPanel.SetActive(false);
        winScreenPanel.SetActive(false);
        freezedScreenPanel.SetActive(false);

        currentMainMenuState = MainMenuState.SplashScreenBlackStar;

        fadeAnimation = "fadeIn";

        var image = BlackStarPanel.GetComponentInChildren<UnityEngine.UI.Image>();
        tempColor = image.color;
        tempColor.a = 0f;
        image.color = tempColor;
    }

    public void ShowMainMenuPanel()
    {
        radio.PlayMainMenuTheme();

        thuthuluPanel.SetActive(false);
        GerPanel.SetActive(false);
        BlackStarPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
        OpcoesPanel.SetActive(false);
        LevelSelectionPanel.SetActive(false);
        winScreenPanel.SetActive(false);
        freezedScreenPanel.SetActive(false);

        currentMainMenuState = MainMenuState.MainMenu;

        fadeAnimation = "fadeIn";
    }

    public void ShowOptionsPanel()
    {
        thuthuluPanel.SetActive(false);
        GerPanel.SetActive(false);
        BlackStarPanel.SetActive(false);
        OpcoesPanel.SetActive(true);
        winScreenPanel.SetActive(false);
        freezedScreenPanel.SetActive(false);
        currentMainMenuState = MainMenuState.Options;
    }

    public void PlayGame()
    {
        //Debug.Log("Opening the level dificulty screen");

        LevelSelectionPanel.SetActive(true);
        currentMainMenuState = MainMenuState.LevelSelection;
    }

    public void OpenOptions()
    {
        //Debug.Log("Opening Options Menu");
        ShowOptionsPanel();
    }

    public void ExitOptionsMainMenu()
    {
        currentMainMenuState = MainMenuState.MainMenu;
        OpcoesPanel.SetActive(false);
    }

    public void ExitGane()
    {
        //Debug.Log("Exiting game!");
        Application.Quit();
        // Testing on Unity's editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void ExitLevelSelection()
    {
        currentMainMenuState = MainMenuState.MainMenu;
        LevelSelectionPanel.SetActive(false);
    }

    public void OpenWinScreen()
    {
        winScreenPanel.SetActive(true);
        currentMainMenuState = MainMenuState.WinScreen;
    }

    public void OpenFreezedScreen()
    {
        freezedScreenPanel.SetActive(true);
        currentMainMenuState = MainMenuState.GameOver;
    }

    
}
