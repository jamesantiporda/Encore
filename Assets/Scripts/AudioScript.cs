using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioScript : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void SetMusicSound(float soundLevel)
    {
        masterMixer.SetFloat("musicVol", soundLevel);
    }

    public void SetSFXSound(float soundLevel)
    {
        masterMixer.SetFloat("sfxVol", soundLevel);
    }

    public void SetMasterSound(float soundLevel)
    {
        masterMixer.SetFloat("volume", soundLevel);
    }
}
