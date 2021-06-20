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

    public static void RegisterCallbackSuccess(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Registration msg: {_msg}");
    }
    public static void RegisterCallbackFailure(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Registration msg: {_msg}");
    }

    public static void LoginCallbackSuccess(Packet _packet)
    {
        string _msg = _packet.ReadString();
        var splittedMsg = _msg.Split(':');
        if (splittedMsg.Length == 2 && splittedMsg[0] == "Login succeed")
        {
            Debug.Log("Login succed");
            Client.instance.playerId = splittedMsg[1];
            GameObject.Find("UIManager").GetComponent<Login>().SaveLogin();

        }
        else
        {
            Debug.Log("Login sent wrong data attepmt!!!");
        }
    }

    public static void LoginCallbackFailure(Packet _packet)
    {
        string _msg = _packet.ReadString();

        Debug.Log($"{_msg}");

    }
}