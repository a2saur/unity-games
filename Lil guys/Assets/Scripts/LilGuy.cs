using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilGuy : MonoBehaviour
{
    public int mode = 1; // idle, following, path
    public Vector3 followSpot;
    public Vector3 prevSpot;
    public GameObject player;
    private float bufferDist = 2.5f;
    private FollowingMover followController;

    void Start()
    {
        followController = GetComponent<FollowingMover>();
        followController.SetMoveSpeed(SettingsManager.lilMoveSpeed);
        followController.SetOffset(bufferDist);
        player = GameObject.FindWithTag("Player");
        followSpot = player.transform.position;
        prevSpot = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (SettingsManager.playing){
            if (mode == 0){
                // idle, don't do anything
                followController.moving = false;
            } else if (mode == 1){
                // follow
                // move towards the player
                followController.UpdateTargetPos(followSpot);
                
                if (Vector3.Distance(followSpot, player.transform.position) > bufferDist){
                    followSpot = prevSpot;
                    prevSpot = player.transform.position;
                }
                
            } else {
                // path
            }
        }
    }
}
