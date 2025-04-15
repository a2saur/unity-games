using System.Collections.Generic;
using UnityEngine;

public class FollowingMover : MonoBehaviour
{
    public bool moving;
    public Vector3 targetPos;
    public float offset = 1f;
    public float moveSpeed = 2.5f;
    public float radius = 2.5f;

    public float waitingTime;
    private Vector3 prevPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving){
            waitingTime += Time.deltaTime;
            if (waitingTime > 0.5f){
                if (Vector3.Distance(prevPos, transform.position) < moveSpeed/10){
                    // hasn't moved
                    moving = false;
                } else {
                    waitingTime = 0;
                    prevPos = transform.position;
                }
            }

            Vector3 toTarget = (targetPos - transform.position).normalized;
            Vector3 sideStepForce = (Vector3) Vector2.Perpendicular((Vector2) toTarget).normalized * 0.5f;

            Vector3 center = transform.position;
            
            Vector3 avoidanceForce = Vector2.zero;
            Collider2D[] obstacles = Physics2D.OverlapCircleAll(center, radius);//Physics2D.OverlapCircleAll(transform.position, avoidanceRadius, obstacleLayer);
            foreach (Collider2D col in obstacles)
            {
                Vector3 dirAway = (transform.position - (Vector3) col.ClosestPoint(transform.position));
                float distance = dirAway.magnitude;
                if (distance > 0)
                {
                    avoidanceForce += dirAway.normalized / distance; // stronger repulsion the closer you are
                }
            }

            Vector3 movementDirection = (toTarget + sideStepForce + avoidanceForce).normalized;

            if (Mathf.Abs(movementDirection.magnitude) < 0.05f){
                Vector3 side = Vector2.Perpendicular(movementDirection);
                movementDirection = (movementDirection + side * 0.5f).normalized;
            } else {
                transform.position += (movementDirection * moveSpeed * Time.deltaTime);
            }

            transform.position += (movementDirection * moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < offset) {
                moving = false;
            }
        }
    }

    public void SetTargetPos(Vector3 target){
        moving = true;
        waitingTime = 0;
        targetPos = target;
        targetPos.z = transform.position.z;
    }

    public void UpdateTargetPos(Vector3 target){
        if (Vector3.Distance(transform.position, targetPos) > offset) {
            moving = true;
            waitingTime = 0;
        }
        targetPos = target;
        targetPos.z = transform.position.z;
    }

    public void SetMoveSpeed(float newSpeed){
        moveSpeed = newSpeed;
    }

    public void SetOffset(float newOffset){
        offset = newOffset;
    }
}
