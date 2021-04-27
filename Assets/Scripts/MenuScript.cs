using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;

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
            if(Screen.currentResolution.width == resolutions[i].width && Screen.currentResolution.height == resolutions[i].height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);

        //ustalam wrtoœæ na liœcie na równ¹ rozdzielczoœci monitora u¿ytkownika
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        //ustawiam gre nia widok nie-pe³noekranowy
        Screen.fullScreen = false;
    }

    public void PlayChess()
    {
        SceneManager.LoadScene("Scenes/MartaTesty");
    }

    public void PlayCheckers()
    {
        SceneManager.LoadScene("Scenes/Main");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }

    public void SetQuality(int level)
    {
        // 0 - high
        // 1 - medium
        // 2 - low
        QualitySettings.SetQualityLevel(2 - level);
    }

    public void SetFullscreen(bool toogleState)
    {
        Screen.fullScreen = toogleState;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution chosenResolution = resolutions[resolutionIndex];
        Screen.SetResolution(chosenResolution.width, chosenResolution.height,Screen.fullScreen);
    }
}
