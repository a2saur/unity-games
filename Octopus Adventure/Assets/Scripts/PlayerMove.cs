using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool isGrounded = true;

    private float speed = 5f;

    private Vector3 rightFacing;
    private Vector3 leftFacing;
    private bool facingRight = true;

    public SetManager SMR;
    void Start()
    {
        SMR = GameObject.FindWithTag("SETMANAGER").GetComponent<SetManager>();

        leftFacing = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        rightFacing = new Vector3(-1*transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void OnCollisionStay(){
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (SMR.playing){
            // move left/right
            if (Input.GetKey(SMR.rightArrow)){
                transform.position += Vector3.right * speed * Time.deltaTime;
                facingRight = true;
            } if (Input.GetKey(SMR.leftArrow)){
                transform.position += Vector3.left * speed * Time.deltaTime;
                facingRight = false;
            }

            // move up/down
            if (Input.GetKey(SMR.upArrow)){
                transform.position += Vector3.forward * speed * Time.deltaTime;
            } if (Input.GetKey(SMR.downArrow)){
                transform.position += Vector3.back * speed * Time.deltaTime;
            }
            
            // jump
            if (Input.GetKeyDown(SMR.jumpButton) && isGrounded){
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0)*SMR.jumpSpeed, ForceMode.Impulse);
                isGrounded = false;
            }

            // flip sides
            if (facingRight){
                    transform.localScale = Vector3.Lerp(transform.localScale, rightFacing, speed * Time.deltaTime);
            } else {
                transform.localScale = Vector3.Lerp(transform.localScale, leftFacing, speed * Time.deltaTime);
            }
        }
    }
}
