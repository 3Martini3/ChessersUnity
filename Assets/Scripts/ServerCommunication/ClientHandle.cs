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
        int _myId = _packet.ReadInt();
        Debug.Log($"Registration msg: {_msg}");
    }
}