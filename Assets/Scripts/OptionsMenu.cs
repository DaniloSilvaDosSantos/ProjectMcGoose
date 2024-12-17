using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private Slider volumeSlider;
    void Start()
    {
        volumeSlider = GameObject.Find("Slider").GetComponent<Slider>();

        volumeSlider.value = PlayerPrefs.GetFloat("volume", 0.8f);
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }
    
    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
        AudioListener.volume = volume;
    }    
}
