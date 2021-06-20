using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
    public TMP_InputField login;
    public TMP_InputField password;
    private string savedLogin;
    public bool loggedIn;
    public GameObject loginWindow;
    public GameObject mainMenuWindow;


    public void SaveLogin()
    {
        Debug.Log($"Setting to {savedLogin}");
        GameObject.Find("UserView").GetComponent<TextMeshProUGUI>().text = savedLogin;
        loggedIn = true;
        loginWindow.SetActive(false);
        mainMenuWindow.SetActive(true);
    }

    public void LoginUser()
    {

        if (!string.IsNullOrEmpty(login.text) && !string.IsNullOrEmpty(password.text))
        {
            ClientSend.SendLoginMessage(login.text, password.text);
            savedLogin = login.text;
        }
        else
        {
            Debug.Log("Podane pola s¹ obowi¹zkowe do wype³nienia!");
        }
    }
}
