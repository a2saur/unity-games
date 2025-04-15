using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    // public Vector3 targetPos;
    public Vector3 movementVector;
    public float moveSpeed;
    public float radius;

    private List<LineRenderer> lines = new();
    public Material lineMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementVector = new Vector3(Random.value, Random.value, Random.value).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     moving = true;
        //     waitingTime = 0;
        //     targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     targetPos.z = transform.position.z;// = new Vector3(targetPos.x
        // }
        
        List<Vector3> avoidanceForces = new List<Vector3>();
        Vector3 avoidanceForce = Vector2.zero;
        Collider2D[] obstacles = Physics2D.OverlapCircleAll(transform.position, radius);//Physics2D.OverlapCircleAll(transform.position, avoidanceRadius, obstacleLayer);
        Vector3 dirAway;
        float distance;
        foreach (Collider2D col in obstacles)
        {
            dirAway = (transform.position - (Vector3) col.ClosestPoint(transform.position));
            distance = dirAway.magnitude;
            if (distance > 0)
            {
                avoidanceForces.Add(dirAway.normalized / distance);
                avoidanceForce += dirAway.normalized / distance; // stronger repulsion the closer you are
            }
        }

        // avoid walls
        dirAway = (transform.position - new Vector3(-8, transform.position.y, transform.position.z));
        distance = dirAway.magnitude;
        if (distance > 0)
        {
            avoidanceForces.Add(dirAway.normalized / Mathf.Pow(distance, 5));
            avoidanceForce += dirAway.normalized / Mathf.Pow(distance, 5); // stronger repulsion the closer you are
        }
        
        dirAway = (transform.position - new Vector3(8, transform.position.y, transform.position.z));
        distance = dirAway.magnitude;
        if (distance > 0)
        {
            avoidanceForces.Add(dirAway.normalized / Mathf.Pow(distance, 5));
            avoidanceForce += dirAway.normalized / Mathf.Pow(distance, 5); // stronger repulsion the closer you are
        }

        dirAway = (transform.position - new Vector3(transform.position.x, 6, transform.position.z));
        distance = dirAway.magnitude;
        if (distance > 0)
        {
            avoidanceForces.Add(dirAway.normalized / Mathf.Pow(distance, 5));
            avoidanceForce += dirAway.normalized / Mathf.Pow(distance, 5); // stronger repulsion the closer you are
        }

        dirAway = (transform.position - new Vector3(transform.position.x, -6, transform.position.z));
        distance = dirAway.magnitude;
        if (distance > 0)
        {
            avoidanceForces.Add(dirAway.normalized / Mathf.Pow(distance, 5));
            avoidanceForce += dirAway.normalized / Mathf.Pow(distance, 5); // stronger repulsion the closer you are
        }

        // Vector3 movementDirection = (movementVector*0.5f + avoidanceForce).normalized;
        movementVector = (movementVector + avoidanceForce).normalized;

        transform.position += (movementVector * moveSpeed * Time.deltaTime);

        // movementVector = (movementDirection+movementVector)/2;


        DrawVectors(
            movementVector,
            new List<Vector3> { movementVector*0.5f },
            avoidanceForces
        );
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
