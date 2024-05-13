using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OatThrow : MonoBehaviour
{
    public ParticleSystem effect;
    public float SPEED = 2.5f;
    private float xSpeed = 0;
    private float ySpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (xSpeed == 0){
                xSpeed = SPEED * -1;
            } if (xSpeed > 0){
                xSpeed = 0;
            }
        } if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (xSpeed == 0){
                xSpeed = SPEED;
            } if (xSpeed < 0){
                xSpeed = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (ySpeed == 0){
                ySpeed = SPEED * -1;
            } if (ySpeed > 0){
                ySpeed = 0;
            }
        } if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (ySpeed == 0){
                ySpeed = SPEED;
            } if (ySpeed < 0){
                ySpeed = 0;
            }
        }

        transform.position = new Vector3 (transform.position.x+(xSpeed*Time.deltaTime), transform.position.y+(ySpeed*Time.deltaTime), 0);

        if (xSpeed < 0){
            transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);
        } else {
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }

        if (transform.position.y > 3){
            transform.position = new Vector3(transform.position.x, 3, transform.position.z);
        } if (transform.position.y < -4){
            transform.position = new Vector3(transform.position.x, -4, transform.position.z);
        }

        if (transform.position.y > 7.5f){
            transform.position = new Vector3(transform.position.x, 7.5f, transform.position.z);
        } if (transform.position.y < -7.5f){
            transform.position = new Vector3(transform.position.x, -7.5f, transform.position.z);
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)){
            effect.transform.position = transform.position;
            effect.Play();
        }
    }
}
