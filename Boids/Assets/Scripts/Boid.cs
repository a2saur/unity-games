using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public float neighborRadius = 5f; // Radius within which to look for neighbors
    public Vector3 forwardDir;
    public float speed;

    private float X_BOUNDS = 3+7.5f;
    private float Y_BOUNDS = 3+5f;
    private float CORRECTION_STRENGTH = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        forwardDir = GetRandomUnitVector2D();
        // speed += Random.value;
    }

    // Update is called once per frame
    void Update()
    {
        // visual housekeeping
        // face direction
        float targetAngle = Mathf.Atan2(forwardDir.y, forwardDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, targetAngle);
        // move in direction
        transform.position += forwardDir*speed*Time.deltaTime;
        // loop at edges
        if (transform.position.x > X_BOUNDS && forwardDir.x > 0){
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        } if (transform.position.x < -X_BOUNDS && forwardDir.x < 0){
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        }
        if (transform.position.y > Y_BOUNDS && forwardDir.y > 0){
            transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
        } if (transform.position.y < -Y_BOUNDS && forwardDir.y < 0){
            transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
        }

        // BOID STUFF
        List<GameObject> boidObjs = GetSurroundingBoidObjs();
        List<Boid> boids = GetSurroundingBoids();
        MoveTowardsCenter(boidObjs);
        TurnTowardsCenterDirection(boids);
        MatchSpeed(boids);
        AvoidBoids(boidObjs);
    }

    public List<GameObject> GetSurroundingBoidObjs(){
        List<GameObject> surrBoids = new List<GameObject>();

        // find all boids in line of sight
        GameObject[] boidObjs = GameObject.FindGameObjectsWithTag("Boid");
        foreach (GameObject otherBoid in boidObjs){
            if (otherBoid != this){
                float dist = Vector3.Distance(transform.position, otherBoid.transform.position);
                if (dist < neighborRadius){
                    surrBoids.Add(otherBoid);
                }
            }
        }
        return surrBoids;
    }

    public List<Boid> GetSurroundingBoids(){
        List<Boid> surrBoids = new List<Boid>();

        // find all boids in line of sight
        GameObject[] boidObjs = GameObject.FindGameObjectsWithTag("Boid");
        foreach (GameObject otherBoid in boidObjs){
            if (otherBoid != this){
                float dist = Vector3.Distance(transform.position, otherBoid.transform.position);
                if (dist < neighborRadius){
                    surrBoids.Add(otherBoid.GetComponent<Boid>());
                }
            }
        }
        return surrBoids;
    }

    public Vector3 GetRandomUnitVector3D(){
        // Generate random angles for spherical coordinates
        float theta = Random.Range(0f, Mathf.PI * 2); // Random angle in the X-Z plane
        float phi = Mathf.Acos(Random.Range(-1f, 1f)); // Random angle from the Y-axis

        // Convert spherical coordinates to Cartesian coordinates
        float x = Mathf.Sin(phi) * Mathf.Cos(theta);
        float y = Mathf.Sin(phi) * Mathf.Sin(theta);
        float z = Mathf.Cos(phi);

        return new Vector3(x, y, z); // This will be a unit vector
    }

    public Vector3 GetRandomUnitVector2D(){
        float theta = Random.Range(0, Mathf.PI*2);

        float x = Mathf.Cos(theta);
        float y = Mathf.Sin(theta);
        float z = 0;

        return new Vector3(x, y, z);
    }

    public void AvoidBoids(List<GameObject> boidObjs){
        foreach (GameObject otherBoid in boidObjs){
            AvoidObject(otherBoid.transform.position);
        }
    }

    public void AvoidObject(Vector3 obstaclePosition)
    {
        // Calculate the direction towards the obstacle
        Vector3 directionToObstacle = obstaclePosition - transform.position;
        Vector3 unitDirToObstacle = directionToObstacle/directionToObstacle.magnitude;
        // Debug.Log(unitDirToObstacle);

        // if (Vector3.Dot(forwardDir, unitDirToObstacle) < 0.25f){
        // Calculate a new direction that is perpendicular to the direction to the obstacle
        Vector3 avoidanceDirection = Vector3.Cross(directionToObstacle, Vector3.forward);

        // Blend the avoidance direction with the current direction
        // to create a smoother turn away from the obstacle
        forwardDir = Vector3.Lerp(forwardDir, avoidanceDirection.normalized, CORRECTION_STRENGTH).normalized;
        // }
    }

    public void TurnTowardsCenterDirection(List<Boid> boidObjs){
        Vector3 sum = Vector3.zero;
        foreach (Boid otherBoid in boidObjs){
            sum += otherBoid.forwardDir;
        }

        MoveTowardsPos(sum / boidObjs.Count);
    }

    public void MoveTowardsCenter(List<GameObject> boidObjs){
        Vector3 sum = Vector3.zero;
        foreach (GameObject otherBoid in boidObjs){
            sum += otherBoid.transform.position;
        }

        MoveTowardsPos(sum / boidObjs.Count);
    }

    public void MatchSpeed(List<Boid> boidObjs){
        float sum = 0;
        foreach (Boid otherBoid in boidObjs){
            sum += otherBoid.speed;
        }

        float avgSpeed = sum/boidObjs.Count;
        speed = (avgSpeed + speed)/2;
    }

    public void MoveTowardsPos(Vector3 targetPos){
        Vector3 dirToPos = targetPos - transform.position;
        Vector3 unitDirToObstacle = dirToPos/dirToPos.magnitude;
        forwardDir = Vector3.Lerp(forwardDir, unitDirToObstacle, CORRECTION_STRENGTH).normalized;
    }
}
