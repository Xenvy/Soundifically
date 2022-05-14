using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class CutoffFreqExercise : MonoBehaviour
{
    private int sample;
    public List<AudioClip> samples;
    private AudioSource audio_source;
    private AudioSource audio_source2;
    private double correct_frequency;
    private bool is_lowpass;
    public Slider frequency_slider_low;
    public Slider frequency_slider_high;
    public TextMeshProUGUI current_frequency;
    public TextMeshProUGUI CorrectFrequency;
    public TextMeshProUGUI ScoreValue;
    private int score;
    private double slider_value;
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
    GameObject slider_low;
    GameObject slider_high;
    DirectoryInfo cfd_info;
    public AudioMixer main_mixer;
    public Toggle play_button;
    public Toggle play_button2;

    private void ChooseRandomSample()
    {
        sample = Random.Range(0, samples.Count);

        correct_frequency = 40 * Mathf.Pow(2, Random.Range(0.5f, 8.0f));
        if (Random.Range(0, 2) < 1)
        {
            is_lowpass = true;
            main_mixer.SetFloat("CutoffLow", (float)correct_frequency);
            main_mixer.SetFloat("CutoffHigh", 10.0f);
        }
        else
        {
            is_lowpass = false;
            main_mixer.SetFloat("CutoffLow", 22000.0f);
            main_mixer.SetFloat("CutoffHigh", (float)correct_frequency);
        }

        CorrectFrequency.text = correct_frequency.ToString("N0") + " Hz";
    }
    void Start()
    {
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
        slider_low = GameObject.Find("SliderLow");
        slider_high = GameObject.Find("SliderHigh");
        audio_source = FindObjectOfType<AudioSource>();
        audio_source2 = FindObjectsOfType<AudioSource>()[1];
        string custom_file_directory = Application.persistentDataPath + "//EQ Exercise";
        StartCoroutine(LoadCustomAudio(custom_file_directory));
        ChooseRandomSample();      
        
        if (is_lowpass)
        {
            text2.SetActive(false);
            slider_low.SetActive(true);
            slider_high.SetActive(false);
        }
        else
        {
            text.SetActive(false);
            slider_low.SetActive(false);
            slider_high.SetActive(true);
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
        if(is_lowpass)
        {
            current_frequency.text = (20 * Mathf.Pow(2, frequency_slider_low.value)).ToString("N0") + " Hz";
        }
        else
        {
            current_frequency.text = (20 * Mathf.Pow(2, frequency_slider_high.value)).ToString("N0") + " Hz";
        }
    }

    public void PlaySample1()
    {
        if (play_button.isOn)
        {
            if (!play_button2.isOn)
            {
                main_mixer.SetFloat("CutoffVolume1", 0.0f);
                main_mixer.SetFloat("CutoffVolume2", -80.0f);
                audio_source.clip = samples[sample];
                audio_source.Play();
                audio_source2.clip = samples[sample];
                audio_source2.Play();
            }
            else
            {
                main_mixer.SetFloat("CutoffVolume1", 0.0f);
                main_mixer.SetFloat("CutoffVolume2", -80.0f);
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
            if (!play_button.isOn)
            {
                main_mixer.SetFloat("CutoffVolume1", -80.0f);
                main_mixer.SetFloat("CutoffVolume2", 0.0f);
                audio_source.clip = samples[sample];
                audio_source.Play();
                audio_source2.clip = samples[sample];
                audio_source2.Play();
            }
            else
            {
                main_mixer.SetFloat("CutoffVolume1", -80.0f);
                main_mixer.SetFloat("CutoffVolume2", 0.0f);
                play_button.isOn = false;
            }
        }
        else
        {
            if (!play_button.isOn)
            {
                audio_source.Stop();
                audio_source2.Stop();
            }
        }
    }

    public void Check_correct()
    {
        if(is_lowpass)
        {
            slider_value = 20 * Mathf.Pow(2, frequency_slider_low.value);
            if (correct_frequency / slider_value > 0.95 && correct_frequency / slider_value < 1.05)
            {
                text3.SetActive(true);
                score += 500;
            }
            else if (correct_frequency / slider_value > 0.90 && correct_frequency / slider_value < 1.1)
            {
                text4.SetActive(true);
                score += 200;
            }
            else if (correct_frequency / slider_value > 0.8 && correct_frequency / slider_value < 1.2)
            {
                text5.SetActive(true);
                score += 100;
            }
            else if (correct_frequency / slider_value > 0.67 && correct_frequency / slider_value < 1.33)
            {
                text6.SetActive(true);
                score += 50;
            }
            else
            {
                text7.SetActive(true);
                score = 0;
            }
        }
        else
        {
            slider_value = 20 * Mathf.Pow(2, frequency_slider_high.value);
            if (correct_frequency / slider_value > 0.95 && correct_frequency / slider_value < 1.05)
            {
                text3.SetActive(true);
                score += 500;
            }
            else if (correct_frequency / slider_value > 0.90 && correct_frequency / slider_value < 1.1)
            {
                text4.SetActive(true);
                score += 200;
            }
            else if (correct_frequency / slider_value > 0.8 && correct_frequency / slider_value < 1.2)
            {
                text5.SetActive(true);
                score += 100;
            }
            else if (correct_frequency / slider_value > 0.67 && correct_frequency / slider_value < 1.33)
            {
                text6.SetActive(true);
                score += 50;
            }
            else
            {
                text7.SetActive(true);
                score = 0;
            }
        }

        ScoreValue.text = score + "";
        audio_source.Stop();
        audio_source2.Stop();
        play_button.isOn = false;
        play_button2.isOn = false;
        text9.SetActive(false);
        text8.SetActive(true);
        text10.SetActive(true);
    }
    public void Next()
    {
        text.SetActive(false);
        text2.SetActive(false);
        ChooseRandomSample();
        if (is_lowpass)
        {
            text.SetActive(true);
            slider_low.SetActive(true);
            slider_high.SetActive(false);
        }
        else
        {
            text2.SetActive(true);
            slider_low.SetActive(false);
            slider_high.SetActive(true);
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

    public void Scene_Selection()
    {
        SceneManager.LoadScene("Exercise Selection");
    }
}
