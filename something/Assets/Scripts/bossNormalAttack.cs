// This attack just goes in the direction it starts in

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossNormalAttack : MonoBehaviour
{
    public Vector3 initialDir; // unit vector in a direction
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x+(initialDir.x*speed*Time.deltaTime), 
        transform.position.y+(initialDir.y*speed*Time.deltaTime),
        transform.position.z+(initialDir.z*speed*Time.deltaTime));
    }

    // Sets the initial direction based on a time
    public void SetDirectionTime(float time, float maxTime){
        float angleInDegrees = (time/maxTime)*360;
        float angleInRadians = angleInDegrees * 2 * Mathf.Deg2Rad; // Convert degrees to radians
        float x = Mathf.Cos(angleInRadians);
        float y = Mathf.Sin(angleInRadians);

        initialDir = new Vector3(x, y, 0);
    }
}
