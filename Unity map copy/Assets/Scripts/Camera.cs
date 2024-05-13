using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;

    public Vector2 turn;
    public float sensitivity = .5f;
    public Vector3 deltaMove;
    public float speed = 1;

    public float s = 10f;

    private Vector3 offset;

    void Start () 
    {
        // pass
    }

    void LateUpdate()
    {
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;
        transform.localRotation = Quaternion.Euler(30-turn.y, turn.x, 0);

        // Offset calculation

        float zAngle = 30-turn.y;
        offset.y = s*Mathf.Sin(zAngle * 0.01745329251f);

        float xyOffset = s * Mathf.Cos(zAngle * 0.01745329251f);

        float xAngle = turn.x;
        offset.x = -1 * xyOffset * Mathf.Sin(xAngle * 0.01745329251f);
        offset.z = -1 * xyOffset * Mathf.Cos(xAngle * 0.01745329251f);

        transform.position = player.transform.position + offset;
    }
}
