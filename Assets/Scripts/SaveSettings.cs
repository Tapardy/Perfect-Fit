using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSettings : MonoBehaviour
{
    public Settings settings;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    public int highScore;

    private void Start()
    {
        masterSlider.value = settings.masterVolume;
        musicSlider.value = settings.musicVolume;
        sfxSlider.value = settings.sfxVolume;
        highScore = settings.highScore;
    }

    void Update()
    {
        settings.masterVolume = masterSlider.value;
        settings.musicVolume = musicSlider.value;
        settings.sfxVolume = sfxSlider.value;
        settings.highScore = highScore;

    }
}
