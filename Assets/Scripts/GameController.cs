//using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//using UnityEditor.Build.Content;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instanceGameController;
    [SerializeField] private List<GameObject> levels = new List<GameObject>();
    private Dictionary<string, bool[]> levelsStars = new Dictionary<string, bool[]>();
    private StartsHud starsHud;
    private Timer timer;
    public string CurrentLevel { get; set; }
    public int CurrentCutscene { get; set; }
    private string savePath;

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

        LoadData();

        for(int i = 0; i < levels.Count; i++)
        {
            addLevelStars(levels[i].name, new bool[3]);
        }

        savePath = Path.Combine(Application.persistentDataPath, "levelsStars.json");
    }

    void Start()
    {
        //
    }

    void Update()
    {
        //
    }

    void OnApplicationQuit()
    {
        SaveData();
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
        starsHud = GameObject.Find("Stars").GetComponent<StartsHud>();
        starsHud.starsAll[0] = true;
    }

    public IEnumerator WinTheLevel()
    {
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        starsHud = GameObject.Find("Stars").GetComponent<StartsHud>();

        yield return new WaitForSeconds(1.5f);

        if(CountingEnemies() > 0)
        {
            Debug.Log("Derrota");

            if(timer.executarFuncao) timer.StopTimer();

            yield return new WaitForSeconds(0.5f);

            MenuController menuController = GameObject.Find("CanvasMenu").GetComponent<MenuController>();
            menuController.OpenFreezedScreen();

            //EnableDefeatScreen();
        }
        else
        {
            Debug.Log("Vitoria");

            if(timer.executarFuncao)
            {
                timer.StopTimer();
                starsHud.starsAll[1] = true;
            }

            yield return new WaitForSeconds(0.5f);

            PlayerGetFirstStarInTheLevel();

            updateLevelStars(CurrentLevel, starsHud.starsAll);

            MenuController menuController = GameObject.Find("CanvasMenu").GetComponent<MenuController>();
            menuController.OpenWinScreen();

            //EnableWinScreen();
        }

        //teste
        /*foreach(string level in levelsStars.Keys)
        {
            bool[] stars = getLevelStars(level);

            Debug.Log($"Level: '{level}', stars: '{string.Join(", ", stars)}'");
        }*/
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

    public void SaveData()
    {
        LevelStarsWrapper wrapper = new LevelStarsWrapper();

        foreach (var entry in levelsStars)
        {
            wrapper.levelsStarsList.Add(new LevelStars(entry.Key, entry.Value));
        }

        string json = JsonUtility.ToJson(wrapper, true);

        File.WriteAllText(savePath, json);

        Debug.Log("Dados salvos em {savePath}");
    }

    public void LoadData()
    {
        if(File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);

            LevelStarsWrapper wrapper = JsonUtility.FromJson<LevelStarsWrapper>(json);

            levelsStars.Clear();

            foreach (var levelStars in wrapper.levelsStarsList)
            {
                levelsStars.Add(levelStars.levelName, levelStars.stars);
            }

            Debug.Log("Dados carregados com sucesso");
        }
        else
        {
            Debug.Log("Arquivo de dados não encontrado");
        }
    }

    public void DeleteData()
    {
        if(File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);

            LevelStarsWrapper wrapper = JsonUtility.FromJson<LevelStarsWrapper>(json);

            levelsStars.Clear();

            foreach (var levelStars in wrapper.levelsStarsList)
            {
                levelsStars.Add(levelStars.levelName, new bool[3]);
            }

            Debug.Log("Dados apagados com sucesso");
        }
        else
        {
            Debug.LogWarning("Arquivo de dados não encontrado");
        }
    }

    public void CreateLevel(string levelName)
    {
        switch(levelName)
        {
            case "Level01":
                Instantiate(levels[0], Vector3.zero, Quaternion.identity);
                break;
            case "Level02":
                Instantiate(levels[1], Vector3.zero, Quaternion.identity);
                break;
            case "Level03":
                Instantiate(levels[2], Vector3.zero, Quaternion.identity);
                break;
            case "Level04":
                Instantiate(levels[3], Vector3.zero, Quaternion.identity);
                break;
            case "Level05":
                Instantiate(levels[4], Vector3.zero, Quaternion.identity);
                break;
            case "Level06":
                Instantiate(levels[5], Vector3.zero, Quaternion.identity);
                break;
            case "Level07":
                Instantiate(levels[6], Vector3.zero, Quaternion.identity);
                break;
            case "Level08":
                Instantiate(levels[7], Vector3.zero, Quaternion.identity);
                break;
            case "Level09":
                Instantiate(levels[8], Vector3.zero, Quaternion.identity);
                break;
        }
    }
}



[System.Serializable]
public class LevelStarsWrapper
{
    public List<LevelStars> levelsStarsList = new List<LevelStars>();
}

[System.Serializable]
public class LevelStars
{
    public string levelName;
    public bool[] stars;

    public LevelStars(string levelName, bool[] stars)
    {
        this.levelName = levelName;
        this.stars = stars;
    }
}
