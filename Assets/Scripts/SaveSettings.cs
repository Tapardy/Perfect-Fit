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

    private void Start()
    {
        masterSlider.value = settings.masterVolume;
        musicSlider.value = settings.musicVolume;
        sfxSlider.value = settings.sfxVolume;
        
    }

    void Update()
    {
        settings.masterVolume = masterSlider.value;
        settings.musicVolume = musicSlider.value;
        settings.sfxVolume = sfxSlider.value;

    }
}
