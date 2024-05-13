using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goose : MonoBehaviour
{
    public Sprite normal;
    public Sprite angry;
    public float wanderRadius = 5f;
    public float wanderSpeed = 2f;
    public float chargeSpeed = 5f;
    public float chargeDistance = 3f;
    public float chargeLength = 3f;
    public float maxWanderDist = 10f;
    public float delaySet = 1;

    private Vector3 wanderTarget;
    private Vector3 originalSpot;
    private bool isCharging = false;
    private float dX = 0;
    private float dY = 0;
    private float dT = 0;
    private float chargeTime = 0;
    private float delay = 0;
    private Transform player;

    public SetManager SETMANAGER;
    // Start is called before the first frame update
    void Start()
    {
        SETMANAGER = GameObject.FindWithTag("SetManager").GetComponent<SetManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalSpot = transform.position;
        SetNewWanderTarget();
    }

    void Update()
    {
        if (!SETMANAGER.isPaused){
            if (Mathf.Abs(originalSpot.x-transform.position.x) > maxWanderDist){
                wanderTarget = originalSpot;
                GetComponent<SpriteRenderer>().sprite = normal;
                Wander();
            } else {
                if (isCharging) {
                    GetComponent<SpriteRenderer>().sprite = angry;
                    if (delay < 1) {
                        // Debug.Log("Charging");
                        Charge();
                    } else {
                        // Debug.Log("Waiting");
                        delay -= Time.deltaTime;
                    }
                }
                else {
                    GetComponent<SpriteRenderer>().sprite = normal;
                    Wander();
                }

                if (transform.position.y > 4){
                    transform.position = new Vector3(transform.position.x, 4, transform.position.z);
                } if (transform.position.y < -4){
                    transform.position = new Vector3(transform.position.x, -4, transform.position.z);
                }
            }
        }
    }

    void Wander()
    {
        // Move towards the wander target
        transform.position = Vector3.MoveTowards(transform.position, wanderTarget, wanderSpeed * Time.deltaTime);

        // Check if the enemy reached the wander target
        if (Vector3.Distance(transform.position, wanderTarget) < 0.1f)
        {
            SetNewWanderTarget();
        }

        // Check if the player is within charge distance
        if (Vector3.Distance(transform.position, player.position) < chargeDistance)
        {
            // Debug.Log("Distance");
            isCharging = true;
            delay = delaySet;
            dX = ((player.position.x-transform.position.x)/(chargeLength))*Time.deltaTime;
            dY = ((player.position.y-transform.position.y)/(chargeLength))*Time.deltaTime;
            dT = Time.deltaTime;
            chargeTime = chargeLength;
        }
    }

    void Charge()
    {
        // Move towards the player
        // transform.position = Vector3.MoveTowards(transform.position, player.position, chargeSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x+dX, transform.position.y+dY, transform.position.z);
        chargeTime -= dT;

        // Check if the enemy is close to the player
        if (Vector3.Distance(transform.position, player.position) < 0.1f)
        {
            // Handle player caught logic or apply damage, etc.
            // Debug.Log("Player caught!");
            isCharging = false;
            wanderTarget = originalSpot; // After charging, resume wandering
        } if (chargeTime < 0) {
            isCharging = false;
            wanderTarget = originalSpot; // After charging, resume wandering
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
