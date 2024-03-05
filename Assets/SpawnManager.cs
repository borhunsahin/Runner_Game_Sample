using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    
    [SerializeField] GameObject[] spawnPrefab;
    [SerializeField] Vector3 spawnPosition = new Vector3(-10,200,0);
    private PlayerController playerControllerScript;
    void Start()
    {
        
        
        InvokeRepeating("SpawnObstacle", 2, 6);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

    }


    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        if(playerControllerScript.gameOver == false && playerControllerScript.isStarted)
        {
            Instantiate(spawnPrefab[0], spawnPosition, spawnPrefab[0].transform.rotation);
        }
        
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
