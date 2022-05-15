using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExerciseSelection : MonoBehaviour
{
    public void Compression_Exercise()
    {
        SceneManager.LoadScene("Compression Exercise Info");
    }

    public void Cutoff_Exercise()
    {
        SceneManager.LoadScene("CutoffFreq Exercise Info");
    }

    public void EQ_Exercise()
    {
        SceneManager.LoadScene("EQ Exercise Info");
    }

    public void LevelDiff_Exercise()
    {
        SceneManager.LoadScene("LevelDiff Exercise Info");
    }

    public void Rhytm_Exercise()
    {
        SceneManager.LoadScene("Rhytm Exercise Info");
    }

    public void Tonal_Exercise()
    {
        SceneManager.LoadScene("TonalAnalysis Exercise Info");
    }

    public void Main_Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}