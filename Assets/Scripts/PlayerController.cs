using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    UIManager uiManager;
    [HideInInspector] public AudioSource playerAudioSource;

    CharacterController characterController;
    Animator playerAnimator;

    Vector3 direction;
    [SerializeField] protected float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float laneChangeDistance;
    private int laneIndex = 0;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip lowJumpSound;
    [SerializeField] private ParticleSystem explotionEffect;
    [SerializeField] private AudioClip explotionSound;

    private float gravity = -9.81f;
    [SerializeField] float gravityMultiplier;
    private float velocity;

    [SerializeField] private Weapons weapons;
    [SerializeField] private Collactables collactables;
    

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        characterController = GetComponent<CharacterController>();
        playerAudioSource = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        playerAnimator.SetBool("isIdle",true);

        if (!gameManager.isPaused)
        {
            ApplyMovement();
            OpenFire();
            StrikeBaseballBat();
        }    
        ApplyGravity();
        if (!gameManager.isPaused)
            ApplyControllers();
    }
    private void ApplyMovement()
    {
        direction = transform.forward * moveSpeed + transform.up * velocity;
        characterController.Move(direction * Time.deltaTime);
    }
    private void ApplyGravity()
    {
        if (characterController.isGrounded)
            velocity = -1f;
        else
            velocity += gravity * gravityMultiplier * Time.deltaTime;
    }
    private void ChangeLane(float laneChangeDistance)
    {
        characterController.enabled = false;
        transform.Translate(Vector3.right * laneChangeDistance);
        characterController.enabled = true;

        if (laneChangeDistance < 0)
            laneIndex++;
        if (laneChangeDistance > 0)
            laneIndex--;
    }
    private void Jump(float jumpForce)
    {
        velocity += jumpForce;
    }
    private void ApplyControllers()
    {
        if (Input.GetKeyDown(KeyCode.A) && laneIndex < 1)
            ChangeLane(-laneChangeDistance);
        if (Input.GetKeyDown(KeyCode.D) && laneIndex > -1)
            ChangeLane(laneChangeDistance);
        if (Input.GetKeyDown(KeyCode.W) && characterController.isGrounded )
        {
            if(gameManager.energy > 0)
            {
                Jump(jumpForce);
                playerAudioSource.PlayOneShot(jumpSound);
                gameManager.energy -= 10;
            }
            else
                Jump(jumpForce/4);
            playerAudioSource.PlayOneShot(lowJumpSound);
        } 
        if (Input.GetKeyDown(KeyCode.S) && !characterController.isGrounded && gameManager.energy > 0)
        {
            Jump(-jumpForce);  
        }
    }
    private void OpenFire()
    {
        if (gameManager.isPistol)
            weapons.pistol.SetActive(true);
        else
            weapons.pistol.SetActive(false);

        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded && Time.time > weapons.nextRate)
            if (gameManager.isPistol)
            {
                weapons.nextRate = Time.time + weapons.fireRate;
                playerAnimator.SetTrigger("isFire");
                Instantiate(weapons.bullet, transform.position + new Vector3(0, 0, 2), weapons.bullet.transform.rotation);
                playerAudioSource.PlayOneShot(weapons.shootSound);
                gameManager.bullet -= 1;
                uiManager.DecreaseBulletIcon();
            }
    }
    private void StrikeBaseballBat()
    {
        if (gameManager.isBaseballBat && !gameManager.isPistol)
            weapons.baseballBat.SetActive(true);
        else
            weapons.baseballBat.SetActive(false);

        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
            if (gameManager.isBaseballBat)
            {
                playerAnimator.SetTrigger("isStrike");
            }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pistol"))
        {
            gameManager.bullet += 8;
            playerAudioSource.PlayOneShot(collactables.takeWeaponSound);
            Instantiate(collactables.takeWeaponEffect, transform.position, collactables.takeWeaponEffect.transform.rotation);
            uiManager.GetPistolIcons();
        }
            
        if (other.CompareTag("BaseballBat"))
        {
            gameManager.isBaseballBat = true;
            playerAudioSource.PlayOneShot(collactables.takeWeaponSound);
            Instantiate(collactables.takeWeaponEffect, transform.position, collactables.takeWeaponEffect.transform.rotation);
        }
            
        if (other.CompareTag("EnergyBoost"))
        {
            gameManager.energy += 200;
            playerAudioSource.PlayOneShot(collactables.takeEnergySound);
            Instantiate(collactables.takeEnergyEffect, transform.position, collactables.takeEnergyEffect.transform.rotation);
        }
            
        if (other.CompareTag("Coin"))
        {
            gameManager.coin += 1;
            playerAudioSource.PlayOneShot(collactables.takeCoinSound);
            Instantiate(collactables.takeCoinEffect, transform.position, collactables.takeCoinEffect.transform.rotation);
        }

        if(other.CompareTag("Magnet"))
            gameManager.isMagnet = true;
            

        if (other.CompareTag("Obstacle") || other.CompareTag("Zombie"))
        {
            gameManager.GameOver();
            explotionEffect.Play();
            playerAudioSource.PlayOneShot(explotionSound);
        }
    }
    public Vector3 GetPlayerPos() => transform.position;
    public bool isOnGround() => characterController.isGrounded;
}

[Serializable]
struct Weapons
{
    public GameObject bullet;
    public GameObject pistol;
    public AudioClip shootSound;
    public GameObject baseballBat;
    public float fireRate;
    public float nextRate;
}
[Serializable]
struct Collactables
{
    public AudioClip takeEnergySound;
    public AudioClip takeCoinSound;
    public AudioClip takeWeaponSound;

    public ParticleSystem takeEnergyEffect;
    public ParticleSystem takeCoinEffect;
    public ParticleSystem takeWeaponEffect;
}

