using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Pause menu controll
/// </summary>
public class PauseMenuScript : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;

    // Start is called before the first frame update
    /// <summary>
    /// Initialy hides menu 
    /// </summary>
    void Start()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
    }

    // Update is called once per frame
    /// <summary>
    /// Uptades once per frame checking for pause button click
    /// </summary>
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            pauseMenuUI.SetActive(isPaused);
            settingsMenuUI.SetActive(false);
        }
    }
    /// <summary>
    /// Resumes game 
    /// </summary>
    public void Resume()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
    }

    /// <summary>
    /// Sets state of menu
    /// </summary>
    public void SetActiveSettingsMenu()
    {
        if(settingsMenuUI.activeSelf)
        {
            settingsMenuUI.SetActive(false);
            pauseMenuUI.SetActive(true);
        }
        else
        {
            settingsMenuUI.SetActive(true);
            pauseMenuUI.SetActive(false);
        }
    }
}
