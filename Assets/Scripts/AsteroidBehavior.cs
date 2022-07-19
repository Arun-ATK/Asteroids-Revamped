using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{
    [Header("Target Direction")]
    private readonly float xMaxTargetPoint = 8.0f;
    private readonly float yMaxTargetPoint = 4.0f;
    private Vector2 targetDirection;

    public float moveSpeed;

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
        // TODO: Create smaller asteroids from the current asteroid
        Destroy(gameObject);
    }
}
