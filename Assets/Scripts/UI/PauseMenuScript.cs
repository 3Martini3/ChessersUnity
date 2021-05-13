using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            pauseMenuUI.SetActive(isPaused);
            settingsMenuUI.SetActive(false);
        }
    }

    public void Resume()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
    }

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
