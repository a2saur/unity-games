using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilGuyPrevVer : MonoBehaviour
{
    public int mode = 1; // idle, following, path
    public List<Vector3> followingSpots;
    public GameObject target;
    public float bufferDist;

    void Start()
    {
        followingSpots = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SettingsManager.playing){
            if (mode == 0){
                // idle, don't do anything
            } else if (mode == 1){
                // follow
                // move towards the player
                // followController.SetTargetPos(player.transform.position);

                // if there's a spot in the following spots list, move towards that
                // if (followingSpots.Count > 0){
                //     // if the target is more than dist away from the last following spot, add the current position to the list
                //     if (Vector3.Distance(target.transform.position, followingSpots[followingSpots.Count-1]) > bufferDist){
                //         followingSpots.Add(target.transform.position);
                //     }

                //     Vector3 dist = followingSpots[0] - transform.position;
                //     transform.position += Vector3.Normalize(dist) * SettingsManager.lilMoveSpeed * Time.deltaTime;
                //     // if close to the following spot, remove that one from the list
                //     if (Vector3.Distance(transform.position, followingSpots[0]) < 0.01){
                //         followingSpots.RemoveAt(0);
                //     }
                // } else {
                //     // if the target is more than dist away from current, add the current position to the list
                //     if (Vector3.Distance(target.transform.position, transform.position) > bufferDist){
                //         followingSpots.Add(Vector3.Lerp(target.transform.position, transform.position, 0.5f));
                //     }
                // }
                
            } else {
                // path
            }
        }
    }
}
