using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
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
            //Debug.Log($"{Client.instance.myId}, {UIManager.instance.usernameField.text}");
            var usernameInput = GameObject.Find("LoginInput").GetComponent<TMP_InputField>().text;
            var passwordInput = GameObject.Find("PasswordInput").GetComponent<TMP_InputField>().text;
            var register = new RegisterMessage
            {
                type = "Register",
                username = usernameInput,
                password = passwordInput
            };
            var json = JsonUtility.ToJson(register);
            Debug.Log(json);
            _packet.Write(json);
            Debug.Log("Welcome received");

            SendTCPData(_packet);
        }
    }

    public static void SendRegisterMessage(string usernameInput,string passwordInput)
    {
        using (Packet _packet = new Packet((int)ClientPackets.messageSent))
        {
            _packet.Write(Client.instance.myId);
            //Debug.Log($"{Client.instance.myId}, {UIManager.instance.usernameField.text}");
            var register = new RegisterMessage
            {
                type = "Register",
                username = usernameInput,
                password = passwordInput
            };
            var json = JsonUtility.ToJson(register);
            _packet.Write(json);
            Debug.Log("Registration sent");

            SendTCPData(_packet);
        }
    }

    public static void SendMessage()
    {
        using (Packet _packet = new Packet((int)ClientPackets.messageSent))
        {
            _packet.Write(Client.instance.myId);
            //Debug.Log($"{Client.instance.myId}, {UIManager.instance.usernameField.text}");
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