using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class CompressionExcercise : MonoBehaviour
{
    private int sample;
    private int sample_correct;
    public List<AudioClip> samples;
    private AudioSource audio_source;
    private AudioSource audio_source2;
    public TextMeshProUGUI ScoreValue;
    private int score;
    private int attempt = 0;
    DirectoryInfo cfd_info;
    GameObject text;
    GameObject text2;
    GameObject text3;
    GameObject sample_button;
    GameObject sample_button2;

    private void ChooseRandomSample()
    {
        if (PlayerPrefs.GetInt("Custom Sounds Only", 0) == 1 && samples.Count > 28)
        {
            sample = Random.Range(0, samples.Count - 28) + 28;
        }
        else
        {
            sample = Random.Range(0, samples.Count);
        }

        sample_correct = Random.Range(0, 2);
    }
    void Awake()
    {
        audio_source = FindObjectOfType<AudioSource>();
        audio_source2 = FindObjectsOfType<AudioSource>()[1];
        string custom_file_directory = Application.persistentDataPath + "//Compression Exercise";
        StartCoroutine(LoadCustomAudio(custom_file_directory));
        ChooseRandomSample();
        text = GameObject.Find("Correct");
        text2 = GameObject.Find("Incorrect");
        text3 = GameObject.Find("NextButton");
        sample_button = GameObject.Find("Sample1Button");
        sample_button2 = GameObject.Find("Sample2Button");
        text.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);
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
                    //using (UnityWebRequest get_custom_file = UnityWebRequestMultimedia.GetAudioClip("file://" + fi.FullName, AudioType.MPEG))
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
                    //using (UnityWebRequest get_custom_file = UnityWebRequestMultimedia.GetAudioClip("file://" + fi.FullName, AudioType.WAV))
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

    public void PlaySample1()
    {
        if(sample_correct == 1)
        {
            audio_source.clip = samples[sample];
            audio_source.Play();
        }
        else
        {
            audio_source2.clip = samples[sample];
            audio_source2.Play();
        }
    }
    public void Playsample2()
    {
        if (sample_correct == 1)
        {
            audio_source2.clip = samples[sample];
            audio_source2.Play();
        }
        else
        {
            audio_source.clip = samples[sample];
            audio_source.Play();
        }
    }
    public void ChooseSample1()
    {
        if(sample_correct == 0)
        {
            score += 100;
            text.SetActive(true);
        }
        else
        {
            text2.SetActive(true);
        }
        text3.SetActive(true);
        ScoreValue.text = score + "";
        sample_button.SetActive(false);
        sample_button2.SetActive(false);
        attempt++;
    }

    public void ChooseSample2()
    {
        if (sample_correct == 1)
        {
            score += 100;
            text.SetActive(true);
        }
        else
        {
            text2.SetActive(true);
        }
        text3.SetActive(true);
        ScoreValue.text = score + "";
        sample_button.SetActive(false);
        sample_button2.SetActive(false);
        attempt++;
    }

    public void Next()
    {
        if(attempt<16)
        {
            text.SetActive(false);
            text2.SetActive(false);
            ChooseRandomSample();
            text3.SetActive(false);
            sample_button.SetActive(true);
            sample_button2.SetActive(true);
        }
        else
        {
            ScoreManager.Instance.score = score;
            ScoreManager.Instance.exercise_id = 0;
            ScoreManager.Instance.incorrect_count = 16 - score / 100;
            SceneManager.LoadScene("Score Summary");

            if (PlayerPrefs.GetInt("Compression Highscore", 0) < score)
            {
                PlayerPrefs.SetInt("Compression Highscore", score);
            }
        }
    }

    public void Scene_Selection()
    {
        SceneManager.LoadScene("Exercise Selection");
    }
}
