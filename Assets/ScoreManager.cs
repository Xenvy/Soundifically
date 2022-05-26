using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score;
    public float difficulty;
    public int exercise_id;
    public int perfect_count;
    public int excellent_count;
    public int good_count;
    public int close_count;
    public int incorrect_count;
    public int note_mode;
    public int interval_mode;
    public int chord_mode;
    public int advanced_mode;
    public int key_mode;
    public int player_id;
    public float usage_time;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        usage_time = PlayerPrefs.GetFloat("Usage time", 0);
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        usage_time += Time.deltaTime;
    }
}
