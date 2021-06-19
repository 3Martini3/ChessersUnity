using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();
    }

    public static void ConnectionEstablished(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Connection succeed!. Message from server: {_msg}");
        Client.instance.myId = _myId;
        Client.instance.connected = true;
        GameObject.Find("ConnectionView").GetComponent<TextMeshProUGUI>().text = "Connected";
    }

    public static void RegisterCallback(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Registration msg: {_msg}");
    }

    public static void LoginCallback(Packet _packet)
    {
        string _msg = _packet.ReadString();
        
        if (_msg=="Login failed")
        {
            Debug.Log($"{_msg}");
        }else
        {
            var splittedMsg = _msg.Split(':');
            if(splittedMsg.Length==2&&splittedMsg[0]=="Login succed")
            {
                Debug.Log("Login succed");
                Client.instance.playerId = splittedMsg[1];
                GameObject.Find("UIManager").GetComponent<Login>().SaveLogin();
                
            }else
            {
                Debug.Log("Login sent wrong data attepmt!!!");
            }
        }
        
    }
}