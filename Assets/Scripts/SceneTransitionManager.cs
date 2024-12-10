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
        transitionAnim.SetTrigger("Start");
        SceneManager.sceneLoaded += OnSceneLoaded;
        transitionCanvas.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(2f);
        transitionCanvas.SetActive(false);
    }
}
