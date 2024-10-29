// This attack follows the character a little bit

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossFollowAttack : MonoBehaviour
{
    public GameObject mc; // doesn't need to be defined
    public float speed;
    private float minSpeed = 0.5f;
    private float timer = 0;
    private float sizeTimer;
    private float SIZE_OVERALL = 1f;
    public Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        sizeTimer = SIZE_OVERALL;
        mc = GameObject.FindWithTag("Player");
        Vector3 tempDir = new Vector3(
            mc.transform.position.x-transform.position.x,
            mc.transform.position.y-transform.position.y,
            mc.transform.position.z-transform.position.z
        );

        moveDir = tempDir/tempDir.magnitude;

        speed += (Random.value*(speed-minSpeed))-((speed-minSpeed)/2);
    }

    // Update is called once per frame
    void Update()
    {
        if (sizeTimer > 0){
            timer += Time.deltaTime;
            if (timer >= 3){
                // fizzle out
                transform.localScale = new Vector3(sizeTimer/SIZE_OVERALL, sizeTimer/SIZE_OVERALL, sizeTimer/SIZE_OVERALL); // Default
                sizeTimer -= Time.deltaTime;
            }

            Vector3 tempDir = new Vector3(
                mc.transform.position.x-transform.position.x,
                mc.transform.position.y-transform.position.y,
                mc.transform.position.z-transform.position.z
            );

            moveDir = tempDir/tempDir.magnitude;


            transform.position = new Vector3(transform.position.x+(moveDir.x*speed*Time.deltaTime), 
                                            transform.position.y+(moveDir.y*speed*Time.deltaTime),
                                            transform.position.z+(moveDir.z*speed*Time.deltaTime));
        }
    }
}
