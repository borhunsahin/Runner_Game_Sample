using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    Animator playerAnimator;
    Rigidbody playerRb;

    public GameObject mainCam;
    public GameObject playerCam;
    
    public AudioClip explositionClip;

    public ParticleSystem walkParticle;
    public ParticleSystem explositionParticle;

    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button startButton;


    public bool gameOver = false;
    private bool isOnGround = true;
    public bool isStarted = false;

    


    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        walkParticle = GetComponentInChildren<ParticleSystem>();
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && isOnGround && gameOver == false)
        {
            playerRb.AddForce(Vector3.up * 6, ForceMode.Impulse);
            isOnGround = false;
            playerAnimator.SetTrigger("isJump");
        }

        if(!isOnGround && isStarted)
        {
            walkParticle.Stop();
        }

        


    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }

        if (collision.gameObject.CompareTag("Ground") && isStarted)
        {
            walkParticle.Play();
        }

        
    }

    public void StartGame() 
    {
        playerAnimator.SetBool("isRun", true);

        walkParticle.Play();

        playerCam.SetActive(true);
        mainCam.SetActive(false);

        startButton.gameObject.SetActive(false);

        isStarted = true;

        

        

    }
    void GameOver()
    {
        gameOver = true;

        playerAnimator.SetBool("isRun", false);     
        playerAnimator.SetTrigger("isDead");

        walkParticle.Stop();
        explositionParticle.Play();

        GetComponent<AudioSource>().PlayOneShot(explositionClip, 1.0f);

        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
}