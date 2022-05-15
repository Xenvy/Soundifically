using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class EQExcercise : MonoBehaviour
{
    private int sample;
    public List<AudioClip> samples;
    private AudioSource audio_source;
    private AudioSource audio_source2;
    private double correct_frequency;
    private int is_boosted;
    public Slider frequency_slider;
    public TextMeshProUGUI current_frequency;
    public TextMeshProUGUI CorrectFrequency;
    public TextMeshProUGUI ScoreValue;
    private int score;
    private double slider_value;
    private int attempt;
    private int perfect_c = 0;
    private int excellent_c = 0;
    private int good_c = 0;
    private int close_c = 0;
    private int incorrect_c = 0;
    GameObject text;
    GameObject text2;
    GameObject text3;
    GameObject text4;
    GameObject text5;
    GameObject text6;
    GameObject text7;
    GameObject text8;
    GameObject text9;
    GameObject text10;
    DirectoryInfo cfd_info;
    public AudioMixer main_mixer;
    public Toggle play_button;
    public Toggle play_button2;

    private void ChooseRandomSample()
    {
        sample = Random.Range(0, samples.Count);

        is_boosted = Random.Range(0, 2);

        if(is_boosted == 0)
        {
            main_mixer.SetFloat("FilterGain", 0.65f);
        }
        else
        {
            main_mixer.SetFloat("FilterGain", 1.5f);
        }

        correct_frequency = 40 * Mathf.Pow(2, Random.Range(0.5f, 8.0f));

        main_mixer.SetFloat("FilterFreq", (float)correct_frequency);

        CorrectFrequency.text = correct_frequency.ToString("N0") + " Hz";
    }
    void Start()
    {
        audio_source = FindObjectOfType<AudioSource>();
        audio_source2 = FindObjectsOfType<AudioSource>()[1];
        string custom_file_directory = Application.persistentDataPath + "//EQ Exercise";
        StartCoroutine(LoadCustomAudio(custom_file_directory));
        ChooseRandomSample();
        text = GameObject.Find("Title");
        text2 = GameObject.Find("Title2");
        text3 = GameObject.Find("Perfect");
        text4 = GameObject.Find("Excellent");
        text5 = GameObject.Find("Good");
        text6 = GameObject.Find("Close");
        text7 = GameObject.Find("TryAgain");
        text8 = GameObject.Find("NextButton");
        text9 = GameObject.Find("CheckButton");
        text10 = GameObject.Find("CorrectFrequency");
        if(is_boosted == 0)
        {
            text.SetActive(false);
        }
        else
        {
            text2.SetActive(false);
        }
        text3.SetActive(false);
        text4.SetActive(false);
        text5.SetActive(false);
        text6.SetActive(false);
        text7.SetActive(false);
        text8.SetActive(false);
        text10.SetActive(false);
    }

    IEnumerator LoadCustomAudio(string cfd)
    {
        cfd_info = new DirectoryInfo(cfd);

        foreach (FileInfo fi in cfd_info.GetFiles())
        {
            if (fi.Extension == ".mp3")
            {
                using (UnityWebRequest get_custom_file = UnityWebRequestMultimedia.GetAudioClip(fi.FullName, AudioType.MPEG))
                {
                    yield return get_custom_file.SendWebRequest();

                    if (get_custom_file.result != UnityWebRequest.Result.ConnectionError)
                    {
                        samples.Add(DownloadHandlerAudioClip.GetContent(get_custom_file));
                    }
                }
            }
            else if (fi.Extension == ".wav")
            {
                using (UnityWebRequest get_custom_file = UnityWebRequestMultimedia.GetAudioClip(fi.FullName, AudioType.WAV))
                {
                    yield return get_custom_file.SendWebRequest();

                    if (get_custom_file.result != UnityWebRequest.Result.ConnectionError)
                    {
                        samples.Add(DownloadHandlerAudioClip.GetContent(get_custom_file));
                    }
                }
            }

        }
    }

    private void Update()
    {
        current_frequency.text = (20 * Mathf.Pow(2, frequency_slider.value)).ToString("N0") + " Hz";        
    }
    public void PlaySample1()
    {
        if(play_button.isOn)
        {
            if(!play_button2.isOn)
            {
                main_mixer.SetFloat("EQVolume1", -4.0f);
                main_mixer.SetFloat("EQVolume2", -80.0f);
                audio_source.clip = samples[sample];
                audio_source.Play();
                audio_source2.clip = samples[sample];
                audio_source2.Play();
            }
            else
            {
                main_mixer.SetFloat("EQVolume1", -4.0f);
                main_mixer.SetFloat("EQVolume2", -80.0f);
                play_button2.isOn = false;
            }
        }
        else
        {
            if (!play_button2.isOn)
            {
                audio_source.Stop();
                audio_source2.Stop();
            }
        }
    }
    public void Playsample2()
    {
        if (play_button2.isOn)
        {
            if(!play_button.isOn)
            {
                main_mixer.SetFloat("EQVolume1", -80.0f);
                main_mixer.SetFloat("EQVolume2", -4.0f);
                audio_source.clip = samples[sample];
                audio_source.Play();
                audio_source2.clip = samples[sample];
                audio_source2.Play();
            }
            else
            {
                main_mixer.SetFloat("EQVolume1", -80.0f);
                main_mixer.SetFloat("EQVolume2", -4.0f);
                play_button.isOn = false;
            }
        }
        else
        {
            if(!play_button.isOn)
            {
                audio_source.Stop();
                audio_source2.Stop();
            }        
        }
    }
    public void Check_correct()
    {
        slider_value = 20 * Mathf.Pow(2, frequency_slider.value);
        if (correct_frequency / slider_value > 0.9 && correct_frequency / slider_value < 1.2)
        {
            text3.SetActive(true);
            score += 500;
            perfect_c++;
        }
        else if(correct_frequency / slider_value > 0.8 && correct_frequency / slider_value < 1.4)
        {
            text4.SetActive(true);
            score += 200;
            excellent_c++;
        }
        else if(correct_frequency / slider_value > 0.67 && correct_frequency / slider_value < 1.66)
        {
            text5.SetActive(true);
            score += 100;
            good_c++;
        }
        else if(correct_frequency / slider_value > 0.5 && correct_frequency / slider_value < 2.0)
        {
            text6.SetActive(true);
            score += 50;
            close_c++;
        }
        else
        {
            text7.SetActive(true);
            incorrect_c++;
        }
        ScoreValue.text = score + "";
        audio_source.Stop();
        audio_source2.Stop();
        play_button.isOn = false;
        play_button2.isOn = false;
        text9.SetActive(false);
        text8.SetActive(true);
        text10.SetActive(true);
        attempt++;
    }
    public void Next()
    {
        if(attempt<10)
        {
            text.SetActive(false);
            text2.SetActive(false);
            ChooseRandomSample();
            if (is_boosted == 1)
            {
                text.SetActive(true);
            }
            else
            {
                text2.SetActive(true);
            }
            text3.SetActive(false);
            text4.SetActive(false);
            text5.SetActive(false);
            text6.SetActive(false);
            text7.SetActive(false);
            text8.SetActive(false);
            text10.SetActive(false);
            text9.SetActive(true);
        }
        else
        {
            ScoreManager.Instance.score = score;
            ScoreManager.Instance.exercise_id = 2;
            ScoreManager.Instance.incorrect_count = incorrect_c;
            ScoreManager.Instance.perfect_count = perfect_c;
            ScoreManager.Instance.excellent_count = excellent_c;
            ScoreManager.Instance.good_count = good_c;
            ScoreManager.Instance.close_count = close_c;
            SceneManager.LoadScene("Score Summary");
        }
    }

    public void Scene_Selection()
    {
        SceneManager.LoadScene("Exercise Selection");
    }
}
