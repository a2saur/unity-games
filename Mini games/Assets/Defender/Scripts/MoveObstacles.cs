using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacles : MonoBehaviour
{
    public Ship ship;
    private int max_pos = 50;

    // Start is called before the first frame update
    void Start()
    {
        ship = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Ship>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x-(ship.speed*Time.deltaTime), transform.position.y, transform.position.z);

        if (transform.position.x > max_pos){
            transform.position = new Vector3(-max_pos, transform.position.y, transform.position.z);
        } if (transform.position.x < -max_pos){
            transform.position = new Vector3(max_pos, transform.position.y, transform.position.z);
        }
    }
}
