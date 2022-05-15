using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RhytmInfo : MonoBehaviour
{
    public void Exercise_Start()
    {
        SceneManager.LoadScene("Rhytm Exercise");
    }

    public void Back_To_Menu()
    {
        SceneManager.LoadScene("Exercise Selection");
    }
}
