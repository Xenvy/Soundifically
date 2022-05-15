using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutoffFreqInfo : MonoBehaviour
{
    public void Exercise_Start()
    {
        SceneManager.LoadScene("CutoffFreq Exercise");
    }

    public void Back_To_Menu()
    {
        SceneManager.LoadScene("Exercise Selection");
    }
}
