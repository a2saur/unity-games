// This type of attack points at the player initially and then speeds off in that direction

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossPointAtAttack : MonoBehaviour
{
    public GameObject mc; // doesn't need to be defined
    private Vector3 initialDir; // unit vector in a direction
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        mc = GameObject.FindWithTag("Player");

        Vector3 tempDir = new Vector3(
            mc.transform.position.x-transform.position.x,
            mc.transform.position.y-transform.position.y,
            mc.transform.position.z-transform.position.z
        );

        initialDir = tempDir/tempDir.magnitude;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x+(initialDir.x*speed*Time.deltaTime), 
                                        transform.position.y+(initialDir.y*speed*Time.deltaTime),
                                        transform.position.z+(initialDir.z*speed*Time.deltaTime));
    }
}
