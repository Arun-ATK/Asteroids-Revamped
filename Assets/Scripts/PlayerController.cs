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

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        horizontalMovement = BoundMovement(gameObject.transform.position.x, xBounds, horizontalMovement);
        verticalMovement   = BoundMovement(gameObject.transform.position.y, yBounds, verticalMovement);

        rb.velocity = new Vector2(horizontalMovement * speed, verticalMovement * speed);

        // Rotation to mouse click

        //if (Input.GetMouseButtonDown(0)) {
        //    Vector2 mousePos = Input.mousePosition;
        //    Vector2 curPos = transform.position;
        //    Vector2 reqDirection = mousePos - curPos;

        //    Vector2 curDirection = transform.rotation.eulerAngles;
        //    Debug.Log("Mouse Position: " + mousePos);
        //    Debug.Log("Current Position: " + curPos);

        //    Debug.Log("Current Direction: " + curDirection);
        //    Debug.Log("Required Direction: " + reqDirection);


        //    float angle = Vector2.SignedAngle(curDirection, reqDirection);
        //    Debug.Log("Angle of Rot: " + angle);

        //    transform.Rotate(0.0f, 0.0f, angle);

        //    //Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);

        //    //float angle = Mathf.Atan2(mousePos.y, pos.x) * Mathf.Rad2Deg;
        //    //transform.rotation = Quaternion.FromToRotation(transform.position, mousePos);
        //}
        if (Input.GetMouseButton(0)) {
            Vector2 reqDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - 
                transform.position).normalized;

            transform.up = reqDirection;

            //transform.up = Vector2.Lerp(transform.up, reqDirection, Time.deltaTime);
        }
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
