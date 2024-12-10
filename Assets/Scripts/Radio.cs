using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    private static Radio Instance { get; set; }
    public AudioClip mainMenuTheme;
    public AudioClip levelTheme;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PlayMainMenuTheme()
    {
        audioSource.Stop();
        audioSource.clip = mainMenuTheme;
        audioSource.Play();
    }

    public void PlayLevelTheme()
    {
        audioSource.Stop();
        audioSource.clip = levelTheme;
        audioSource.Play();
    }

    public void PlayMusicFunctionDelay(string musicFunction)
    {
        Invoke(musicFunction,2f);
    }
}
