using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    void Start()
    {
        if (!PlayerPrefs.HasKey("VolumeLevel"))
        {
            PlayerPrefs.SetFloat("VolumeLevel", 1);
        }
        Load();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("VolumeLevel");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("VolumeLevel", volumeSlider.value);
    }
}
