using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsScreen : MonoBehaviour
{
    public void Back_To_Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
