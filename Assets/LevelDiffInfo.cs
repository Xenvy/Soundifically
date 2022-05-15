using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDiffInfo : MonoBehaviour
{
    public void Exercise_Start()
    {
        SceneManager.LoadScene("LevelDiff Exercise");
    }

    public void Back_To_Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
