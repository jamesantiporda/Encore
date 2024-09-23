using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    private AudioListener audioListener;
    public GameObject slider;

    // Start is called before the first frame update
    void Start()
    {
        audioListener = GetComponent<AudioListener>();
        AudioListener.volume = SettingsMenu.volume;
    }

    public void SetVolume(float sliderValue)
    {
        AudioListener.volume = sliderValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
