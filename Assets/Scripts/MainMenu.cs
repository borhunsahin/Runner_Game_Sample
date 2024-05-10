using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour
{
    AudioSource audioSource;
    AudioListener audioListener;

    [SerializeField] private Panels panels;
    [SerializeField] private Toggles toggles;

    [SerializeField] private Slider volumeSlider;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioListener = GameObject.Find("Main Camera").GetComponent<AudioListener>();
    }    
    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Settings()
    {
        panels.SettingsPanel.SetActive(true);
        panels.MainMenuPanel.SetActive(false);
    }
    public void BackToMainMenu()
    {
        panels.SettingsPanel.SetActive(false);
        panels.MainMenuPanel.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void MusicVolume()
    {
        audioSource.volume = volumeSlider.value;
    }
    public void MusicOnOff()
    {
        if(toggles.musicToggle.isOn)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
    public void Mute()
    {
        if (toggles.muteToggle.isOn)
        {
            audioListener.enabled = true;
        }
        else
        {
            audioListener.enabled = false;
        }
    }
}
[Serializable]
public struct Panels
{
    public GameObject MainMenuPanel;
    public GameObject SettingsPanel;
}
[Serializable]
public struct Toggles
{
    public Toggle musicToggle;
    public Toggle muteToggle;
}
