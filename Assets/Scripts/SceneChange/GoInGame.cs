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
        if(localgame)
        {
            CrossSceneInfo.localGame = true;
            SceneManager.LoadScene("Scenes/ChessScene/GameScene");
        }else
        {
            if (Client.instance.tcp.socket.Connected && GetComponent<Login>().loggedIn)
            {
                CrossSceneInfo.localGame = false;
                CrossSceneInfo.client = Client.instance;

            }
            else
            {
                Debug.Log("Cannot Log into game!");
            }
        }
       
    }
}
