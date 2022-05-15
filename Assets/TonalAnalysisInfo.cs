using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TonalAnalysisInfo : MonoBehaviour
{
    public void Exercise_Start()
    {
        SceneManager.LoadScene("TonalAnalysis Exercise");
    }

    public void Back_To_Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
