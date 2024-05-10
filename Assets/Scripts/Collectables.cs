using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] private float rotateSpeed;
    private float rotY;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        //gameManager.ObjectNextPositionUpdater(gameObject);
        rotY += rotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotY, transform.rotation.eulerAngles.z);
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
        if (!gameManager.isGameOver)
            Invoke("ReActiveCollectables", 10f);
    }
    private void ReActiveCollectables() => gameObject.SetActive(true);
}
