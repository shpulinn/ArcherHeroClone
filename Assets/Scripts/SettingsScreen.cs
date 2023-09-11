using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle muteToggle;
    
    private void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("GlobalVolume", 1f);
        volumeSlider.value = AudioListener.volume;
        AudioListener.pause = PlayerPrefs.GetInt("MuteSounds", 1) == 0;
        muteToggle.isOn = AudioListener.pause;

        PlayerPrefs.Save();
        
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("GlobalVolume", 1f);
        volumeSlider.value =  PlayerPrefs.GetFloat("GlobalVolume", 1f);//AudioListener.volume;
        AudioListener.pause = PlayerPrefs.GetInt("MuteSounds", 1) == 0;
        muteToggle.isOn = PlayerPrefs.GetInt("MuteSounds", 1) == 0;//AudioListener.pause;
    }

    public void MuteAllSounds(bool value)
    {
        AudioListener.pause = value;
        PlayerPrefs.SetInt("MuteSounds", value == true ? 0 : 1);
        PlayerPrefs.Save();
    }

    public void SetGlobalVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("GlobalVolume", value);
        PlayerPrefs.Save();
    }
}
