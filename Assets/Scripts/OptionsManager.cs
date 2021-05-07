using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public static OptionsManager Instance;

    public AudioMixer mixer;
    public AudioSetting[] audioSettings;

    private enum AudioGroups { Master, Music, SFX };

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            for (int i = 0; i < audioSettings.Length; i++)
            {
                audioSettings[i].Initialize();
            }
            this.enabled = false;
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        //if (Instance != this) return;
        for (int i = 0; i < audioSettings.Length; i++)
        {
            audioSettings[i].Initialize();
        }
    }

    public void SetMasterVolume(float value)
    {
        audioSettings[(int)AudioGroups.Master].SetExposedParam(value);
    }

    public void SetMusicVolume(float value)
    {
        audioSettings[(int)AudioGroups.Music].SetExposedParam(value);

    }

    public void SetSFXVolume(float value)
    {
        audioSettings[(int)AudioGroups.SFX].SetExposedParam(value);

    }

    public void SetWindowedMode(bool value)
    {
        Screen.fullScreen = value;
    }

}

[System.Serializable]
public class AudioSetting
{
    [SerializeField]
    private string groupName;
    public Slider slider;
    //public GameObject redX;
    public string exposedParam;

    public void Initialize()
    {
        slider.value = PlayerPrefs.GetFloat(exposedParam, 0);
    }

    public void SetExposedParam(float value) // 1
    {
        //redX.SetActive(value <= slider.minValue); // 2
        OptionsManager.Instance.mixer.SetFloat(exposedParam, value); // 3
        PlayerPrefs.SetFloat(exposedParam, value); // 4
    }
}
