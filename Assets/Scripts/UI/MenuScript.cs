using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Loads scenne with chess/ quit aplication
/// </summary>
public class MenuScript : MonoBehaviour
{
    /// <summary>
    /// Load chess
    /// </summary>
    public void PlayChess()
    {
        SceneManager.LoadScene("Scenes/ChessScene/GameScene");
    }
    /// <summary>
    /// quits aplication
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
