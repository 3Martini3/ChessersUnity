using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inputs : MonoBehaviour
{
    public void OnTextChange(TextMeshProUGUI text)
    {
        //Debug.Log(text.text);
        if(text.text.Length>10)
        {
            text.text=text.text.Substring(0,text.text.Length-2);
            //Debug.Log(text.text.Substring(0, text.text.Length - 2));
        }
        text.text = "AAAAAAAAAAA";
        //Debug.Log(text.text);
    }
}
