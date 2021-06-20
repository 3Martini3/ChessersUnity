using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
/// <summary>
/// Settings menu, contains methods to control resolution, audio and full screen option
/// </summary>
public class SettingsScript : MonoBehaviour
{
    public AudioMixer MasterMixer;
    public Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public TMPro.TMP_Dropdown qualityDropdown;
    public Slider musicSlider;
    public Slider SFXSlider;
    public Toggle fullscreenToogle;

    /// <summary>
    /// Sets starting resolution to user screen resoluton/set saved settings 
    /// </summary>
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
        //to samo co wy¿ej z efektami specjalnymi
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            MasterMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        else
        {
            MasterMixer.SetFloat("SFXVolume", -40);
            SFXSlider.value = -40;
        }

        //ustawiam gre nia widok nie-pe³noekranowy lub zapamiêtany z poprzednich scen / innych sesji
        if (PlayerPrefs.HasKey("fullscreen"))
        {
            Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("fullscreen"));
            fullscreenToogle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("fullscreen"));
        }
        else
        {
            Screen.fullScreen = false;
            fullscreenToogle.isOn = false;
        }
        //musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }

   /// <summary>
   /// Sets volume of music
   /// </summary>
   /// <param name="volume"></param>
        public void SetVolume(float volume)
    { 
        MasterMixer.SetFloat("musicVolume", volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    /// <summary>
    /// Sets volume of SFX
    /// </summary>
    /// <param name="volume"></param>
    public void SetSFXVolume(float volume)
    {
        MasterMixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    /// <summary>
    /// Sets quality 
    /// </summary>
    /// <param name="level"></param>
    public void SetQuality(int level)
    {
        // 0 - high
        // 1 - medium
        // 2 - low
        QualitySettings.SetQualityLevel(2 - level);
        PlayerPrefs.SetInt("quality", level);
    }
    /// <summary>
    /// Eneables/disables full screen
    /// </summary>
    /// <param name="toggleState"></param>
    public void SetFullscreen(bool toggleState)
    {
        Screen.fullScreen = toggleState;
        int toggleStateInt = toggleState ? 1 : 0;
        PlayerPrefs.SetInt("fullscreen", toggleStateInt);
    }
    /// <summary>
    /// sets resolution
    /// </summary>
    /// <param name="resolutionIndex"></param>
    public void SetResolution(int resolutionIndex)
    {
        Resolution chosenResolution = resolutions[resolutionIndex];
        PlayerPrefs.SetInt("resolution", resolutionIndex);
        Screen.SetResolution(chosenResolution.width, chosenResolution.height, Screen.fullScreen);
    }
}
