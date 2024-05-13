using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public Transform mainChar;

    public float smoothTime = 0.3f;
    public int xOffset = 0;
    public int yOffset = 0;
    private Vector3 velocity = Vector3.zero;

    void Start () {
        // transform.position = mainChar.transform.position;
    }

    void Update () {
        // Move camera
        if (Input.GetKey(KeyCode.A)) {
            xOffset = -50;
        } else if (Input.GetKey(KeyCode.D)) {
            xOffset = 50;
        } else {
            xOffset = 0;
        }
        if (Input.GetKey(KeyCode.S)) {
            yOffset = -30;
        } else if (Input.GetKey(KeyCode.W)) {
            yOffset = 30;
        } else {
            yOffset = 0;
        }

        // Smoothly move the camera towards the mainChar position
        // Vector3 targetPosition = mainChar.TransformPoint(new Vector3(xOffset, yOffset, -10));
        // transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.position = new Vector3(mainChar.transform.position.x+xOffset, mainChar.transform.position.y+yOffset, -10);
    }
}