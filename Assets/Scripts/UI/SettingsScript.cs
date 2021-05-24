using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public TMPro.TMP_Dropdown qualityDropdown;
    public Slider musicSlider;
    public Toggle fullscreenToogle;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            //szukam rozdzielczo�ci r�wnej rozdzielczo�ci monitora u�ytkownika
            if (Screen.currentResolution.width == resolutions[i].width && Screen.currentResolution.height == resolutions[i].height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);

        //ustalam wrto�� na li�cie na r�wn� rozdzielczo�ci monitora u�ytkownika lub zapami�tan� z innych scen / innych sesji
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

        //ustawiam warto�� jako�ci tekstur na domy�ln� lub zapami�tan� z innych scen / innych sesji
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

        //ustawiam muzyk� na po�ow� lub warto�� zapiami�tan� z innych scen / innych sesji
        if(PlayerPrefs.HasKey("musicVolume"))
        {
            audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume"));
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            audioMixer.SetFloat("musicVolume", -40);
            musicSlider.value = -40;
        }

        //ustawiam gre nia widok nie-pe�noekranowy lub zapami�tany z poprzednich scen / innych sesji
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
        audioMixer.SetFloat("musicVolume", volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
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