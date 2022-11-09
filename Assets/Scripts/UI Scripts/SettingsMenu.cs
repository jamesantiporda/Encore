using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public static float volume = 1.0f;

    public void SetVolume(float sliderValue)
    {
        volume = sliderValue;
    }
}
