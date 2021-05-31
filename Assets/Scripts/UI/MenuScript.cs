using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public void PlayChess()
    {
        SceneManager.LoadScene("Scenes/ChessScene/GameScene");
    }

    public void PlayCheckers()
    {
        SceneManager.LoadScene("Scenes/MartaTesty");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
