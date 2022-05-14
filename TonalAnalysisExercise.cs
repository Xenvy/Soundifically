using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TonalAnalysisExercise : MonoBehaviour
{
    private int random_note;
    private int random_interval;
    private int random_chord_type;
    private int random_chord_key;
    private int random_chord_inver;
    private int random_chord_inver2;
    private int random_chord_inver3;
    private int random_chord_inver4;
    public AudioClip[] samples;
    private AudioSource audio_source;
    private AudioSource audio_source2;
    private AudioSource audio_source3;
    private AudioSource audio_source4;
    private AudioSource audio_source5;
    public TextMeshProUGUI score_value;
    public TextMeshProUGUI correct_answer;
    public TMP_Dropdown mode_select;
    public TMP_Dropdown note_ans;
    public TMP_Dropdown interval_ans;
    public TMP_Dropdown chord_ans;
    public TMP_Dropdown adv_chord_ans;
    public Toggle advanced_chords;
    public Toggle include_key;
    private int score;
    GameObject title;
    GameObject title2;
    GameObject title3;
    GameObject mode_selection;
    GameObject note_answer;
    GameObject chord_answer;
    GameObject chord_adv_answer;
    GameObject interval_answer;
    GameObject check_button;
    GameObject next_button;
    GameObject correct_text;
    GameObject incorrect_text;

    void Awake()
    {
        title = GameObject.Find("Title");
        title2 = GameObject.Find("Title2");
        title3 = GameObject.Find("Title3");
        mode_selection = GameObject.Find("ModeSelection");
        note_answer = GameObject.Find("AnsKey");
        chord_answer = GameObject.Find("AnsChordType");
        chord_adv_answer = GameObject.Find("AnsAdvChordType");
        interval_answer = GameObject.Find("AnsInterval");
        check_button = GameObject.Find("CheckButton");
        next_button = GameObject.Find("NextButton");
        correct_text = GameObject.Find("Correct");
        incorrect_text = GameObject.Find("Incorrect");
        audio_source = FindObjectOfType<AudioSource>();
        audio_source2 = FindObjectsOfType<AudioSource>()[1];
        audio_source3 = FindObjectsOfType<AudioSource>()[2];
        audio_source4 = FindObjectsOfType<AudioSource>()[3];
        audio_source5 = FindObjectsOfType<AudioSource>()[4];
        ModeSelect();
        title.SetActive(true);
        title2.SetActive(false);
        title3.SetActive(false);
        correct_text.SetActive(false);
        incorrect_text.SetActive(false);
        next_button.SetActive(false);
    }

    private int GetMode()
    {
        return mode_select.value;
    }

    public void ModeSelect()
    {
        switch(GetMode())
        {
            case 0:
                NoteMode();
                break;
            case 1:
                IntervalMode();
                break;
            case 2:
                ChordMode(advanced_chords.isOn, include_key.isOn);
                break;
            default:
                break;
        }
    }

    private void NoteMode()
    {
        title.SetActive(true);
        title2.SetActive(false);
        title3.SetActive(false);
        interval_answer.SetActive(false);
        chord_answer.SetActive(false);
        chord_adv_answer.SetActive(false);
        note_answer.SetActive(true);

        random_note = Random.Range(0, 12);
        audio_source.clip = samples[random_note + Random.Range(1, 4) * 12];
    }

    private void IntervalMode()
    {
        int start_note;

        title.SetActive(false);
        title2.SetActive(true);
        title3.SetActive(false);
        interval_answer.SetActive(true);
        chord_answer.SetActive(false);
        chord_adv_answer.SetActive(false);
        note_answer.SetActive(false);

        random_interval = Random.Range(1, 13);
        start_note = Random.Range(12, 48) - 12;
        audio_source.clip = samples[start_note];
        audio_source2.clip = samples[start_note + random_interval];
    }

    private void ChordMode(bool adv_toggle, bool key_toggle)
    {
        title.SetActive(false);
        title2.SetActive(false);
        title3.SetActive(true);
        interval_answer.SetActive(false);
        note_answer.SetActive(false);

        if(adv_toggle)
        {
            chord_answer.SetActive(false);
            chord_adv_answer.SetActive(true);

            if(key_toggle)
            {
                note_answer.SetActive(true);

            }
            else
            {
                note_answer.SetActive(false);
            }

            random_chord_type = Random.Range(0, 9);
            random_chord_key = Random.Range(0, 12);
            random_chord_inver = Random.Range(-1, 2);
            random_chord_inver2 = Random.Range(-1, 2);
            random_chord_inver3 = Random.Range(-1, 2);
            random_chord_inver4 = Random.Range(-1, 2);

            switch (random_chord_type)
            {
                case 0:
                    audio_source.clip = samples[random_chord_key + 24 + 12 * random_chord_inver];
                    audio_source4.clip = samples[random_chord_key];
                    if (random_chord_key > 7)
                    {
                        audio_source2.clip = samples[random_chord_key + 16 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 19 + 12 * random_chord_inver3];
                    }
                    else if (random_chord_key > 4)
                    {
                        audio_source2.clip = samples[random_chord_key + 28 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 19 + 12 * random_chord_inver3];
                    }
                    else
                    {
                        audio_source2.clip = samples[random_chord_key + 28 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 31 + 12 * random_chord_inver3];
                    }
                    break;

                case 1:
                    audio_source.clip = samples[random_chord_key + 24 + 12 * random_chord_inver];
                    audio_source4.clip = samples[random_chord_key];
                    if (random_chord_key > 8)
                    {
                        audio_source2.clip = samples[random_chord_key + 15 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 19 + 12 * random_chord_inver3];
                    }
                    else if (random_chord_key > 4)
                    {
                        audio_source2.clip = samples[random_chord_key + 27 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 19 + 12 * random_chord_inver3];
                    }
                    else
                    {
                        audio_source2.clip = samples[random_chord_key + 27 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 31 + 12 * random_chord_inver3];
                    }
                    break;

                case 2:
                    audio_source.clip = samples[random_chord_key + 24 + 12 * random_chord_inver];
                    audio_source4.clip = samples[random_chord_key];
                    if (random_chord_key > 8)
                    {
                        audio_source2.clip = samples[random_chord_key + 15 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 18 + 12 * random_chord_inver3];
                    }
                    else if (random_chord_key > 5)
                    {
                        audio_source2.clip = samples[random_chord_key + 27 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 18 + 12 * random_chord_inver3];
                    }
                    else
                    {
                        audio_source2.clip = samples[random_chord_key + 27 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 30 + 12 * random_chord_inver3];
                    }
                    break;

                case 3:
                    audio_source.clip = samples[random_chord_key + 24 + 12 * random_chord_inver];
                    audio_source4.clip = samples[random_chord_key];
                    if (random_chord_key > 7)
                    {
                        audio_source2.clip = samples[random_chord_key + 16 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 20 + 12 * random_chord_inver3];
                    }
                    else if (random_chord_key > 3)
                    {
                        audio_source2.clip = samples[random_chord_key + 28 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 20 + 12 * random_chord_inver3];
                    }
                    else
                    {
                        audio_source2.clip = samples[random_chord_key + 28 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 32 + 12 * random_chord_inver3];
                    }
                    break;

                case 4:
                    audio_source.clip = samples[random_chord_key + 24 + 12 * random_chord_inver];
                    audio_source4.clip = samples[random_chord_key];
                    if (random_chord_key > 9)
                    {
                        audio_source2.clip = samples[random_chord_key + 14 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 19 + 12 * random_chord_inver3];
                    }
                    else if (random_chord_key > 4)
                    {
                        audio_source2.clip = samples[random_chord_key + 26 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 19 + 12 * random_chord_inver3];
                    }
                    else
                    {
                        audio_source2.clip = samples[random_chord_key + 26 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 31 + 12 * random_chord_inver3];
                    }
                    break;

                case 5:
                    audio_source.clip = samples[random_chord_key + 24 + 12 * random_chord_inver];
                    audio_source4.clip = samples[random_chord_key];
                    if (random_chord_key > 6)
                    {
                        audio_source2.clip = samples[random_chord_key + 17 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 19 + 12 * random_chord_inver3];
                    }
                    else if (random_chord_key > 4)
                    {
                        audio_source2.clip = samples[random_chord_key + 29 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 19 + 12 * random_chord_inver3];
                    }
                    else
                    {
                        audio_source2.clip = samples[random_chord_key + 29 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 31 + 12 * random_chord_inver3];
                    }
                    break;

                case 6:
                    audio_source.clip = samples[random_chord_key + 24 + 12 * random_chord_inver];
                    audio_source4.clip = samples[random_chord_key];
                    if (random_chord_key > 7)
                    {
                        audio_source2.clip = samples[random_chord_key + 16 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 19 + 12 * random_chord_inver3];
                        audio_source5.clip = samples[random_chord_key + 22 + 12 * random_chord_inver4];
                    }
                    else if (random_chord_key > 4)
                    {
                        audio_source2.clip = samples[random_chord_key + 28 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 19 + 12 * random_chord_inver3];
                        audio_source5.clip = samples[random_chord_key + 22 + 12 * random_chord_inver4];
                    }
                    else if (random_chord_key > 1)
                    {
                        audio_source2.clip = samples[random_chord_key + 28 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 31 + 12 * random_chord_inver3];
                        audio_source5.clip = samples[random_chord_key + 22 + 12 * random_chord_inver4];
                    }
                    else
                    {
                        audio_source2.clip = samples[random_chord_key + 28 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 31 + 12 * random_chord_inver3];
                        audio_source5.clip = samples[random_chord_key + 34 + 12 * random_chord_inver4];
                    }
                    break;

                case 7:
                    audio_source.clip = samples[random_chord_key + 24 + 12 * random_chord_inver];
                    audio_source4.clip = samples[random_chord_key];
                    if (random_chord_key > 7)
                    {
                        audio_source2.clip = samples[random_chord_key + 16 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 19 + 12 * random_chord_inver3];
                        audio_source5.clip = samples[random_chord_key + 23 + 12 * random_chord_inver4];
                    }
                    else if (random_chord_key > 4)
                    {
                        audio_source2.clip = samples[random_chord_key + 28 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 19 + 12 * random_chord_inver3];
                        audio_source5.clip = samples[random_chord_key + 23 + 12 * random_chord_inver4];
                    }
                    else if (random_chord_key > 0)
                    {
                        audio_source2.clip = samples[random_chord_key + 28 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 31 + 12 * random_chord_inver3];
                        audio_source5.clip = samples[random_chord_key + 23 + 12 * random_chord_inver4];
                    }
                    else
                    {
                        audio_source2.clip = samples[random_chord_key + 28 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 31 + 12 * random_chord_inver3];
                        audio_source5.clip = samples[random_chord_key + 35 + 12 * random_chord_inver4];
                    }
                    break;

                case 8:
                    audio_source.clip = samples[random_chord_key + 24 + 12 * random_chord_inver];
                    audio_source4.clip = samples[random_chord_key];
                    if (random_chord_key > 8)
                    {
                        audio_source2.clip = samples[random_chord_key + 15 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 19 + 12 * random_chord_inver3];
                        audio_source5.clip = samples[random_chord_key + 22 + 12 * random_chord_inver4];
                    }
                    else if (random_chord_key > 4)
                    {
                        audio_source2.clip = samples[random_chord_key + 27 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 19 + 12 * random_chord_inver3];
                        audio_source5.clip = samples[random_chord_key + 22 + 12 * random_chord_inver4];
                    }
                    else if (random_chord_key > 1)
                    {
                        audio_source2.clip = samples[random_chord_key + 27 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 31 + 12 * random_chord_inver3];
                        audio_source5.clip = samples[random_chord_key + 22 + 12 * random_chord_inver4];
                    }
                    else
                    {
                        audio_source2.clip = samples[random_chord_key + 27 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 31 + 12 * random_chord_inver3];
                        audio_source5.clip = samples[random_chord_key + 34 + 12 * random_chord_inver4];
                    }
                    break;

                default:
                    break;

            }

        }
        else
        {
            chord_answer.SetActive(true);
            chord_adv_answer.SetActive(false);

            if(key_toggle)
            {
                note_answer.SetActive(true);
            }
            else
            {
                note_answer.SetActive(false);
            }

            random_chord_type = Random.Range(0, 4);
            random_chord_key = Random.Range(0, 12);
            random_chord_inver = Random.Range(-1, 2);
            random_chord_inver2 = Random.Range(-1, 2);
            random_chord_inver3 = Random.Range(-1, 2);

            switch (random_chord_type)
            {
                case 0:
                    audio_source.clip = samples[random_chord_key + 24 + 12 * random_chord_inver];
                    audio_source4.clip = samples[random_chord_key];
                    if (random_chord_key > 7)
                    {
                        audio_source2.clip = samples[random_chord_key + 16 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 19 + 12 * random_chord_inver3];
                    }
                    else if (random_chord_key > 4)
                    {
                        audio_source2.clip = samples[random_chord_key + 28 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 19 + 12 * random_chord_inver3];
                    }
                    else
                    {
                        audio_source2.clip = samples[random_chord_key + 28 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 31 + 12 * random_chord_inver3];
                    }
                    break;

                case 1:
                    audio_source.clip = samples[random_chord_key + 24 + 12 * random_chord_inver];
                    audio_source4.clip = samples[random_chord_key];
                    if (random_chord_key > 8)
                    {
                        audio_source2.clip = samples[random_chord_key + 15 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 19 + 12 * random_chord_inver3];
                    }
                    else if (random_chord_key > 4)
                    {
                        audio_source2.clip = samples[random_chord_key + 27 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 19 + 12 * random_chord_inver3];
                    }
                    else
                    {
                        audio_source2.clip = samples[random_chord_key + 27 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 31 + 12 * random_chord_inver3];
                    }
                    break;

                case 2:
                    audio_source.clip = samples[random_chord_key + 24 + 12 * random_chord_inver];
                    audio_source4.clip = samples[random_chord_key];
                    if (random_chord_key > 8)
                    {
                        audio_source2.clip = samples[random_chord_key + 15 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 18 + 12 * random_chord_inver3];
                    }
                    else if (random_chord_key > 5)
                    {
                        audio_source2.clip = samples[random_chord_key + 27 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 18 + 12 * random_chord_inver3];
                    }
                    else
                    {
                        audio_source2.clip = samples[random_chord_key + 27 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 30 + 12 * random_chord_inver3];
                    }
                    break;

                case 3:
                    audio_source.clip = samples[random_chord_key + 24 + 12 * random_chord_inver];
                    audio_source4.clip = samples[random_chord_key];
                    if (random_chord_key > 9)
                    {
                        audio_source2.clip = samples[random_chord_key + 14 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 19 + 12 * random_chord_inver3];
                    }
                    else if (random_chord_key > 4)
                    {
                        audio_source2.clip = samples[random_chord_key + 26 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 19 + 12 * random_chord_inver3];
                    }
                    else
                    {
                        audio_source2.clip = samples[random_chord_key + 26 + 12 * random_chord_inver2];
                        audio_source3.clip = samples[random_chord_key + 31 + 12 * random_chord_inver3];
                    }
                    break;

                default:
                    break;

            }

        }

    }

    public void PlaySamples()
    {
        switch(GetMode())
        {
            case 0:
                audio_source.Play();
                break;
            case 1:
                audio_source.Play();
                audio_source2.Play();
                break;
            case 2:
                audio_source.Play();
                audio_source2.Play();
                audio_source3.Play();
                audio_source4.Play();
                if(random_chord_type > 5)
                {
                    audio_source5.Play();
                }
                break;
            default:
                break;
        }
    }

    private void CheckNote()
    {
        if (random_note == note_ans.value)
        {
            correct_text.SetActive(true);           
        }
        else
        {
            incorrect_text.SetActive(true);
        }
        check_button.SetActive(false);
        correct_answer.text = note_ans.options[random_note].text;
        next_button.SetActive(true);
    }

    private void CheckInterval()
    {
        if (random_interval == interval_ans.value + 1)
        {
            correct_text.SetActive(true);
        }
        else
        {
            incorrect_text.SetActive(true);         
        }
        check_button.SetActive(false);
        correct_answer.text = interval_ans.options[random_interval - 1].text;
        next_button.SetActive(true);
    }

    private void CheckChord()
    {
        if(advanced_chords.isOn)
        {
            if (include_key.isOn)
            {
                if (random_chord_type == adv_chord_ans.value)
                {
                    if (random_chord_key == note_ans.value)
                    {
                        correct_text.SetActive(true);
                    }
                    else
                    {
                        incorrect_text.SetActive(true);
                    }
                }
                else
                {
                    incorrect_text.SetActive(true);
                }
                correct_answer.text = note_ans.options[random_chord_key].text + " " + adv_chord_ans.options[random_chord_type].text;
            }
            else
            {
                if (random_chord_type == adv_chord_ans.value)
                {
                    correct_text.SetActive(true);
                }
                else
                {
                    incorrect_text.SetActive(true);
                }
                correct_answer.text = adv_chord_ans.options[random_chord_type].text;
            }
        }
        else
        {
            if (include_key.isOn)
            {
                if (random_chord_type == chord_ans.value)
                {
                    if (random_chord_key == note_ans.value)
                    {
                        correct_text.SetActive(true);
                    }
                    else
                    {
                        incorrect_text.SetActive(true);
                    }
                }
                else
                {
                    incorrect_text.SetActive(true);
                }
                correct_answer.text = note_ans.options[random_chord_key].text + " " + chord_ans.options[random_chord_type].text;
            }
            else
            {
                if (random_chord_type == chord_ans.value)
                {
                    correct_text.SetActive(true);
                }
                else
                {
                    incorrect_text.SetActive(true);
                }
                correct_answer.text = chord_ans.options[random_chord_type].text;
            }
        }
        
        
        check_button.SetActive(false);      
        next_button.SetActive(true);
    }

    public void CheckCorrect()
    {
        switch (GetMode())
        {
            case 0:
                CheckNote();
                break;
            case 1:
                CheckInterval();
                break;
            case 2:
                CheckChord();
                break;
            default:
                break;
        }
    }

    public void Next()
    {
        ModeSelect();
        correct_text.SetActive(false);
        incorrect_text.SetActive(false);
        check_button.SetActive(true);
        next_button.SetActive(false);
        correct_answer.text = "";
    }

    public void Scene_Selection()
    {
        SceneManager.LoadScene("Exercise Selection");
    }

}