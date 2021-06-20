using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Control UI conection data
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI connection;
    public bool connectionCalled = false;

   
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            //Debug.Log("Instance already exists, destroing object!");
            Destroy(this);
        }
    }

    public void Start()
    {
        InvokeRepeating("HoldConnection", 1f, 5f);
    }
    /// <summary>
    /// Manages connection holding
    /// </summary>
    public void HoldConnection()
    {
        if(connectionCalled==false)
        {
            Client.instance.ConnectToServer();
            connectionCalled = true;
        }else
        {

        Debug.Log(Client.instance?.tcp?.socket?.Connected);
        if (Client.instance?.tcp?.socket?.Connected==false)
        {
                if(connection!=null)
                {
                    connection.text = "Disconnected";
                }
            Client.instance.ConnectToServer();
        }

        }

    }



    public void SendToServer()
    {
        ClientSend.SendMessage();
    }


}
