using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    UIManager instance;
    public TextMeshProUGUI connection;
    public bool connectionCalled = false;

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
            connection.text = "Disconnected";
            Debug.Log(PlayerPrefs.GetString("socketId"));
        }
    }



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
                if (connection != null)
                {
                    connection.text = "Disconnected";
                }
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
