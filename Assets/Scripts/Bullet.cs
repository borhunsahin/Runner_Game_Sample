using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{    
    [SerializeField] private float moveSpeed;
    void Start()
    {
        
    }
    void Update()
    {
        BulletMove();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Pistol") && !other.CompareTag("BaseballBat") && !other.CompareTag("Coin") && !other.CompareTag("EnergyBoost") && !other.CompareTag("Magnet"))
            Destroy(gameObject);
    }
    private void BulletMove() => transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
}
