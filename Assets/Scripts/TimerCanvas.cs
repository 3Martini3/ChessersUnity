using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimerCanvas : MonoBehaviour
{
    public float timeRemainingWhite;
    public float timeRemainingBlack;
    private bool timerIsRunning;
    private string timeText;
    public ChessEnum.Color turn;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        turn = ChessEnum.Color.White;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if(turn == ChessEnum.Color.White)
            {
                if (timeRemainingWhite > 0)
                {
                    timeRemainingWhite -= Time.deltaTime;
                    DisplayTime(timeRemainingWhite);
                }
                else
                {
                    //Debug.Log("Time has run out!");
                    timeRemainingWhite = 0;
                    timerIsRunning = false;
                }
            }
            else if(turn == ChessEnum.Color.Black)
            {
                if (timeRemainingBlack > 0)
                {
                    timeRemainingBlack -= Time.deltaTime;
                    DisplayTime(timeRemainingBlack);
                }
                else
                {
                    //Debug.Log("Time has run out!");
                    timeRemainingBlack = 0;
                    timerIsRunning = false;
                }
            }
            
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText = string.Format("{0:00}:{1:00}", minutes, seconds);

        GameObject.FindWithTag("Timers").GetComponent<TMPro.TextMeshProUGUI>().text = timeText;
    }
}