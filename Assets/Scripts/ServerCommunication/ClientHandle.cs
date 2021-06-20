using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// manages client side of connection
/// </summary>
public class ClientHandle : MonoBehaviour
{
    public GameObject connection;
    public GameObject noConnection;

    /// <summary>
    /// semds welcome info and tata
    /// </summary>
    /// <param name="_packet"></param>
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();
    }
    /// <summary>
    /// inform about establishing connection send coresponding info
    /// </summary>
    /// <param name="_packet"></param>
    public static void ConnectionEstablished(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Connection succeed!. Message from server: {_msg}");
        var oldId = Client.instance.myId;
        var socketId = PlayerPrefs.GetString("socketId");
        if (string.IsNullOrEmpty(socketId))
        {
            Client.instance.socketId = _msg;
            PlayerPrefs.SetString("socketId", _msg);
            PlayerPrefs.SetInt("clientId", oldId);
        }
        Client.instance.myId = _myId;
        Client.instance.socketId = _msg;
        Client.instance.connected = true;
        if(UIManager.instance?.Connection(true)==false)
        {
            InGameUIManager.instance.Connection(true);
        }
        if (!string.IsNullOrEmpty(socketId))
        {
            ClientSend.Reconnect();
        }
    }
    /// <summary>
    /// inform about registration status - sucees
    /// </summary>
    /// <param name="_packet"></param>
    public static void RegisterCallbackSuccess(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Registration msg: {_msg}");
    }
    /// <summary>
    /// inform about registration status -faliure
    /// </summary>
    /// <param name="_packet"></param>
    public static void RegisterCallbackFailure(Packet _packet)
    {
        string _msg = _packet.ReadString();
        Debug.Log($"Registration msg: {_msg}");
    }
   /// <summary>
   /// Manages callback in success login
   /// </summary>
   /// <param name="_packet"></param>
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
    /// <summary>
    /// Manages calllback success faliure
    /// </summary>
    /// <param name="_packet"></param>
    public static void LoginCallbackFailure(Packet _packet)
    {
        string _msg = _packet.ReadString();

        Debug.Log($"{_msg}");

    }

    public static void Reconnected(Packet _packet)
    {

        Debug.Log("Reconnect succeed!");
        string oldKey = _packet.ReadString();

        if(PlayerPrefs.GetString("socketId")==oldKey)
        {
            string _msg = _packet.ReadString();
            PlayerPrefs.SetString("socketId", _msg);
            PlayerPrefs.SetInt("clientId", Client.instance.myId);
            Client.instance.socketId = _msg;

        }
    }

    public static void NotReconnected(Packet _packet)
    {
        
        string oldKey = _packet.ReadString();
        Debug.Log($"Reconnect failed! {oldKey}, {Client.instance.socketId}");
        if (Client.instance.socketId == oldKey)
        {
            string _msg = _packet.ReadString();
            Client.instance.socketId = _msg;
            PlayerPrefs.SetString("socketId", _msg);
            PlayerPrefs.SetInt("clientId", Client.instance.myId);

        }
    }
}