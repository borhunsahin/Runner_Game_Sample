using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;
    PlayerController playerController;

    [SerializeField] private GamePanel gamePanel;
    [SerializeField] private PausePanel pausePanel;
    [SerializeField] private SettingsPanel settingsPanel;
    [SerializeField] public GameOverPanel gameOverPanel;
    [SerializeField] public StartPanel tabToStartPanel;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        gamePanel.coinText.text = gameManager.coin.ToString() + " $";
        gamePanel.distanceText.text = ((int)playerController.GetPlayerPos().z).ToString() + "M";
        gamePanel.energyBar.fillAmount = gameManager.energy / 100;

        if (Input.anyKey && !gameManager.isMenu)
            if(!gameManager.isGameOver)
                gameManager.StartGame();
    }
    /**** GamePanel ****/
    public void PauseMenuButton()
    {
        gameManager.PauseGame(true);
        gameManager.Menu(true);

        pausePanel.pausePanel.SetActive(true);
        gamePanel.pauseMenuButton.SetActive(false);
        gamePanel.ReturnGameButton.SetActive(true);
    }
    public void ReturnGameButton()
    {
        gameManager.Menu(false);

        gamePanel.pauseMenuButton.SetActive(true);
        gamePanel.ReturnGameButton.SetActive(false);
    }
    public void SoundOffButton()
    {
        gameManager.isSound = false;
        settingsPanel.soundToggle.isOn = true;

        gamePanel.soundOffButton.SetActive(false);
        gamePanel.soundOnButton.SetActive(true);

        if(gameManager.isMusic)
            MusicOffButton();
    }
    public void SoundOnButton()
    {
        gameManager.isSound = true;
        settingsPanel.soundToggle.isOn = false;

        gamePanel.soundOffButton.SetActive(true);
        gamePanel.soundOnButton.SetActive(false);

        if (gameManager.isMusic)
            MusicOnButton();
    }
    public void MusicOffButton()
    {
        settingsPanel.musicToggle.isOn = true;

        gamePanel.musicOffButton.SetActive(false);
        gamePanel.musicOnButton.SetActive(true);
    }
    public void MusicOnButton()
    {
        settingsPanel.musicToggle.isOn = false;

        gamePanel.musicOffButton.SetActive(true);
        gamePanel.musicOnButton.SetActive(false);
    }
    public void PauseButton()
    {
        gameManager.PauseGame(true);
        gameManager.Menu(true);

        gamePanel.PauseButton.SetActive(false);
        gamePanel.PlayButton.SetActive(true);
    }
    public void PlayButton()
    {
        gameManager.PauseGame(false);
        gameManager.Menu(false);

        gamePanel.PauseButton.SetActive(true);
        gamePanel.PlayButton.SetActive(false);
    }
    public void GetPistolIcons()
    {
        foreach(var bullet in gamePanel.pistolMagazine)
            bullet.SetActive(true);
        gamePanel.pistolIcon.SetActive(true);
    }
    public void DecreaseBulletIcon()
    {
        gamePanel.pistolMagazine[gameManager.bullet].SetActive(false);
        if(gameManager.bullet == 0)
            gamePanel.pistolIcon.SetActive(false);
    }
    /**** Menu ****/
    public void ResumeButton()
    {
        gameManager.PauseGame(false);
        gameManager.Menu(false);

        pausePanel.pausePanel.SetActive(false);
        gamePanel.pauseMenuButton.SetActive(true);
        gamePanel.ReturnGameButton.SetActive(false);
        gamePanel.PauseButton.SetActive(true);
        gamePanel.PlayButton.SetActive(false);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(1);
    }
    public void SettingsButton()
    {
        if (!gameManager.isGameOver)
        {
            settingsPanel.settingsPanelMenu.SetActive(true);
            pausePanel.pausePanel.SetActive(false);
        }
        if (gameManager.isGameOver)
        {
            gameOverPanel.gameOverPanel.SetActive(false);
            settingsPanel.settingsPanelMenu.SetActive(true);
        }
    }
    public void BackButton()
    {
        if(!gameManager.isGameOver)
        {
            settingsPanel.settingsPanelMenu.SetActive(false);
            pausePanel.pausePanel.SetActive(true);
        }
        if (gameManager.isGameOver)
        {
            settingsPanel.settingsPanelMenu.SetActive(false);
            gameOverPanel.gameOverPanel.SetActive(true);
        }
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Application.Quit();
    }
    /**** Settings Panel ****/
    public void VolumeSlider()
    {
        gameManager.soundVolume = settingsPanel.volumeSlider.value;
    }
    public void SoundMuteToggle()
    {
        if (settingsPanel.soundToggle.isOn)
        {
            gameManager.isSound = false;
            gamePanel.soundOffButton.SetActive(false);
            gamePanel.soundOnButton.SetActive(true);
            MusicOffButton();
        }
        else
        {
            gameManager.isSound = true;
            gamePanel.soundOffButton.SetActive(true);
            gamePanel.soundOnButton.SetActive(false);
            MusicOnButton();
        }
    }
    public void MusicMuteToggle()
    {
        if (settingsPanel.musicToggle.isOn)
        {
            gameManager.isMusic = false;
            gamePanel.musicOffButton.SetActive(false);
            gamePanel.musicOnButton.SetActive(true);
        }
        else
        {
            gameManager.isMusic = true;
            gamePanel.musicOffButton.SetActive(true);
            gamePanel.musicOnButton.SetActive(false);
        }
    }
}
[Serializable] public struct GamePanel
{
    public GameObject gamePanel;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI distanceText;

    public Image energyBar;

    public GameObject pauseMenuButton;
    public GameObject ReturnGameButton;

    public GameObject soundOffButton;
    public GameObject soundOnButton;

    public GameObject musicOffButton;
    public GameObject musicOnButton;

    public GameObject PauseButton;
    public GameObject PlayButton;

    public GameObject[] pistolMagazine;
    public GameObject pistolIcon;
}
[Serializable] public struct PausePanel
{
    public GameObject pausePanel;
}
[Serializable] struct SettingsPanel
{
    public GameObject settingsPanelMenu;

    public Toggle soundToggle;
    public Toggle musicToggle;
    public Slider volumeSlider;
}
[Serializable] public struct GameOverPanel
{
    public GameObject gameOverPanel;

    [SerializeField] private TextMeshProUGUI scoreText;
}
[Serializable] public struct StartPanel
{
    public GameObject startPanel;
}
