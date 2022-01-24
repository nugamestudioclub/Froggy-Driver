using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
 

    public void ToGame()
    {
        SceneManager.LoadScene("UILayout", LoadSceneMode.Single);
    }

    public void ToGameOver()
    {
        SceneManager.LoadScene("Lose", LoadSceneMode.Single);
    }

    public void ToWin()
    {
        SceneManager.LoadScene("Win", LoadSceneMode.Single);
    }


    public void ToCredits()
    {
        SceneManager.LoadScene("Credit", LoadSceneMode.Single);
    }

    public void ToTitleScreen()
    {
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
