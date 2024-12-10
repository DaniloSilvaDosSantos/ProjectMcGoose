using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    private static SceneTransitionManager instance;
    [SerializeField] Animator transitionAnim;
    [SerializeField] GameObject transitionCanvas;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private IEnumerator LoadScene(string sceneName)
    {
        transitionAnim.SetTrigger("End");
        SceneManager.sceneLoaded += OnSceneLoaded;
        transitionCanvas.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        transitionCanvas.SetActive(false);
    }
}
