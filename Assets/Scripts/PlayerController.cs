using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("GameArea Bounds")]
    private readonly float xBounds = 8.5f;
    private readonly float yBounds = 4.5f;
    
    [Header("Ship Properties")]
    private float speed = 10.0f;
    private int bulletsPerSecond = 3;
    private float timeSinceLastBullet = 1;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float rotationSpeed = 0.2f;

    private Rigidbody2D rb;
    private SpawnManager spawnManager;

    public GameObject[] bullets;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastBullet += Time.deltaTime;
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        // Pointing at mouse location takes priority over
        // pointing in the direction of movement
        // Ship rotates towards mouse location and fires bullets
        if (Input.GetMouseButton(0)) {
            Vector2 targetDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) -
                transform.position).normalized;

            RotateShip(targetDirection);

            if (timeSinceLastBullet > (1.0f / bulletsPerSecond)) {
                FireBullet(transform.up);
                timeSinceLastBullet = 0;
            }
        }
        // Normal rotation based on movement direction
        else if (horizontalMovement != 0 || verticalMovement != 0) {
            Vector2 targetDirection = new Vector2(horizontalMovement, verticalMovement).normalized;
            RotateShip(targetDirection);
        }

        // BoundMovement used to ensure player stays within bounds
        horizontalMovement = BoundMovement(gameObject.transform.position.x, xBounds, horizontalMovement);
        verticalMovement = BoundMovement(gameObject.transform.position.y, yBounds, verticalMovement);

        rb.velocity = new Vector2(horizontalMovement * speed, verticalMovement * speed);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid")) {
            print("Player callee");
            spawnManager.StopSpawning();
        }
    }

    private bool SameSign(float a, float b)
    {
        return (a >= 0 && b >= 0) || (a < 0 && b < 0);
    }

    // Prevents the player from leaving the play area in a particular axis
    private float BoundMovement(float position, float bound, float movement)
    {
        // Allow player to move away from the bounding line
        // Prevent movement that tries to cross bounding line
        if (Mathf.Abs(position) > bound) {
            return (SameSign(movement, position)) ? 0 : movement;
        }
        else {
            return movement;
        }
    }

    private void RotateShip(Vector2 targetDirection)
    {
        float targetAngle = Vector2.SignedAngle(transform.up, targetDirection);
        float angle = targetAngle * rotationSpeed;
        Quaternion reqRotation = Quaternion.Euler(0, 0, angle);

        transform.up = reqRotation * transform.up;
    }

    private void FireBullet(Vector2 targetDirection)
    {
        GameObject bullet = Instantiate(bullets[0], transform);
        bullet.GetComponent<BulletController>().SetDirection(targetDirection);
    }
}
