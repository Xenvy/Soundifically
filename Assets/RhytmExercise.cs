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
    public int[] random_kick;
    public int[] random_snare;
    public int[] random_ohat;
    public int[] random_chat;
    private bool play_started;
    public TextMeshProUGUI score_value;
    public Slider bpm;
    public Slider difficulty;
    private int score;
    public Toggle[] kick;
    public Toggle[] snare;
    public Toggle[] ohat;
    public Toggle[] chat;
    public Toggle play_button;
    public Image[] beat_correct;
    public Image[] beat_missed;
    public Image[] beat_wrong;
    GameObject check_button;
    GameObject next_button;
    GameObject time_cursor;

    void Awake()
    {

        check_button = GameObject.Find("CheckButton");
        next_button = GameObject.Find("NextButton");
        time_cursor = GameObject.Find("TimeCursor");

        audio_source4 = FindObjectOfType<AudioSource>();
        audio_source = FindObjectsOfType<AudioSource>()[1];
        audio_source2 = FindObjectsOfType<AudioSource>()[2];
        audio_source3 = FindObjectsOfType<AudioSource>()[3];

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
            random_ohat[i] = Random.Range(0, 12 - (int)difficulty.value);
            random_chat[i] = Random.Range(0, 10 - (int)difficulty.value);
        }

        for (int i = 4; i < 16; i += 8)
        {
            random_kick[i] = Random.Range(0, 8 - (int)difficulty.value);
            random_snare[i] = Random.Range(0, 6 - (int)difficulty.value);
            random_ohat[i] = Random.Range(0, 12 - (int)difficulty.value);
            random_chat[i] = Random.Range(0, 10 - (int)difficulty.value);
        }

        for (int i = 2; i < 16; i += 4)
        {
            random_kick[i] = Random.Range(0, 10 - (int)difficulty.value);
            random_snare[i] = Random.Range(0, 16 - (int)difficulty.value*2);
            random_ohat[i] = Random.Range(0, 7 - (int)difficulty.value);
            random_chat[i] = Random.Range(0, 10 - (int)difficulty.value);
        }

        for (int i = 1; i < 16; i += 2)
        {
            random_kick[i] = Random.Range(0, 45 - (int)difficulty.value*5);
            random_snare[i] = Random.Range(0, 60 - (int)difficulty.value*5);
            random_ohat[i] = Random.Range(0, 16 - (int)difficulty.value*2);
            random_chat[i] = Random.Range(0, 9 - (int)difficulty.value);
        }
    }

    IEnumerator GetInitialPosition()
    {
        yield return new WaitForSeconds(0.5f);
        initial_position = time_cursor.transform.position;
    }

    IEnumerator PlaySamples()
    {      
        for (int i = 0; i < 160; i++)
        {           
            if(i % 10 == 0)
            {
                if (random_kick[i/10] == 0)
                {
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
                time_cursor.transform.position += Vector3.right * 6.9f;
            }
            else if(i > bpm.value * bpm.value / 3600)
            {
                time_cursor.transform.position = initial_position;
            }
            else if(play_started)
            {
                time_cursor.transform.position += Vector3.right * 6.9f;
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
            }
            else if(kick[i].isOn == true && random_kick[i] != 0)
            {
                beat_wrong[i].enabled = true;
            }
            else if(kick[i].isOn == false && random_kick[i] == 0)
            {
                beat_missed[i].enabled = true;
            }

            if (snare[i].isOn == true && random_snare[i] == 0)
            {
                beat_correct[i+16].enabled = true;
            }
            else if (snare[i].isOn == true && random_snare[i] != 0)
            {
                beat_wrong[i+16].enabled = true;
            }
            else if (snare[i].isOn == false && random_snare[i] == 0)
            {
                beat_missed[i+16].enabled = true;
            }

            if (ohat[i].isOn == true && random_ohat[i] == 0)
            {
                beat_correct[i+32].enabled = true;
            }
            else if (ohat[i].isOn == true && random_ohat[i] != 0)
            {
                beat_wrong[i+32].enabled = true;
            }
            else if (ohat[i].isOn == false && random_ohat[i] == 0)
            {
                beat_missed[i+32].enabled = true;
            }

            if (chat[i].isOn == true && random_chat[i] == 0)
            {
                beat_correct[i+48].enabled = true;
            }
            else if (chat[i].isOn == true && random_chat[i] != 0)
            {
                beat_wrong[i+48].enabled = true;
            }
            else if (chat[i].isOn == false && random_chat[i] == 0)
            {
                beat_missed[i+48].enabled = true;
            }
        }

        check_button.SetActive(false);
        next_button.SetActive(true);
    }

    public void Next()
    {
        for(int i=0; i<64; i++)
        {
            beat_correct[i].enabled = false;
            beat_missed[i].enabled = false;
            beat_wrong[i].enabled = false;
        }
        for(int i=0; i<16; i++)
        {
            kick[i].isOn = false;
            snare[i].isOn = false;
            ohat[i].isOn = false;
            chat[i].isOn = false;
        }
        check_button.SetActive(true);
        next_button.SetActive(false);
        RandomPattern();
    }

    public void Scene_Selection()
    {
        SceneManager.LoadScene("Exercise Selection");
    }

}
