
using UnityEngine;

public class Obsticle : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] float speed = 7;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (!gameManager.gameOver && gameManager.isStarted)
            transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
