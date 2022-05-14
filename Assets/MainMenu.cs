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

    public void Quit_App()
    {
        Application.Quit();
    }
}
