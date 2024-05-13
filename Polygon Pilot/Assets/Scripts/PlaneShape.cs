using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneShape : MonoBehaviour
{
    public float maxTiltAngle = 30.0f;  // max tilt angle of the plane
    public float turnSpeed = 1f;  // max tilt angle of the plane
    public float moveSpeed = 10.0f;  // max tilt angle of the plane
    public Vector3 moveDirection = Vector3.forward; // the current move direction of the plane
    public Vector3 cameraDirection = Vector3.forward; // the current move direction of the plane
    
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(-90, 90, 180); // plane
    }

    // Update is called once per frame
    void Update()
    {
        // get input from the arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // apply rotation based on arrow key input
        // transform.Rotate(verticalInput * turnSpeed, horizontalInput * turnSpeed, 0.0f, Space.Self); // cube
        transform.Rotate(horizontalInput * turnSpeed * -1, verticalInput * turnSpeed, 0.0f, Space.Self); // plane
        
        moveDirection = Quaternion.AngleAxis(horizontalInput * turnSpeed, Vector3.up) * moveDirection;
        moveDirection = Quaternion.AngleAxis(verticalInput * turnSpeed, Vector3.right) * moveDirection;
        cameraDirection = Quaternion.AngleAxis(horizontalInput * turnSpeed, Vector3.up) * moveDirection;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position -= moveDirection * moveSpeed * Time.deltaTime;
        }
    }
}
