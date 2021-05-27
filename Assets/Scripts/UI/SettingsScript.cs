using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public AudioMixer MasterMixer;
    public Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public TMPro.TMP_Dropdown qualityDropdown;
    public Slider musicSlider;
    public Toggle fullscreenToogle;

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            //szukam rozdzielczoœci równej rozdzielczoœci monitora u¿ytkownika
            if (Screen.currentResolution.width == resolutions[i].width && Screen.currentResolution.height == resolutions[i].height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);

        //ustalam wrtoœæ na liœcie na równ¹ rozdzielczoœci monitora u¿ytkownika lub zapamiêtan¹ z innych scen / innych sesji
        if(PlayerPrefs.HasKey("resolution"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("resolution");
            resolutionDropdown.RefreshShownValue();
        }
        else
        {
            resolutionDropdown.value = currentResolutionIndex;
            PlayerPrefs.SetInt("resolution",currentResolutionIndex);
            resolutionDropdown.RefreshShownValue();
        }

        //ustawiam wartoœæ jakoœci tekstur na domyœln¹ lub zapamiêtan¹ z innych scen / innych sesji
        if(PlayerPrefs.HasKey("quality"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality"));
            qualityDropdown.value = PlayerPrefs.GetInt("quality");
            qualityDropdown.RefreshShownValue();
        }
        else
        {
            QualitySettings.SetQualityLevel(0);
            qualityDropdown.value = 0;
        }

        //ustawiam muzykê na po³owê lub wartoœæ zapiamiêtan¹ z innych scen / innych sesji
        if(PlayerPrefs.HasKey("musicVolume"))
        {
            MasterMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume"));
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            MasterMixer.SetFloat("musicVolume", -40);
            musicSlider.value = -40;
        }

        //ustawiam gre nia widok nie-pe³noekranowy lub zapamiêtany z poprzednich scen / innych sesji
        if(PlayerPrefs.HasKey("fullscreen"))
        {
            Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("fullscreen"));
            fullscreenToogle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("fullscreen"));
        }
        else
        {
            Screen.fullScreen = false;
            fullscreenToogle.isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float volume)
    {
        MasterMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetQuality(int level)
    {
        // 0 - high
        // 1 - medium
        // 2 - low
        QualitySettings.SetQualityLevel(2 - level);
        PlayerPrefs.SetInt("quality", level);
    }

    public void SetFullscreen(bool toggleState)
    {
        Screen.fullScreen = toggleState;
        int toggleStateInt = toggleState ? 1 : 0;
        PlayerPrefs.SetInt("fullscreen", toggleStateInt);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution chosenResolution = resolutions[resolutionIndex];
        PlayerPrefs.SetInt("resolution", resolutionIndex);
        Screen.SetResolution(chosenResolution.width, chosenResolution.height, Screen.fullScreen);
    }
}
