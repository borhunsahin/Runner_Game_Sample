using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    PlayerController playerControllerScript;
    GameManager gameManager;

    CharacterController characterController;
    
    private Vector3 startPos;
    private Quaternion startRot;

    private float playerStartYpos;

    [SerializeField] private float moveSpeed;
    [SerializeField] ParticleSystem bloodEffect;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        characterController = GetComponent<CharacterController>();

        startPos = transform.position;
        startRot = transform.rotation;

        playerStartYpos = playerControllerScript.gameObject.transform.position.y; 
    }
    void Update()
    {
        if(!gameManager.isPaused)
            ZombieMovement();
    }
    private void ZombieMovement()
    {
        Vector3 target = new Vector3(playerControllerScript.gameObject.transform.position.x, playerStartYpos, playerControllerScript.gameObject.transform.position.z);

        if (Mathf.Abs(DistanceToPlayer()) < 15)
        {
            transform.LookAt(target);
            Vector3 moveDirection = target - transform.position;
            characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
        else if(Mathf.Abs(DistanceToPlayer()) > 20)
        {
            transform.position = startPos;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet") || other.CompareTag("BaseballBat"))
        {
            gameObject.SetActive(false);
            Instantiate(bloodEffect, transform.position, bloodEffect.transform.rotation);
        }
        if(!gameManager.isGameOver)
            Invoke("ReActiveZombie", 20f);
    }
    private float DistanceToPlayer() => transform.position.z - playerControllerScript.GetPlayerPos().z;
    private void ReActiveZombie() => gameObject.SetActive(true);
}
