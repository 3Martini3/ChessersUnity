using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Turns : MonoBehaviour
{
    public ChessEnum.Color turn;

    public void SwapTurn()
    {
        if (turn == ChessEnum.Color.White)
        {
            turn = ChessEnum.Color.Black;
        }
        else
        {
            turn = ChessEnum.Color.White;
        }
        GetComponent<TextMeshProUGUI>().text = turn.ToString();
        GameObject.FindGameObjectWithTag("Timers").GetComponent<TimerCanvas>().turn = turn; //timer turn swap
        
    }
}
