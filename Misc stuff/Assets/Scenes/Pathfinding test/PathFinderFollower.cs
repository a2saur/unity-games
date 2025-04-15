using System.Collections.Generic;
using UnityEngine;

public class PathFinderFollower : MonoBehaviour
{
    public bool moving;
    public Vector3 targetPos;
    public float offset;
    public float moveSpeed;
    public float radius;

    public float waitingTime;
    private Vector3 prevPos;

    private List<LineRenderer> lines = new();
    public Material lineMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            moving = true;
            waitingTime = 0;
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = transform.position.z;// = new Vector3(targetPos.x
        }

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

            DrawVectors(
                movementDirection,
                new List<Vector3> { toTarget, sideStepForce },
                new List<Vector3> { avoidanceForce }
            );
        }
    }

    private void DrawLine(LineRenderer lr, Vector3 start, Vector3 vec, Color color)
    {
        float length = Mathf.Clamp(vec.magnitude, 0f, 3f);
        Vector3 end = start + vec.normalized * length;

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.startColor = lr.endColor = color;
        lr.enabled = true;
    }


    public void DrawVectors(Vector3 overallVector, List<Vector3> posVectors, List<Vector3> negVectors)
    {
        Vector3 basePos = transform.position + Vector3.down * 0.1f;
        int totalLines = posVectors.Count + negVectors.Count + 1;

        // Ensure we have enough line renderers
        while (lines.Count < totalLines) {
            GameObject lineObj = new GameObject("VectorLine");
            LineRenderer lr = lineObj.AddComponent<LineRenderer>();
            lr.material = lineMaterial;
            lr.widthMultiplier = 0.05f;
            lr.positionCount = 2;
            lr.useWorldSpace = true;
            lr.numCapVertices = 2;
            lines.Add(lr);
        }

        int index = 0;

        // Green: toward vectors
        foreach (var vec in posVectors) {
            DrawLine(lines[index++], basePos, vec, Color.green);
        }

        // Red: avoidance vectors
        foreach (var vec in negVectors){
            DrawLine(lines[index++], basePos, vec, Color.red);
        }

        // Blue: final movement
        DrawLine(lines[index++], basePos, overallVector, Color.blue);

        // Disable any extra lines
        for (int i = index; i < lines.Count; i++)
        {
            lines[i].enabled = false;
        }
    }

}
