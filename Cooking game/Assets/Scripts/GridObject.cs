using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    public Vector3 cellSize = new Vector3(1f, 1f, 1f);

    void Update()
    {
        // Get the current position of the object
        Vector3 currentPosition = transform.position;

        // Snap the position to the grid
        currentPosition.x = Mathf.Floor(currentPosition.x / cellSize.x) * cellSize.x;
        currentPosition.y = Mathf.Floor(currentPosition.y / cellSize.y) * cellSize.y;
        currentPosition.z = Mathf.Floor(currentPosition.z / cellSize.z) * cellSize.z;

        // Update the object's position
        transform.position = currentPosition;
    }
}
