using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float speed = 50.0f;         // forward speed of the plane
    public float maxTiltAngle = 30.0f;  // max tilt angle of the plane
    public float tiltSpeed = 1.0f;      // speed at which the plane tilts
    public float turnSpeed = 2.0f;      // speed at which the plane turns
    public float brakeForce = 10.0f;    // force applied when braking
    public Vector3 moveDirection = Vector3.forward; // the current move direction of the plane

    void Start()
    {
        transform.rotation = Quaternion.Euler(90, 0, -90);
    }


    void Update()
    {
        // get input from the arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical") * -1;

        // apply rotation based on arrow key input
        float tiltAngleX = Mathf.Clamp(verticalInput, -1.0f, 1.0f) * maxTiltAngle;
        float tiltAngleY = Mathf.Clamp(horizontalInput, -1.0f, 1.0f) * maxTiltAngle;
        // transform.rotation = Quaternion.Euler(-90 + tiltAngleX, 0, -90 + tiltAngleY);

        // calculate the new move direction based on arrow key input
        Vector3 targetDirection = Quaternion.Euler(verticalInput * turnSpeed, horizontalInput * turnSpeed, 0) * moveDirection;
        moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, tiltSpeed * Time.deltaTime, 0);
        // transform.rotation = Quaternion.Euler(-90 + tiltAngleX, 0, -90 + tiltAngleY);
        // transform.Rotate(0.0f, verticalInput * turnSpeed, 0.0f, Space.Self);
        // transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + (verticalInput * tiltSpeed * 10), 0, -90 + tiltAngleY);
        // set the rotation of the plane based on the moveDirection vector
        transform.rotation = Quaternion.LookRotation(moveDirection);



        // // calculate the forward direction based on the rotation of the object
        // Vector3 forward = transform.rotation * Vector3.forward;

        // // move the object in the forward direction at the desired speed
        // transform.position += forward * speed * Time.deltaTime;
        // move the object in the move direction at the desired speed
        transform.position += moveDirection * speed * Time.deltaTime;

        // // apply braking if spacebar is pressed
        // if (Input.GetKey(KeyCode.Space))
        // {
        //     transform.position -= forward * brakeForce * Time.deltaTime;
        // }
        // apply braking if spacebar is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position -= moveDirection * brakeForce * Time.deltaTime;
        }
    }
}
