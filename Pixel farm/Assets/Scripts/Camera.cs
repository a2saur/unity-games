using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player; // The player object to follow
    public Vector3 offset; // The camera's offset from the player

    void Update()
    {
        // Move the camera to the player's position with the offset
        transform.position = player.position + offset;
    }
}
