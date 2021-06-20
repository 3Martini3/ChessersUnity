using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages in game UI connections 
/// </summary>
public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager instance;
    public bool connectionCalled = false;

    public GameObject connectionimg;
    public GameObject noConnection;
    /// <summary>
    /// Sets connection icon to true or false
    /// </summary>
    /// <param name="isConnected"></param>
    public bool Connection(bool isConnected)
    {
        if (noConnection != null && connectionimg != null)
        {
            noConnection?.SetActive(!isConnected);
            connectionimg?.SetActive(isConnected);
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
        { 
            Destroy(this);
        }
    }

    /// <summary>
    /// Sett starting crossScene dta
    /// </summary>
    public void Start()
    {
        if (CrossSceneInfo.onlineGame)
        {
            Client.instance.playerId = CrossSceneInfo.playerId;
            Client.instance.ip = CrossSceneInfo.IP;
            Client.instance.port = CrossSceneInfo.Port;
            Client.instance.myId = CrossSceneInfo.myId;
            Debug.Log($"Online game start for player {Client.instance.playerId}");
            InvokeRepeating("HoldConnection", 1f, 5f);
            Debug.Log(PlayerPrefs.GetString("socketId"));
            
        }else
        {
            noConnection.SetActive(false);
        }
    }


    /// <summary>
    /// Holds connection, control connection state 
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
}
