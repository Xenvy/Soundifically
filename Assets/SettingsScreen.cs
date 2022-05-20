using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
    public Slider volume_slider;
    public Toggle telemetry_enabled;
    public Toggle custom_sounds;
    public AudioMixer main_mixer;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Main Volume"))
        {
            volume_slider.value = PlayerPrefs.GetFloat("Main Volume");
        }
        else
        {
            volume_slider.value = -0.3f;
        }

        if (PlayerPrefs.HasKey("TelemetryOn"))
        {
            if(PlayerPrefs.GetInt("TelemetryOn") == 1)
            {
                telemetry_enabled.isOn = true;
            }
            else
            {
                telemetry_enabled.isOn = false;
            }
        }
        else
        {
            telemetry_enabled.isOn = true;
            PlayerPrefs.SetInt("TelemetryOn", 1);
        }

        if (PlayerPrefs.HasKey("Custom Sounds Only"))
        {
            if (PlayerPrefs.GetInt("Custom Sounds Only") == 1)
            {
                custom_sounds.isOn = true;
            }
            else
            {
                custom_sounds.isOn = false;
            }
        }
        else
        {
            custom_sounds.isOn = false;
            PlayerPrefs.SetInt("Custom Sounds Only", 0);
        }
    }

    public void Set_Volume()
    {     
        main_mixer.SetFloat("MainVolume", volume_slider.value);
        PlayerPrefs.SetFloat("Main Volume", volume_slider.value);
    }

    public void Enable_Telemetry()
    {
        if (telemetry_enabled.isOn)
        {
            PlayerPrefs.SetInt("TelemetryOn", 1);
        }
        else
        {
            PlayerPrefs.SetInt("TelemetryOn", 0);
        }
    }

    public void Custom_Sounds_Only()
    {
        if (custom_sounds.isOn)
        {
            PlayerPrefs.SetInt("Custom Sounds Only", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Custom Sounds Only", 0);
        }
    }

    public void Back_To_Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
