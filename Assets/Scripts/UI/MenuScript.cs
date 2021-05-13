using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public void PlayChess()
    {
        SceneManager.LoadScene("Scenes/MartaTesty");
    }

    public void PlayCheckers()
    {
        SceneManager.LoadScene("Scenes/Main");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
