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
    }

    void Start()
    {
        for(int i = 0; i < levels.Count; i++)
        {
            addLevelStars(levels[i].name, new bool[3]);
        }

        //teste
        foreach(string level in levelsStars.Keys)
        {
            bool[] stars = getLevelStars(level);

            Debug.Log($"Level: '{level}', stars: '{string.Join(", ", stars)}'");
        }
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
}
