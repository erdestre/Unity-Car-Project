using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetGeneralVolume(float Volume)
    {
        audioMixer.SetFloat("Volume", Volume);
    }
    public void SetSFXVolume(float Volume)
    {

    }
    public void SetMusicVolume(float Volume)
    {

    }
    public void SetAntiAlliasing(bool Value)
    {

    }
    public void SetVSync(bool Value)
    {

    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
