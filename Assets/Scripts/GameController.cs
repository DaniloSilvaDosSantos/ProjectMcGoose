using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instanceGameController;
    [SerializeField] private List<GameObject> levels = new List<GameObject>();
    private Dictionary<string, bool[]> levelsStars = new Dictionary<string, bool[]>();
    private StartsHud startsHud;
    private Timer timer;
    private string currentLevel = "Level02";

    void Awake()
    {
        if (instanceGameController == null)
        {
            instanceGameController = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        startsHud = GameObject.Find("Stars").GetComponent<StartsHud>();
        timer = GameObject.Find("Timer").GetComponent<Timer>();
    }

    void Start()
    {
        for(int i = 0; i < levels.Count; i++)
        {
            addLevelStars(levels[i].name, new bool[3]);
        }

        //teste
        /*foreach(string level in levelsStars.Keys)
        {
            bool[] stars = getLevelStars(level);

            Debug.Log($"Level: '{level}', stars: '{string.Join(", ", stars)}'");
        }*/
    }

    public void addLevelStars(string levelName, bool[] starsValor)
    {
        if(!levelsStars.ContainsKey(levelName))
        {
            levelsStars.Add(levelName, starsValor);
            Debug.Log($"Level '{levelName}' adicionado com valor '{string.Join(", ", starsValor)}'.");
        }
        else
        {
            Debug.Log($"Level '{levelName}' já existe no dicionario");
        }
    }

    public void updateLevelStars(string levelName, bool[] newStarsValor)
    {
        if(levelsStars.ContainsKey(levelName))
        {
            levelsStars[levelName] = newStarsValor;
            Debug.Log($"Level '{levelName}' tem estrelas atualizadas para '{string.Join(", ", newStarsValor)}'");
        }
        else
        {
            Debug.Log($"Level '{levelName}' não existe");
        }
    }

    public bool[] getLevelStars(string levelName)
    {
        if(levelsStars.ContainsKey(levelName))
        {
            return levelsStars[levelName];
        }
        else
        {
            Debug.Log($"Level{levelName} não existe");
            return new bool[3];
        }
    }

    public void PlayerGetFirstStarInTheLevel()
    {
        startsHud.starsAll[0] = true;
    }

    public IEnumerator WinTheLevel()
    {
        if(CountingEnemies() > 0)
        {
            Debug.Log("Derrota");

            if(timer.executarFuncao) timer.StopTimer();

            yield return new WaitForSeconds(0.5f);

            //EnableDefeatScreen();
        }
        else
        {
            Debug.Log("Vitoria");

            if(timer.executarFuncao)
            {
                timer.StopTimer();
                startsHud.starsAll[1] = true;
            }

            yield return new WaitForSeconds(0.5f);

            PlayerGetFirstStarInTheLevel();

            updateLevelStars(currentLevel, startsHud.starsAll);
            //EnableWinScreen();

        }

        //teste
        foreach(string level in levelsStars.Keys)
        {
            bool[] stars = getLevelStars(level);

            Debug.Log($"Level: '{level}', stars: '{string.Join(", ", stars)}'");
        }
    }

    public int CountingEnemies()
    {
        GameObject[] AllObjects = GameObject.FindObjectsOfType<GameObject>();
        int allEnemies = 0;

        for(int i = 0; i < AllObjects.Length; i++)
        {
            if(AllObjects[i].layer == LayerMask.NameToLayer("Enemy")) allEnemies++;
        }

        return allEnemies;
    }
}
