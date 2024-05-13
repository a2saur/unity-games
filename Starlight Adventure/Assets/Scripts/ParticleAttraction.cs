using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAttraction : MonoBehaviour {
    public GameObject player;
    public int forceMultiplier;
    public int moveDistance;
    public int absorbDistance;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Calculate the distance between the particle system and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Only move the particles if they are within the specified distance range
        if (distanceToPlayer <= moveDistance)
        {
            // Calculate the direction from the particle system to the player
            Vector3 directionToPlayer = player.transform.position - transform.position;

            // Apply a force in that direction
            rb.AddForce(directionToPlayer.normalized * forceMultiplier);
        }
        
        // If the particles are too close to the player, disable the particle system
        if (distanceToPlayer <= absorbDistance)
        {
            GetComponent<ParticleSystem>().Stop();
        }
    }
}
