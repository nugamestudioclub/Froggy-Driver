using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
 

    public void ToGame()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void ToGameOver()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

    public void ToWin()
    {
        SceneManager.LoadScene("GameWon", LoadSceneMode.Single);
    }


    public void ToCredits()
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }

    public void ToTitleScreen()
    {
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
    }

    public void ToHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
