using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;
    PlayerController playerController;

    AudioSource audioSource;

    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject restartPanel;
    public GameObject gameOverPanel;
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && gameManager.isStarted)
            PauseButton();
    }

    public void StartButton()
    {
        gameManager.StartGame();

        mainMenuPanel.gameObject.SetActive(false);
        restartPanel.gameObject.SetActive(false);
    }
    public void SettingsButton()
    {
        mainMenuPanel.gameObject.SetActive(false);
        settingsPanel.gameObject.SetActive(true);
    }
    public void ContinueButton()
    {
        restartPanel.SetActive(false);
        gameManager.gameOver = false;
        playerController.playerAnimator.SetBool("isRun",true);
        playerController.walkParticle.Play();
    }
    public void PauseButton()
    {
        restartPanel.SetActive(true);

        gameManager.gameOver = true;

        playerController.playerAnimator.SetBool("isRun", false);
        playerController.walkParticle.Stop();
    }
    public void BackButton()
    {
        mainMenuPanel.gameObject.SetActive(true);
        settingsPanel.gameObject.SetActive(false);
    }

    public void SoundToggle()
    {
        if (gameManager.soundToggle.isOn)
            audioSource.Play();
        else
            audioSource.Pause();
    }
    public void VolumeSlider()
    {
        audioSource.volume = gameManager.volumeSlider.value;
    }
    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
