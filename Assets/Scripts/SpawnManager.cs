
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] GameObject[] spawnPrefab;
    [SerializeField] Vector3 spawnPosition = new Vector3(-10,200,0);

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        InvokeRepeating("SpawnObstacle", 2, 6);
    }
    void SpawnObstacle()
    {
        if (!gameManager.gameOver && gameManager.isStarted)
            Instantiate(spawnPrefab[0], spawnPosition, spawnPrefab[0].transform.rotation);
    }
}
