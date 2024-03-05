using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Obsticle : MonoBehaviour
{
    [SerializeField] float speed = 7;
    private PlayerController playerControllerScript;
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position.x>10)
        {
            Destroy(gameObject);
        }

        if(playerControllerScript.gameOver == false) 
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        
    }
}
