using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AsteroidBehavior : MonoBehaviour
{
    [Header("Target Direction")]
    private readonly float xMaxTargetPoint = 8.0f;
    private readonly float yMaxTargetPoint = 4.0f;
    private Vector2 targetDirection;

    public float moveSpeed;

    public GameObject[] Asteroids;
    public int splitsRemaining;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 targetPoint = new(
            Random.Range(-xMaxTargetPoint, xMaxTargetPoint),
            Random.Range(-yMaxTargetPoint, yMaxTargetPoint));
        targetDirection = (targetPoint - (Vector2)transform.position).normalized;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * targetDirection);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AsteroidBound")) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) {
            collision.gameObject.GetComponent<BulletController>().HandleAsteroidCollision();
            HandleBulletCollsion();
        }
    }

    private void HandleBulletCollsion()
    {
        if (splitsRemaining > 0) {
            CreateSmallerAsteroids();
        }
        Destroy(gameObject);
    }

    private void CreateSmallerAsteroids()
    {
        int numOfAsteroids = Random.Range(2, 4);
        for (int i = 0; i < numOfAsteroids; ++i) {
            Vector2 spawnPos = (Vector2)transform.position + 
                new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));

            Instantiate(Asteroids[splitsRemaining - 1], 
                spawnPos, 
                Quaternion.identity);
            print("Came here");
        }
    }
}
