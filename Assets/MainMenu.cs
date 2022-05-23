using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Scene_Selection()
    {
        SceneManager.LoadScene("Exercise Selection");
    }

    public void Settings_Screen()
    {
        SceneManager.LoadScene("Settings Screen");
    }

    public void Highscores_Screen()
    {
        SceneManager.LoadScene("Highscores");
    }

    public void Quit_App()
    {
        Application.Quit();
    }
}
