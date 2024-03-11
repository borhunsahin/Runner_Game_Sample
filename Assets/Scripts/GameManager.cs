using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    PlayerController playerController;
    UIManager uiManager;

    public GameObject mainCam;
    public GameObject playerCam;

    public Toggle soundToggle;

    public ParticleSystem explositionParticle;

    public Slider volumeSlider;

    public bool gameOver = false;
    public bool isStarted = false;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }
    public void StartGame()
    {
        playerCam.SetActive(true);
        mainCam.SetActive(false);

        playerController.playerAnimator.ResetTrigger("isDead");
        playerController.playerAnimator.SetBool("isRun", true);
        playerController.walkParticle.Play();
        
        isStarted = true;
        gameOver = false;
    } 

    public void GameOver()
    {
        gameOver = true;

        playerController.playerAnimator.SetBool("isRun", false);
        playerController.playerAnimator.SetTrigger("isDead");
        explositionParticle.Play();

        uiManager.gameOverPanel.SetActive(true);
    }
}
