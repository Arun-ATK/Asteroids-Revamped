using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float xBounds = 8.5f;
    private float yBounds = 4.5f;
    private float speed = 10.0f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float rotationLerpSpeed = 0.2f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        // Rotation
        if (Input.GetMouseButton(0) || horizontalMovement != 0 || verticalMovement != 0) {
            Vector2 reqDirection;

            if (Input.GetMouseButton(0)) {
                reqDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) -
                    transform.position).normalized;
            }
            else {
                reqDirection = new Vector2(horizontalMovement, verticalMovement).normalized;
            }

            float targetAngle = Vector2.SignedAngle(transform.up, reqDirection);
            float angle = targetAngle * rotationLerpSpeed;
            Quaternion reqRotation = Quaternion.Euler(0, 0, angle);

            transform.up = reqRotation * transform.up;
        }

        // Movement
        horizontalMovement = BoundMovement(gameObject.transform.position.x, xBounds, horizontalMovement);
        verticalMovement = BoundMovement(gameObject.transform.position.y, yBounds, verticalMovement);

        rb.velocity = new Vector2(horizontalMovement * speed, verticalMovement * speed);
    }

    private bool SameSign(float a, float b)
    {
        return (a >= 0 && b >= 0) || (a < 0 && b < 0);
    }

    private float BoundMovement(float position, float bound, float movement)
    {
        if (Mathf.Abs(position) > bound) {
            return (SameSign(movement, position)) ? 0 : movement;
        }
        else {
            return movement;
        }
    }
}
