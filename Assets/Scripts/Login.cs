using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
    public TMP_InputField login;
    public TMP_InputField password;
    private string savedLogin;
    public GameObject loginWindow;
    public GameObject mainMenuWindow;


    public void SaveLogin()
    {
        GameObject.Find("UserView").GetComponent<TextMeshProUGUI>().text = savedLogin;
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
            Debug.Log("Podane pola s� obowi�zkowe do wype�nienia!");
        }
    }
}