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

    public GameObject connection;
    public GameObject noConnection;
    public bool connectionCalled = false;


    /// <summary>
    /// Sets connection icon to true or false
    /// </summary>
    /// <param name="isConnected"></param>
    public bool Connection(bool isConnected)
    {
        if (noConnection != null && connection != null)
        {
            noConnection?.SetActive(!isConnected);
            connection?.SetActive(isConnected);
            return true;
        }
        return false;
    }
   
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {            //Debug.Log("Instance already exists, destroing object!");
            Destroy(this);
        }
    }

    public void Start()
    {
        InvokeRepeating("HoldConnection", 1f, 1f);
    }
    /// <summary>
    /// Manages connection holding
    /// </summary>
    public void HoldConnection()
    {
        if (connectionCalled == false)
        {
            Client.instance.ConnectToServer();
            connectionCalled = true;
        }
        else
        {

            Debug.Log(Client.instance?.tcp?.socket?.Connected);
            if (Client.instance?.tcp?.socket?.Connected == false)
            {
                Connection(false);
                Client.instance.ConnectToServer();
            }
            else
            {
                if (Client.instance.myId == 0)
                {
                    Client.instance.tcp.socket.Dispose();
                }
            }

        }

    }
}
