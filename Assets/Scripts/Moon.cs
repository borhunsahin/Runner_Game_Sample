using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    GameManager gameManager;

    public float rotateSpeed = -0.01f;
    
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(!gameManager.gameOver && gameManager.isStarted)
            transform.Rotate(0, 0, rotateSpeed);
    }

    public void MoonRotate(float rotateSpeed)
    {
        transform.Rotate(0, 0, rotateSpeed); 
    }
}
