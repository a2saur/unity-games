using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseEnd : MonoBehaviour
{
    public float wanderRadius = 5f;
    public float wanderSpeed = 2f;
    public ParticleSystem effect;

    private Vector3 wanderTarget;
    private Vector3 originalSpot;

    // Start is called before the first frame update
    void Start()
    {
        originalSpot = transform.position;
        SetNewWanderTarget();
    }

    void Update()
    {
        Wander();
        if (transform.position.y > 3){
            transform.position = new Vector3(transform.position.x, 3, transform.position.z);
            wanderTarget = originalSpot;
        } if (transform.position.y < -4){
            transform.position = new Vector3(transform.position.x, -4, transform.position.z);
            wanderTarget = originalSpot;
        }

        if (transform.position.y > 7.5f){
            transform.position = new Vector3(transform.position.x, 7.5f, transform.position.z);
            wanderTarget = originalSpot;
        } if (transform.position.y < -7.5f){
            transform.position = new Vector3(transform.position.x, -7.5f, transform.position.z);
            wanderTarget = originalSpot;
        }
    }

    void Wander()
    {
        // Move towards the wander target
        transform.position = Vector3.MoveTowards(transform.position, wanderTarget, wanderSpeed * Time.deltaTime);

        // Check if the enemy reached the wander target
        if (Vector3.Distance(transform.position, wanderTarget) < 0.1f)
        {
            if (Random.Range(1,2) == 1){
                SetNewWanderTarget();
            } else {
                wanderTarget = effect.transform.position;
            }
        }
    }

    void SetNewWanderTarget()
    {
        // Generate a random point within the wander radius
        wanderTarget = Random.insideUnitSphere * wanderRadius;
        wanderTarget.z = 0;
        wanderTarget += transform.position;
    }
}
