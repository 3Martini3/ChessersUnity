using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// Manages player registration
/// </summary>
public class Register : MonoBehaviour
{
    public TMP_InputField login;
    public TMP_InputField password;
    public TMP_InputField passwordRepeat;

    /// <summary>
    /// check if registration data is valid
    /// </summary>
    public void RegisterUser()
    {
        //startMenu.SetActive(false);
        //  usernameField.interactable = false;
        if (!string.IsNullOrEmpty(login.text) && !string.IsNullOrEmpty(password.text) && !string.IsNullOrEmpty(passwordRepeat.text))
        {
            if (password.text == passwordRepeat.text)
            {
                ClientSend.SendRegisterMessage(login.text, password.text);
            }
            else
            {
                Debug.Log("Has³a musz¹ byæ takie same!");
            }
        }
        else
        {
            Debug.Log("Podane pola s¹ obowi¹zkowe do wype³nienia!");
        }




    }
}
