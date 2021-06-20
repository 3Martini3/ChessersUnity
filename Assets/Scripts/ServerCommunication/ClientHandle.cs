using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// manages client side of connection
/// </summary>
public class ClientHandle : MonoBehaviour
{
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
        Client.instance.myId = _myId;
        Client.instance.connected = true;
        GameObject.Find("ConnectionView").GetComponent<TextMeshProUGUI>().text = "Connected";
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
}