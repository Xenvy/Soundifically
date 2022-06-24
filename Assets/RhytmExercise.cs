using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RhytmExercise : MonoBehaviour
{

    private AudioSource audio_source;
    private AudioSource audio_source2;
    private AudioSource audio_source3;
    private AudioSource audio_source4;
    private Vector3 initial_position;
    private Vector3 end_position;
    private float step;
    public int[] random_kick;
    public int[] random_snare;
    public int[] random_ohat;
    public int[] random_chat;
    private bool play_started;
    public TextMeshProUGUI score_value;
    public TextMeshProUGUI exercise_progress;
    public Slider bpm;
    public Slider difficulty;
    private float score = 0;
    public Toggle[] kick;
    public Toggle[] snare;
    public Toggle[] ohat;
    public Toggle[] chat;
    public Toggle play_button;
    public Image[] beat_correct;
    public Image[] beat_missed;
    public Image[] beat_wrong;
    private float correct_count = 0;
    private float missed_count = 0;
    private float wrong_count = 0;
    private int attempt = 0;
    private float diff_bonus = 0;
    GameObject check_button;
    GameObject next_button;
    GameObject time_cursor;
    GameObject dummy_cursor;

    void Awake()
    {

        check_button = GameObject.Find("CheckButton");
        next_button = GameObject.Find("NextButton");
        time_cursor = GameObject.Find("TimeCursor");
        dummy_cursor = GameObject.Find("DummyCursor");

        audio_source4 = FindObjectOfType<AudioSource>();
        audio_source = FindObjectsOfType<AudioSource>()[1];
        audio_source2 = FindObjectsOfType<AudioSource>()[2];
        audio_source3 = FindObjectsOfType<AudioSource>()[3];

        ScoreManager.Instance.incorrect_count = 0;
        ScoreManager.Instance.excellent_count = 0;
        ScoreManager.Instance.close_count = 0;

        RandomPattern();

        next_button.SetActive(false);

        StartCoroutine(GetInitialPosition());
    }

    private void RandomPattern()
    {
        for(int i = 0; i < 16; i += 8)
        {
            random_kick[i] = Random.Range(0, 7 - (int)difficulty.value);
            random_snare[i] = Random.Range(0, 10 - (int)difficulty.value);
            random_ohat[i] = Random.Range(0, 10 - (int)difficulty.value);
            random_chat[i] = Random.Range(0, 13 - (int)difficulty.value*2);
        }

        for (int i = 4; i < 16; i += 8)
        {
            random_kick[i] = Random.Range(0, 8 - (int)difficulty.value);
            random_snare[i] = Random.Range(0, 7 - (int)difficulty.value);
            random_ohat[i] = Random.Range(0, 10 - (int)difficulty.value);
            random_chat[i] = Random.Range(0, 13 - (int)difficulty.value*2);
        }

        for (int i = 2; i < 16; i += 4)
        {
            random_kick[i] = Random.Range(0, 19 - (int)difficulty.value*3);
            random_snare[i] = Random.Range(0, 21 - (int)difficulty.value*3);
            random_ohat[i] = Random.Range(0, 7 - (int)difficulty.value);
            random_chat[i] = Random.Range(0, 8 - (int)difficulty.value);
        }

        for (int i = 1; i < 16; i += 2)
        {
            random_kick[i] = Random.Range(0, 32 - (int)difficulty.value*4);
            random_snare[i] = Random.Range(0, 45 - (int)difficulty.value*4);
            random_ohat[i] = Random.Range(0, 17 - (int)difficulty.value*2);
            random_chat[i] = Random.Range(0, 11 - (int)difficulty.value);
        }
    }

    IEnumerator GetInitialPosition()
    {
        yield return new WaitForSeconds(0.5f);
        initial_position = time_cursor.transform.position;
        end_position = dummy_cursor.transform.position;
        step = (end_position.x - initial_position.x) / 160;
    }

    IEnumerator PlaySamples()
    {      
        for (int i = 0; i < 160; i++)
        {           
            if(i % 10 == 0)
            {
                if (random_kick[i/10] == 0)
                {
                    audio_source.Stop();
                    audio_source.Play();
                }
                if (random_snare[i/10] == 0)
                {
                    audio_source2.Play();
                }
                if (random_ohat[i/10] == 0)
                {
                    audio_source3.Play();
                }
                if (random_chat[i/10] == 0)
                {
                    audio_source4.Play();
                }
            }

            if(i > bpm.value*bpm.value/2500)
            {
                time_cursor.transform.position += Vector3.right * step;
            }
            else if(i > bpm.value * bpm.value / 3600)
            {
                time_cursor.transform.position = initial_position;
            }
            else if(play_started)
            {
                time_cursor.transform.position += Vector3.right * step;
            }

            yield return new WaitForSeconds(1.5f/bpm.value);
        }
        play_started = true;
        PlayPattern();
    }


    public void PlayPattern()
    {
        if (!play_button.isOn)
        {
            play_started = false;
            StopAllCoroutines();
            time_cursor.transform.position = initial_position;
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(PlaySamples());            
        }       
    }

    public void CheckCorrect()
    {

        for(int i = 0; i < 16;  i++)
        {
            if(kick[i].isOn == true && random_kick[i] == 0)
            {
                beat_correct[i].enabled = true;
                correct_count++;
            }
            else if(kick[i].isOn == true && random_kick[i] != 0)
            {
                beat_wrong[i].enabled = true;
                wrong_count++;
            }
            else if(kick[i].isOn == false && random_kick[i] == 0)
            {
                beat_missed[i].enabled = true;
                missed_count++;
            }

            if (snare[i].isOn == true && random_snare[i] == 0)
            {
                beat_correct[i+16].enabled = true;
                correct_count++;
            }
            else if (snare[i].isOn == true && random_snare[i] != 0)
            {
                beat_wrong[i+16].enabled = true;
                wrong_count++;
            }
            else if (snare[i].isOn == false && random_snare[i] == 0)
            {
                beat_missed[i+16].enabled = true;
                missed_count++;
            }

            if (ohat[i].isOn == true && random_ohat[i] == 0)
            {
                beat_correct[i+32].enabled = true;
                correct_count++;
            }
            else if (ohat[i].isOn == true && random_ohat[i] != 0)
            {
                beat_wrong[i+32].enabled = true;
                wrong_count++;
            }
            else if (ohat[i].isOn == false && random_ohat[i] == 0)
            {
                beat_missed[i+32].enabled = true;
                missed_count++;
            }

            if (chat[i].isOn == true && random_chat[i] == 0)
            {
                beat_correct[i+48].enabled = true;
                correct_count++;
            }
            else if (chat[i].isOn == true && random_chat[i] != 0)
            {
                beat_wrong[i+48].enabled = true;
                wrong_count++;
            }
            else if (chat[i].isOn == false && random_chat[i] == 0)
            {
                beat_missed[i+48].enabled = true;
                missed_count++;
            }
        }
        if(correct_count != 0)
        {
            score += (1 + difficulty.value * 0.2f) * 200 * (correct_count - wrong_count + 1.0f) / (missed_count + correct_count + 1.0f);
        }

        ScoreManager.Instance.incorrect_count += (int)wrong_count;
        ScoreManager.Instance.excellent_count += (int)correct_count;
        ScoreManager.Instance.close_count += (int)missed_count;

        correct_count = 0;
        missed_count = 0;
        wrong_count = 0;

        score_value.text = score.ToString("N0");
        check_button.SetActive(false);
        next_button.SetActive(true);
        attempt++;
        diff_bonus += difficulty.value;
    }

    public void Play_Kick()
    {
        audio_source.Play();
    }

    public void Play_Snare()
    {
        audio_source2.Play();
    }

    public void Play_OHat()
    {
        audio_source3.Play();
    }

    public void Play_CHat()
    {
        audio_source4.Play();
    }

    public void Next()
    {
        if(attempt<8)
        {
            for (int i = 0; i < 64; i++)
            {
                beat_correct[i].enabled = false;
                beat_missed[i].enabled = false;
                beat_wrong[i].enabled = false;
            }
            for (int i = 0; i < 16; i++)
            {
                kick[i].isOn = false;
                snare[i].isOn = false;
                ohat[i].isOn = false;
                chat[i].isOn = false;
            }
            check_button.SetActive(true);
            next_button.SetActive(false);
            exercise_progress.text = (attempt + 1).ToString() + "/8";
            RandomPattern();
        }
        else
        {
            ScoreManager.Instance.score = (int)score;
            ScoreManager.Instance.exercise_id = 4;
            ScoreManager.Instance.difficulty = diff_bonus/0.8f;
            SceneManager.LoadScene("Score Summary");

            if(PlayerPrefs.GetInt("Rhytm Highscore", 0) < score)
            {
                PlayerPrefs.SetInt("Rhytm Highscore", (int)score);
                ScoreManager.Instance.highscore_beaten = true;
            }
        }
        
    }

    public void Scene_Selection()
    {
        SceneManager.LoadScene("Exercise Selection");
    }

}
