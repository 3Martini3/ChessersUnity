using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckMateScreenHandler : MonoBehaviour
{
    public GameObject checkMateCanvas;


    /// <summary>
    /// shows end screen, depending on parameter- type of information, 1 white won, 2 black won, 3 a tie
    /// </summary>
    /// <param name="checkMateType"></param>
    public void ShowEndScreen(int checkMateType)
    {
        if(checkMateType == 1)
        {
            checkMateCanvas.GetComponent<Text>().text = "White Won!";
            checkMateCanvas.GetComponent<Canvas>().enabled = true;
        }
        if (checkMateType == 2)
        {
            checkMateCanvas.GetComponent<Text>().text = "Black Won!";
            checkMateCanvas.GetComponent<Canvas>().enabled = true;
        }
        if(checkMateType == 3)
        {
            checkMateCanvas.GetComponent<Text>().text = "Tie!";
            checkMateCanvas.GetComponent<Canvas>().enabled = true;
        }
    }


}
