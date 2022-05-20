using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelDiffExercise : MonoBehaviour
{
    private int sample;
    private float level_diff;
    public List<AudioClip> samples;
    private AudioSource audio_source;
    private AudioSource audio_source2;
    public Slider level_diff_slider;
    public TextMeshProUGUI current_diff;
    public TextMeshProUGUI correct_diff;
    public TextMeshProUGUI score_value;
    private int score;
    private double slider_value;
    private int attempt = 0;
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
    DirectoryInfo cfd_info;
    public AudioMixer main_mixer;

    void Start()
    {
        audio_source = FindObjectOfType<AudioSource>();
        audio_source2 = FindObjectsOfType<AudioSource>()[1];
        string custom_file_directory = Application.persistentDataPath + "//Level Difference Exercise";
        StartCoroutine(LoadCustomAudio(custom_file_directory));
        ChooseRandomSample();
        text = GameObject.Find("Title");
        text2 = GameObject.Find("Perfect");
        text3 = GameObject.Find("Excellent");
        text4 = GameObject.Find("Good");
        text5 = GameObject.Find("Close");
        text6 = GameObject.Find("TryAgain");
        text7 = GameObject.Find("NextButton");
        text8 = GameObject.Find("CheckButton");
        text9 = GameObject.Find("correct_diff");
        text2.SetActive(false);
        text3.SetActive(false);
        text4.SetActive(false);
        text5.SetActive(false);
        text6.SetActive(false);
        text7.SetActive(false);
        text9.SetActive(false);
    }

    private void ChooseRandomSample()
    {
        if(PlayerPrefs.GetInt("Custom Sounds Only", 0) == 1)
        {
            sample = Random.Range(0, samples.Count-28) + 28;
        }
        else
        {
            sample = Random.Range(0, samples.Count);
        }
        
        level_diff = Random.Range(0.1f, 6.0f);
        main_mixer.SetFloat("VolDiff", -level_diff);
        correct_diff.text = level_diff.ToString("N1") + " dB";
    }

    IEnumerator LoadCustomAudio(string cfd)
    {
        cfd_info = new DirectoryInfo(cfd);

        if(cfd_info.Exists)
        {
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
        else
        {
            Directory.CreateDirectory(cfd);
        }

    }

    private void Update()
    {
        current_diff.text = level_diff_slider.value.ToString("N1") + " dB";
    }

    public void PlaySample1()
    {
        audio_source.clip = samples[sample];
        audio_source.Play();
    }
    public void PlaySample2()
    {
        audio_source2.clip = samples[sample];
        audio_source2.Play();
    }
    public void CheckCorrect()
    {
        slider_value = level_diff_slider.value;
        if (slider_value - level_diff < 0.2 && slider_value - level_diff > -0.2)
        {
            score += 500;
            text2.SetActive(true);
            perfect_c++;
        }
        else if(slider_value - level_diff < 0.5 && slider_value - level_diff > -0.5)
        {
            score += 200;
            text3.SetActive(true);
            excellent_c++;
        }
        else if (slider_value - level_diff < 0.8 && slider_value - level_diff > -0.8)
        {
            score += 100;
            text4.SetActive(true);
            good_c++;
        }
        else if (slider_value - level_diff < 1.1 && slider_value - level_diff > -1.1)
        {
            score += 50;
            text5.SetActive(true);
            close_c++;
        }
        else
        {
            text6.SetActive(true);
            incorrect_c++;
        }
        text8.SetActive(false);
        text7.SetActive(true);
        text9.SetActive(true);
        score_value.text = score + "";
        attempt++;
    }

    public void Next()
    {
        if(attempt<12)
        {
            text9.SetActive(false);
            text2.SetActive(false);
            text3.SetActive(false);
            text4.SetActive(false);
            text5.SetActive(false);
            text6.SetActive(false);
            ChooseRandomSample();
            text7.SetActive(false);
            text8.SetActive(true);
        }     
        else
        {
            ScoreManager.Instance.score = score;
            ScoreManager.Instance.exercise_id = 3;
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
