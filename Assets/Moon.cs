using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    float rotateSpeed = -0.01f;
    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoonRotate(rotateSpeed);
    }

    public void MoonRotate(float rotateSpeed)
    {
        if (playerControllerScript.gameOver == false && playerControllerScript.isStarted == true)
        {
            transform.Rotate(0, 0, rotateSpeed);
        }
    }
}
