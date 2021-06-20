using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Takes care of visual representation of match outcome 
/// </summary>
public class CheckMateScreenHandler : MonoBehaviour
{

    public AudioSource endSound;
    public GameObject checkMateCanvas;
    public TextMeshProUGUI text;
    public TimerCanvas timer;


    /// <summary>
    /// shows end screen, depending on parameter- type of information, 1 white won, 2 black won, 3 a tie
    /// </summary>
    /// <param name="checkMateType"></param>
    public void ShowEndScreen(int checkMateType)
    {
        
        if(checkMateType == 1)
        {
            endSound.Play();
            text.text = "Bia³e wygra³y!";
            checkMateCanvas.GetComponent<Canvas>().enabled = true;
            timer.timerIsRunning = false;
        }
        if (checkMateType == 2)
        {
            endSound.Play();
            text.text = "Czarne wygra³y!";
            checkMateCanvas.GetComponent<Canvas>().enabled = true;
            timer.timerIsRunning = false;
        }
        if(checkMateType == 3)
        {
            endSound.Play();
            text.text = "Remis!";
            checkMateCanvas.GetComponent<Canvas>().enabled = true;
            timer.timerIsRunning = false;
        }
        text.text += "\n wciœnij [ESC] by powróciæ do menu g³ównego";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && checkMateCanvas.GetComponent<Canvas>().enabled)
        {
            SceneManager.LoadScene("Menu");
        }
    }


}
