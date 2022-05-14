using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExerciseSelection : MonoBehaviour
{
    public void Compression_Exercise()
    {
        SceneManager.LoadScene("Compression Exercise");
    }

    public void Cutoff_Exercise()
    {
        SceneManager.LoadScene("CutoffFreq Exercise");
    }

    public void EQ_Exercise()
    {
        SceneManager.LoadScene("EQ Exercise");
    }

    public void LevelDiff_Exercise()
    {
        SceneManager.LoadScene("LevelDiff Exercise");
    }

    public void Rhytm_Exercise()
    {
        SceneManager.LoadScene("Rhytm Exercise");
    }

    public void Tonal_Exercise()
    {
        SceneManager.LoadScene("TonalAnalysis Exercise");
    }

    public void Main_Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}