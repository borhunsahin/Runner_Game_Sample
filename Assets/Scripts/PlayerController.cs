using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    GameManager gameManager;

    public Rigidbody playerRb;
    public Animator playerAnimator;

    public ParticleSystem walkParticle;

    public bool isOnGround = true;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        playerAnimator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();

        walkParticle = GetComponentInChildren<ParticleSystem>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameManager.gameOver)
        {
            playerRb.AddForce(Vector3.up * 500, ForceMode.Impulse);
            playerAnimator.SetTrigger("isJump");
        }

        else if(!isOnGround && gameManager.isStarted && gameManager.gameOver)
        {
            isOnGround = false;
            walkParticle.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameManager.GameOver();
            walkParticle.Stop();

        }

        if (collision.gameObject.CompareTag("Ground") && gameManager.isStarted && !gameManager.gameOver)
        {
            isOnGround = true;
            walkParticle.Play();
        }  
    }
    
    
}