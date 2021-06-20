using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// Swap turn and send text infromation about it to game
/// </summary>
public class Turns : MonoBehaviour
{
    public ChessEnum.Color turn;
    public GameObject whiteTurnImg;
    public GameObject blackTurnImg;

    /// <summary>
    /// Swaps turn to the next player (color) and sendt text info
    /// </summary>
    public void SwapTurn()
    {
        if (turn == ChessEnum.Color.White)
        {
            turn = ChessEnum.Color.Black;
            whiteTurnImg.SetActive(false);
            blackTurnImg.SetActive(true);
        }
        else
        {
            turn = ChessEnum.Color.White;
            whiteTurnImg.SetActive(true);
            blackTurnImg.SetActive(false);
        }
        GameObject.FindGameObjectWithTag("Timers").GetComponent<TimerCanvas>().turn = turn; //timer turn swap
        
        
    }
}
