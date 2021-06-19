using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
    public TMP_InputField login;
    public TMP_InputField password;

    public void LoginUser()
    {

        if (!string.IsNullOrEmpty(login.text) && !string.IsNullOrEmpty(password.text))
        {
                ClientSend.SendLoginMessage(login.text, password.text);
        }
        else
        {
            Debug.Log("Podane pola s¹ obowi¹zkowe do wype³nienia!");
        }




    }
}
