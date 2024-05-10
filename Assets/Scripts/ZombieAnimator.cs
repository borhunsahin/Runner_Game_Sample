using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ZombieAnimator : MonoBehaviour
{
    Animator zombieAnimator;
    PlayerController playerControllerScript1;
    void Start()
    {
        zombieAnimator = GetComponent<Animator>();
        playerControllerScript1 = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if(Mathf.Abs(DistanceToPlayer1()) < 15)
        {
            zombieAnimator.SetBool("isWalk", true);
            zombieAnimator.SetBool("isIdle", false);
        }
        else
        {
            zombieAnimator.SetBool("isWalk", false);
            zombieAnimator.SetBool("isIdle", true);
        }

        
    }

    private float DistanceToPlayer1() => transform.position.z - playerControllerScript1.GetPlayerPos().z;
}
