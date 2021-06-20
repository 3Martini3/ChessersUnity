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

    /// <summary>
    /// Swaps turn to the next player (color) and sendt text info
    /// </summary>
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
