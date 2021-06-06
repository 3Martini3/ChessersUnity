using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            Debug.Log($"{Client.instance.myId}, {UIManager.instance.usernameField.text}");
            _packet.Write(UIManager.instance.usernameField.text);


            SendTCPData(_packet);
        }
    }

    public static void SendMessage()
    {
        using (Packet _packet = new Packet((int)ClientPackets.messageSent))
        {
            _packet.Write(Client.instance.myId);
            Debug.Log($"{Client.instance.myId}, {UIManager.instance.usernameField.text}");
            _packet.Write("{\"type\":\"login\",\"username\":\"myuser\",\"password\":\"password\"}");
            BuildMessage();

            SendTCPData(_packet);
        }
    }
    #endregion

    public static string BuildMessage()
    {
        StringBuilder sb = new StringBuilder();
        string type = "\"login\"";
        string username = GameObject.FindGameObjectWithTag("LoginInput").GetComponent<TextMesh>().text;
        string password = GameObject.FindGameObjectWithTag("PasswordInput").GetComponent<TextMesh>().text;
        sb.AppendFormat("{\"type\":{0},\"username\":{1},\"password\":{2}}", type, username, password);

        Debug.Log(type + username + password);

        return sb.ToString();

    }
}