using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private bool directionSet = false;
    private Vector2 targetDirection;

    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        transform.up = Vector2.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (directionSet) {
            transform.Translate(bulletSpeed * Time.deltaTime * targetDirection);
        }
    }

    public void SetDirection(Vector2 targetDirection)
    {
        this.targetDirection = targetDirection.normalized;
        directionSet = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AsteroidBound")) {
            Destroy(gameObject);
        }
    }

    public void HandleAsteroidCollision()
    {
        // TODO: Different types of bullets should react differently after collision
        Destroy(gameObject);
    }
}
