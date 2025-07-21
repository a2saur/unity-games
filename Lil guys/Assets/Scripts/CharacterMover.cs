using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public bool targeting = false; // do you have a target? i.e. should you be moving
    public Vector3 targetSpot;
    public float moveSpeed;
    public Vector3 velocity = new Vector3(0, 0, 0);

    private float checkRadius = 2.5f; // how far around to check

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SettingsManager.playing){
            if (targeting){
                // move towards target
                Vector3 towardsTargetMovement;
                if (!CloseToTarget()){
                    towardsTargetMovement = (targetSpot-transform.position).normalized;
                } else {
                    towardsTargetMovement = new Vector3(0, 0, 0);
                }
                velocity = towardsTargetMovement * moveSpeed * Time.deltaTime;

                // check for collisions
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, checkRadius);
                Vector3 avoidanceVectors = new Vector3(0, 0, 0);
                Vector3 hitPos;
                float dist;
                foreach (Collider2D hit in hits)
                {
                    hitPos = hit.gameObject.transform.position;
                    if (GoingToCollide(hit.gameObject)){
                        // add to the vector calculations
                        dist = Vector3.Distance(hitPos, transform.position);
                        if ((1/dist) < 5){
                            if (dist > 0.2f){
                                avoidanceVectors -= (hitPos - transform.position).normalized * (1/Vector3.Distance(hitPos, transform.position));
                            } else {
                                avoidanceVectors -= (hitPos - transform.position).normalized * 5;
                            }
                        }
                    }
                }

                // update position
                transform.position = transform.position + (towardsTargetMovement * moveSpeed * Time.deltaTime) + (avoidanceVectors * moveSpeed * Time.deltaTime);
                velocity = (towardsTargetMovement * moveSpeed * Time.deltaTime) + (avoidanceVectors * Time.deltaTime);
            } else {
                velocity = new Vector3(0, 0, 0);
            }
        } else {
                velocity = new Vector3(0, 0, 0);
        }
    }

    public void SetTarget(Vector3 pos){
        // Sets the target spot to move to
        targeting = true;
        targetSpot = pos;
    }

    public void StopTargeting(){
        // stops moving towards target
        targeting = false;
    }

    public bool CloseToTarget(){
        // is it as close to the target as possible?
        if (Vector3.Distance(targetSpot, transform.position) < 0.75f){
            return true;
        } else {
            return false;
        }
    }

    public bool GoingToCollide(GameObject obj){
        CharacterMover objInfo = obj.GetComponent<CharacterMover>();
        MainChar mcInfo;
        if (objInfo != null)
        {
            // yay it has info
            return GoingToCollidePoints(transform.position, obj.transform.position, velocity, objInfo.velocity);
        } else {
            mcInfo = obj.GetComponent<MainChar>();
            if (mcInfo != null){
                // yay it has info
                return GoingToCollidePoints(transform.position, obj.transform.position, velocity, mcInfo.velocity);
            }
        }

        if (objInfo == null && mcInfo == null){
            // unmoving object
            return GoingToCollidePoints(transform.position, obj.transform.position, velocity, new Vector3(0, 0, 0));
        }

        return false;
    }

    private bool GoingToCollidePoints(Vector3 posA, Vector3 posB, Vector3 velA, Vector3 velB) {
        float collisionThreshold = 1.5f;
        Vector3 relPos = posB - posA;
        Vector3 relVel = velB - velA;

        // Ignore Z
        relPos.z = 0;
        relVel.z = 0;

        float relVelSqrMag = relVel.sqrMagnitude;

        if (relVelSqrMag < 0.0001f)
        {
            // Objects are not moving relative to each other
            return relPos.magnitude < collisionThreshold;
        }

        float t = -Vector3.Dot(relPos, relVel) / relVelSqrMag;

        if (t < 0)
        {
            // Closest point is in the past
            return false;
        }

        Vector3 futurePosA = posA + velA * t;
        Vector3 futurePosB = posB + velB * t;

        float futureDist = Vector3.Distance(futurePosA, futurePosB);

        return futureDist < collisionThreshold;
    }
}
