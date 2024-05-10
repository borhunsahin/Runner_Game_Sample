using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    UIManager uiManager;
    PlayerController playerControllerScript;

    AudioSource gameManagerAudioSource;

    [SerializeField]  private GameObject[] platformList;
    
    [SerializeField] public bool isPaused = false;
    [SerializeField] public bool isGameOver = false;
    [SerializeField] public bool isMenu = false;
    [SerializeField] public bool isMagnet = false;
    [SerializeField] public bool isBaseballBat = false;
    [SerializeField] public bool isPistol = false;

    [SerializeField] public bool isSound = true;
    [SerializeField] public bool isMusic = true;
    [Range(0f, 1f)]
    [SerializeField] public float soundVolume = 1f;
    
    [HideInInspector] public int bullet = 0;
    [HideInInspector] public int coin = 0;
    [HideInInspector] public float energy = 100;

    private float bound;
    [SerializeField] public float platformZScale;
    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        gameManagerAudioSource = GetComponent<AudioSource>();
        bound = platformList.Length*platformZScale;
        SetPlatform();
    }
    void Update()
    {
        if(isMagnet)
            StartCoroutine(MagnetTimer(10f));

        SoundPlayer(isSound, soundVolume);
        MusicPlayer(isMusic, soundVolume);

        BulletCounter();
        EnergyCounter();
    }
    public void SoundPlayer(bool status, float value)
    {
        playerControllerScript.playerAudioSource.volume = value;
        if (!status)
        {
            playerControllerScript.playerAudioSource.volume = 0f;
            gameManagerAudioSource.volume = 0f;
        }
        else
        {
            playerControllerScript.playerAudioSource.volume = value;
            if (isMusic)
                gameManagerAudioSource.volume = value;
        }     
    }
    public void MusicPlayer(bool status, float value)
    {
        if (!status)
            gameManagerAudioSource.volume = 0f;
        else
            gameManagerAudioSource.volume = value;
    }
    public void StartGame()
    {
        PauseGame(false);
        uiManager.tabToStartPanel.startPanel.SetActive(false);
    }
    public void PauseGame(bool status)
    {
        isPaused = status;
    }
    public void Menu(bool status)
    {
        isMenu = status;
    }
    public void GameOver()
    {
        PauseGame(true);
        Menu(true);
        isGameOver = true;
        uiManager.gameOverPanel.gameOverPanel.SetActive(true);

        Debug.Log("GameOver");
    }
    private void EnergyCounter()
    {
        if (!isPaused)
        {
            if (energy > 100)
                energy = 100;
            if (energy < 0)
                energy = 0;
            if (energy > 0)
                energy -= 0.01f;
        }
    }
    private void BulletCounter()
    {
        if (bullet > 8)
            bullet = 8;
        if (bullet < 0)
            bullet = 0;

        if (bullet > 0)
            isPistol = true;
        else
            isPistol = false;
    }
    private void GetPlatform(GameObject[] gameObject, float platformScale)
    {
        int randomIndex = Random.Range(0, platformList.Length);
        Instantiate(gameObject[randomIndex], new Vector3(0, 0, platformScale), gameObject[randomIndex].transform.rotation);
    }
    private void SetPlatform()
    {
        for (int i = -platformList.Length; i < platformList.Length; i++)
            if (!(i == 0))
                GetPlatform(platformList, platformZScale * i);
    }
    public void UpdatePlatform(GameObject gameObject)
    {
        float distanceToPlayer = gameObject.transform.position.z - playerControllerScript.GetPlayerPos().z;
        if (distanceToPlayer < -bound)
        {
            GetPlatform(platformList, playerControllerScript.GetPlayerPos().z + bound);
            Destroy(gameObject);
        }
    }
    private IEnumerator MagnetTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        isMagnet = false;
    }
}
