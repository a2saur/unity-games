using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public float maxTiltAngle = 30.0f;  // max tilt angle of the plane
    public float turnSpeed = 1f;  // max tilt angle of the plane
    public float moveSpeed = 10.0f;  // max tilt angle of the plane
    public Vector3 moveDirection = Vector3.forward; // the current move direction of the plane
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // float horizontalInput = Input.GetAxis("Horizontal");
        // float verticalInput = Input.GetAxis("Vertical") * -1;
        
        // // apply rotation based on arrow key input
        // float tiltAngleX = Mathf.Clamp(verticalInput, -1.0f, 1.0f) * maxTiltAngle;
        // float tiltAngleY = Mathf.Clamp(horizontalInput, -1.0f, 1.0f) * maxTiltAngle;
        // transform.rotation = Quaternion.Euler(tiltAngleX, 0, tiltAngleY);

        // get input from the arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical") * -1;

        // apply rotation based on arrow key input
        transform.Rotate(verticalInput * turnSpeed, horizontalInput * turnSpeed, 0.0f, Space.Self); // cube
        // transform.Rotate(0, horizontalInput * moveSpeed, 0.0f, Space.Self);

        // Vector3 targetDirection = Quaternion.Euler(verticalInput * moveSpeed, horizontalInput * moveSpeed, 0) * moveDirection;
        // moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, moveSpeed * Time.deltaTime, 0);
        moveDirection = Quaternion.AngleAxis(horizontalInput * turnSpeed, Vector3.up) * moveDirection;
        moveDirection = Quaternion.AngleAxis(verticalInput * turnSpeed, Vector3.right) * moveDirection;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position -= moveDirection * moveSpeed * Time.deltaTime;
        }
    }
}
