using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuscript : MonoBehaviour
{
    public void PlayGame()
    {
       SceneManager.LoadScene(2);// this can load the scene number 1 (game)//SceneManager.GetActiveScene().buildIndex +1

    }
    public void QuitGame()
    {

        Application.Quit();
    }
}

