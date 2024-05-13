using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baker : MonoBehaviour
{
    public Sprite normal;
    public Sprite sad;
    public GameObject sprintImage;
    public float SPEED = 2.5f;
    public float recovery = 2.5f;
    public float sprint = 1f;
    public float sprintWait = -3f;
    public SetManager SETMANAGER;

    private float xSpeed = 0;
    private float ySpeed = 0;
    private float recovering = -1;
    private float sprinting = -1;
    private bool sprintable = true;

    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip soundEffect; // The sound effect you want to play
    // Start is called before the first frame update
    void Start()
    {
        SETMANAGER = GameObject.FindWithTag("SetManager").GetComponent<SetManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sprintable){
            sprintImage.SetActive(true);
        } else {
            sprintImage.SetActive(false);
        }

        if (!SETMANAGER.isPaused){
            if (recovering > 0){
                GetComponent<SpriteRenderer>().sprite = sad;
                recovering -= Time.deltaTime;
            } else {
                GetComponent<SpriteRenderer>().sprite = normal;
            }

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

            if (sprinting > sprintWait){
                sprinting -= Time.deltaTime;
                if (sprinting < 0){
                    if (ySpeed > 0){
                        ySpeed = SPEED;
                    } if (ySpeed < 0){
                        ySpeed = -SPEED;
                    }

                    if (xSpeed > 0){
                        xSpeed = SPEED;
                    } if (xSpeed < 0){
                        xSpeed = -SPEED;
                    }
                }

                if (sprinting < sprintWait){
                    sprintable = true;
                }
            }

            if (sprintable) {
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)){
                    if (ySpeed > 0){
                        ySpeed = SPEED * 2.0f;
                    } if (ySpeed < 0){
                        ySpeed = SPEED * -2.0f;
                    }

                    if (xSpeed > 0){
                        xSpeed = SPEED * 2.0f;
                    } if (xSpeed < 0){
                        xSpeed = SPEED * -2.0f;
                    }
                    sprinting = sprint;
                    sprintable = false;
                }
            }

            transform.position = new Vector3 (transform.position.x+(xSpeed*Time.deltaTime), transform.position.y+(ySpeed*Time.deltaTime), 0);


            if (transform.position.y > 3.5f){
                transform.position = new Vector3(transform.position.x, 3.5f, transform.position.z);
            } if (transform.position.y < -3.5f){
                transform.position = new Vector3(transform.position.x, -3.5f, transform.position.z);
            }

            if (transform.position.x < SETMANAGER.StartPos[SETMANAGER.currentLevel]){
                transform.position = new Vector3(SETMANAGER.StartPos[SETMANAGER.currentLevel], transform.position.y, transform.position.z);
                xSpeed = 0;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if (recovering < 0){
            if (other.gameObject.CompareTag("Goose")) {
                if (SETMANAGER.LevelScores[SETMANAGER.currentLevel] > 0){
                    SETMANAGER.LevelScores[SETMANAGER.currentLevel]--;
                    // play sound
                    audioSource.PlayOneShot(soundEffect);
                }
                recovering = recovery;
            }
        }
    }

    public void ResetMovement(){
        xSpeed = 0;
        ySpeed = 0;
    }
}
