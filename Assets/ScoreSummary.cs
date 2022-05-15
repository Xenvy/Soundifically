using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreSummary : MonoBehaviour
{
    public TextMeshProUGUI perfect_value;
    public TextMeshProUGUI excellent_value;
    public TextMeshProUGUI good_value;
    public TextMeshProUGUI close_value;
    public TextMeshProUGUI difficulty_bonus;
    public TextMeshProUGUI perfect_text;
    public TextMeshProUGUI excellent_text;
    public TextMeshProUGUI good_text;
    public TextMeshProUGUI close_text;
    public TextMeshProUGUI difficulty_text;
    public TextMeshProUGUI total_score;

    private void Awake()
    {
        total_score.text = "Total score: " + ScoreManager.Instance.score.ToString();
        switch (ScoreManager.Instance.exercise_id)
        {
            case 0:
                perfect_text.text = "Correct answers (x" + (16 - ScoreManager.Instance.incorrect_count).ToString() + ")";
                perfect_value.text = ScoreManager.Instance.score.ToString();
                excellent_text.text = " ";
                good_text.text = " ";
                close_text.text = " ";
                difficulty_text.text = " ";
                excellent_value.text = " ";
                good_value.text = " ";
                close_value.text = " ";
                difficulty_bonus.text = " ";
                break;

            case 1:
                perfect_text.text = "Perfect (x" + ScoreManager.Instance.perfect_count.ToString() + ")";
                perfect_value.text = (ScoreManager.Instance.perfect_count * 500).ToString();
                excellent_text.text = "Excellent (x" + ScoreManager.Instance.excellent_count.ToString() + ")";
                good_text.text = "Good (x" + ScoreManager.Instance.good_count.ToString() + ")";
                close_text.text = "Close (x" + ScoreManager.Instance.close_count.ToString() + ")";
                difficulty_text.text = " ";
                excellent_value.text = (ScoreManager.Instance.excellent_count * 200).ToString();
                good_value.text = (ScoreManager.Instance.good_count * 100).ToString();
                close_value.text = (ScoreManager.Instance.close_count * 50).ToString();
                difficulty_bonus.text = " ";
                break;

            case 2:
                perfect_text.text = "Perfect (x" + ScoreManager.Instance.perfect_count.ToString() + ")";
                perfect_value.text = (ScoreManager.Instance.perfect_count * 500).ToString();
                excellent_text.text = "Excellent (x" + ScoreManager.Instance.excellent_count.ToString() + ")";
                good_text.text = "Good (x" + ScoreManager.Instance.good_count.ToString() + ")";
                close_text.text = "Close (x" + ScoreManager.Instance.close_count.ToString() + ")";
                difficulty_text.text = " ";
                excellent_value.text = (ScoreManager.Instance.excellent_count * 200).ToString();
                good_value.text = (ScoreManager.Instance.good_count * 100).ToString();
                close_value.text = (ScoreManager.Instance.close_count * 50).ToString();
                difficulty_bonus.text = " ";
                break;

            case 3:
                perfect_text.text = "Perfect (x" + ScoreManager.Instance.perfect_count.ToString() + ")";
                perfect_value.text = (ScoreManager.Instance.perfect_count * 500).ToString();
                excellent_text.text = "Excellent (x" + ScoreManager.Instance.excellent_count.ToString() + ")";
                good_text.text = "Good (x" + ScoreManager.Instance.good_count.ToString() + ")";
                close_text.text = "Close (x" + ScoreManager.Instance.close_count.ToString() + ")";
                difficulty_text.text = " ";
                excellent_value.text = (ScoreManager.Instance.excellent_count * 200).ToString();
                good_value.text = (ScoreManager.Instance.good_count * 100).ToString();
                close_value.text = (ScoreManager.Instance.close_count * 50).ToString();
                difficulty_bonus.text = " ";
                break;

            case 4:
                perfect_text.text = "Correct hits";
                perfect_value.text = ScoreManager.Instance.excellent_count.ToString();
                excellent_text.text = "Wrong hits";
                good_text.text = "Missed hits";
                close_text.text = " ";
                difficulty_text.text = "Difficulty bonus";
                excellent_value.text = ScoreManager.Instance.incorrect_count.ToString();
                good_value.text = ScoreManager.Instance.close_count.ToString();
                close_value.text = " ";
                difficulty_bonus.text = "+" + ScoreManager.Instance.close_count.ToString() + @"%";
                break;

            case 5:
                perfect_text.text = "Notes (x" + ScoreManager.Instance.note_mode.ToString() + ")";
                perfect_value.text = (ScoreManager.Instance.note_mode * 100).ToString();
                excellent_text.text = "Intervals (x" + ScoreManager.Instance.interval_mode.ToString() + ")";
                good_text.text = "Chords (x" + ScoreManager.Instance.chord_mode.ToString() + ")";
                close_text.text = "Advanced chords bonus (x" + ScoreManager.Instance.advanced_mode.ToString() + ")";
                difficulty_text.text = "Correct key bonus (x" + ScoreManager.Instance.key_mode.ToString() + ")";
                excellent_value.text = (ScoreManager.Instance.interval_mode * 100).ToString();
                good_value.text = (ScoreManager.Instance.chord_mode * 100).ToString();
                close_value.text = "+" + (ScoreManager.Instance.advanced_mode * 20).ToString();
                difficulty_bonus.text = "+" + (ScoreManager.Instance.key_mode * 30).ToString();
                break;

            default:
                break;
        }
    }

    public void Try_Again()
    {
        switch (ScoreManager.Instance.exercise_id)
        {
            case 0:
                SceneManager.LoadScene("Compression Exercise Info");
                break;

            case 1:
                SceneManager.LoadScene("CutoffFreq Exercise Info");
                break;

            case 2:
                SceneManager.LoadScene("EQ Exercise Info");
                break;

            case 3:
                SceneManager.LoadScene("LevelDiff Exercise Info");
                break;

            case 4:
                SceneManager.LoadScene("Rhytm Exercise Info");
                break;

            case 5:
                SceneManager.LoadScene("TonalAnalysis Exercise Info");
                break;

            default:
                break;
        }
    }
    public void Back_To_Menu()
    {
        SceneManager.LoadScene("Exercise Selection");
    }
}
