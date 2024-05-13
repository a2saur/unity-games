using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningScreen : MonoBehaviour
{
    public Vector3 startPos = new Vector3(-5.25f, 10.25f, -1f);
    public Vector3 endPos = new Vector3(5f, 0f, -1f);
    public float frames = 1.0f;

    private float dX;
    private float dY;
    private Vector3 change;

    void Start()
    {
        Debug.Log(startPos.x);
        dX = (startPos.x-endPos.x)/frames;
        dY = (startPos.y-endPos.y)/frames;
        change = new Vector3(dX, dY, 0);
    }

    void Update()
    {
        if (transform.position.x > endPos.x || transform.position.y < endPos.y) {
            transform.position = startPos;
        }
        transform.position = transform.position - change;
    }
}
