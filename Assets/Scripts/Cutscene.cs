using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image[] photos;
    [SerializeField] private UnityEngine.UI.Image currentPhoto;
    private int currentPhotoID;
    [SerializeField] private float fadeOut = 1f;
    [SerializeField] private String sceneToLoad;
    [SerializeField] private Radio radio;
    [SerializeField] private AudioSource Audio;
    [SerializeField] private AudioClip turningPages;
    [SerializeField] private List<AudioClip> soundEfects;
    [SerializeField] private AudioClip music;
    [SerializeField] private int musicDuration = 0;
    private SceneTransitionManager sceneTransitionManager;

    void Start()
    {
        radio = GameObject.Find("Radio").GetComponent<Radio>();
        Audio = GameObject.Find("CanvasCutscenes").GetComponent<AudioSource>();
        
        Audio.PlayOneShot(turningPages);

        if (music != null) radio.PlayCutsceneMusic(music);

        sceneTransitionManager = GameObject.Find("SceneTransitionManager").GetComponent<SceneTransitionManager>();

        for (int i = 0; i< photos.Length; i++)
        {
            Color tempColor = photos[i].color;
            tempColor.a = 0f;
            photos[i].color = tempColor;
            //Debug.Log(photos[i].color.a);
        }
        currentPhoto = photos[0];
        currentPhotoID = 0;

        if (soundEfects[currentPhotoID] != null) Audio.PlayOneShot(soundEfects[currentPhotoID]);
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if(currentPhoto.color.a < 1f)
        {
            Color tempColor = currentPhoto.color;
            tempColor.a += fadeOut/60;
            currentPhoto.color = tempColor;

            //Debug.Log(tempColor.a);
        }
        else
        {
            if(currentPhotoID+1 < photos.Length)
            {
                Audio.PlayOneShot(turningPages);

                if (soundEfects[currentPhotoID+1] != null) Audio.PlayOneShot(soundEfects[currentPhotoID+1]);

                currentPhotoID++;
                currentPhoto = photos[currentPhotoID];
                musicDuration--;
                if(musicDuration <= 0) radio.StopMusic();
            }
            else
            {
                if(sceneToLoad != "GameOver") radio.PlayMusicFunctionDelay("PlayLevelTheme");
                sceneTransitionManager.TransitionToScene(sceneToLoad);
                this.enabled = false;
            }
        }
    }
}