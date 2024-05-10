using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backround : MonoBehaviour
{
    PlayerController playerController;
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0,0,playerController.GetPlayerPos().z+50);
    }
}
