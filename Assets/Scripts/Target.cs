using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private float minSpeed = 13f;
    private float maxSpeed = 17f;
    private float maxTorque = 10f;
    private float xRange = 4f;
    private float ySpawnPos = -2f;
    private GameManager gameManager;
    public int pointValue;
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos, 0);
    }

    private void OnMouseDown()
    {
        gameManager.PlayBallSound();
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("Bad"))
            {
                gameManager.PlayBombSound();
                gameManager.UpdateLives();
                if(gameManager.lives == 0)
                {
                    gameManager.GameOver();
                }
            }

            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
    }

    //destroy object when it touches sensor to be destroyed
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.UpdateLives();
            if (gameManager.lives == 0)
            {
                gameManager.GameOver();
            }
        }
    }
}
