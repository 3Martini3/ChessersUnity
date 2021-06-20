using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoInGame : MonoBehaviour
{
    public bool localgame;

    public void GoLocalGame()
    {
        localgame = true;
    }


    public void GoToGameScene()
    {
        if (localgame)
        {
            CrossSceneInfo.onlineGame = false;
            SceneManager.LoadScene("Scenes/ChessScene/GameScene");
        }
        else
        {
            if (Client.instance.tcp.socket.Connected && GetComponent<Login>().loggedIn)
            {
                CrossSceneInfo.onlineGame = true;
                CrossSceneInfo.playerId = Client.instance.playerId;
                CrossSceneInfo.IP = Client.instance.ip;
                CrossSceneInfo.Port = Client.instance.port;
                CrossSceneInfo.myId = Client.instance.myId;
                SceneManager.LoadScene("Scenes/ChessScene/GameScene");
            }
            else
            {
                Debug.Log("Cannot Log into game!");
            }
        }

    }
}
