using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public static float volume = 1.0f;

    public GameObject masterSlider, musicSlider, sfxSlider;

    public AudioMixer masterMixer;

    float masterSoundLevel, musicSoundLevel, sfxSoundLevel;

    private void Start()
    {
        masterMixer.GetFloat("volume", out masterSoundLevel);
        masterMixer.GetFloat("musicVol", out musicSoundLevel);
        masterMixer.GetFloat("sfxVol", out sfxSoundLevel);

        masterSlider.GetComponent<Slider>().value = masterSoundLevel;
        musicSlider.GetComponent<Slider>().value = musicSoundLevel;
        sfxSlider.GetComponent<Slider>().value = sfxSoundLevel;
    }
    public void SetVolume(float sliderValue)
    {
        volume = sliderValue;
    }
}
