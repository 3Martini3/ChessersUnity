using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// handles pressing 'resign' button. At the moment only loads MainMenu, exiting the game
/// </summary>
public class ResignButtonHandler : MonoBehaviour
{
    /// <summary>
    /// ends game, and loads menu scene
    /// </summary>
    public void EndGame()
    {
        SceneManager.LoadScene("Menu");
    }

}
