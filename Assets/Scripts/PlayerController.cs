using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float xBounds;

    [SerializeField]
    private float yBounds;

    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float lerpSpeed = 0.2f;

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
                reqDirection = new Vector2(horizontalMovement, verticalMovement);
            }

            if (((Vector2)transform.up - reqDirection).magnitude < 0.001) {
                transform.up = reqDirection;
            }
            transform.up = Vector2.Lerp(transform.up, reqDirection, lerpSpeed);
            //transform.up = reqDirection;
            
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) {
                Debug.Log("TUP: " + transform.up);
                Debug.Log("RED: " + reqDirection);
            }
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
