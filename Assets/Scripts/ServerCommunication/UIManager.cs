using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;
    public InputField usernameField;
    public Text connectionText;
    public bool connectionCalled = false;

    private void Update()
    {
        if (!connectionCalled)
        {
            connectionCalled = true;
            connectionText.text = "Disconnected";
            ConnectToServer();
            Debug.Log("Connection called");
            
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Debug.Log("Instance already exists, destroing object!");
            Destroy(this);
        }
    }

    public void ConnectToServer()
    {
        //startMenu.SetActive(false);
      //  usernameField.interactable = false;
        Client.instance.ConnectToServer();
    }

    public void SendToServer()
    {
        ClientSend.SendMessage();
    }


}
