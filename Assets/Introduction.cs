using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour
{
    public void Continue_To_Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
