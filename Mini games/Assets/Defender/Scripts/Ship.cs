using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public GameObject laser;
    public bool dir = true; // right
    public float speed = 0;
    public int score = 0;

    private float max_speed = 10;
    private float speed_change = 0.075f;
    private float speed_slow = 0.01f;
    private float vertical_speed = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (speed > speed_slow){
            speed -= speed_slow;
        } else if (speed < -speed_slow){
            speed += speed_slow;
        } else {
            speed = 0;
        }

        if (Input.GetKey(KeyCode.RightArrow)){
            if (dir){
                speed += speed_change;
            } else {
                dir = true;
                speed = speed/-10;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow)){
            if (dir){
                dir = false;
                speed = speed/-10;
            } else {
                speed -= speed_change;
            }
        }

        if (Input.GetKey(KeyCode.UpArrow)){
            if (transform.position.y < 4){
                transform.position = new Vector3(transform.position.x, transform.position.y+(vertical_speed*Time.deltaTime), transform.position.z);
            }
        } if (Input.GetKey(KeyCode.DownArrow)){
            if (transform.position.y > -4){
                transform.position = new Vector3(transform.position.x, transform.position.y-(vertical_speed*Time.deltaTime), transform.position.z);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)){
            Instantiate(laser, new Vector3(transform.position.x+0.75f, transform.position.y, transform.position.z), Quaternion.identity);
        }

        if (speed < -max_speed){
            speed = -max_speed;
        } if (speed > max_speed){
            speed = max_speed;
        }
    }
}
