using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Highscores : MonoBehaviour
{
    public TextMeshProUGUI compression_highscore;
    public TextMeshProUGUI cutoff_highscore;
    public TextMeshProUGUI eq_highscore;
    public TextMeshProUGUI level_highscore;
    public TextMeshProUGUI rhytm_highscore;
    public TextMeshProUGUI tonal_highscore;

    private void Awake()
    {
        compression_highscore.text = PlayerPrefs.GetInt("Compression Highscore", 0).ToString();
        cutoff_highscore.text = PlayerPrefs.GetInt("Cutoff Highscore", 0).ToString();
        eq_highscore.text = PlayerPrefs.GetInt("EQ Highscore", 0).ToString();
        level_highscore.text = PlayerPrefs.GetInt("Level Highscore", 0).ToString();
        rhytm_highscore.text = PlayerPrefs.GetInt("Rhytm Highscore", 0).ToString();
        tonal_highscore.text = PlayerPrefs.GetInt("Tonal Highscore", 0).ToString();
    }
    public void Back_To_Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
