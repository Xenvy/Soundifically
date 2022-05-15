using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompressionInfo : MonoBehaviour
{
    public void Exercise_Start()
    {
        SceneManager.LoadScene("Compression Exercise");
    }

    public void Back_To_Menu()
    {
        SceneManager.LoadScene("Exercise Selection");
    }
}
