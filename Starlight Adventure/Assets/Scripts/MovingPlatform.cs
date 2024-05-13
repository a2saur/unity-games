using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float horizontalDistance = 10.0f;
    public float verticalDistance = 0.0f;
    public float speed = 2.0f;
    public float pauseTime = 1.0f;
    public bool reverse = false;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 currentTarget;
    private bool isPaused = false;
    private float pauseTimer = 0.0f;

    void Start()
    {
        startPosition = transform.position;
        endPosition = transform.position + new Vector3(horizontalDistance, verticalDistance, 0);
        currentTarget = endPosition;
    }

    void FixedUpdate()
    {
        if (!isPaused)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

            if (transform.position == currentTarget)
            {
                isPaused = true;
                pauseTimer = pauseTime;
            }
        }
        else
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0.0f)
            {
                isPaused = false;
                if (reverse)
                {
                    currentTarget = (currentTarget == startPosition) ? endPosition : startPosition;
                }
                else
                {
                    currentTarget = (currentTarget == startPosition) ? endPosition : startPosition;
                }
            }
        }
    }
}
