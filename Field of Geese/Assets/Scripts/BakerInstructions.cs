using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BakerInstructions : MonoBehaviour
{
    public GameObject pauseScreen;
    public TMP_Text textSpot;

    public GameObject sprintImage;
    public float SPEED = 2.5f;
    public float recovery = 2.5f;
    public float sprint = 1f;
    public float sprintWait = -3f;

    private float xSpeed = 0;
    private float ySpeed = 0;
    private float recovering = -1;
    private float sprinting = -1;
    private bool sprintable = true;

    public Texture stars4;
    public Texture stars3;
    public Texture stars2;
    public Texture stars1;
    public Texture stars0;
    public RawImage starSpot;
    private Dictionary<int, Texture> stars;

    private bool playing = false;
    private int stage = 0;
    private int textStage = 0;
    private int starCount = 3;

    // Start is called before the first frame update
    void Start()
    {
        stars = new Dictionary<int, Texture>() {
            {0, stars0},
            {1, stars1},
            {2, stars2},
            {3, stars3},
            {4, stars4},
        };

        sprintImage.SetActive(false);
    }

    void Update()
    {
        if (playing){
            starSpot.texture = stars[starCount];

            if (stage == 0){
                // pass
            } if (stage > 0){
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

                if (stage == 1 && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))){
                    StartCoroutine(PauseSoon());
                }
            } if (stage > 1){
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

                if (stage == 2 && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))){
                    StartCoroutine(PauseSoon());
                }
            } if (stage > 2){
                if (sprintable){
                    sprintImage.SetActive(true);
                } else {
                    sprintImage.SetActive(false);
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
                        if (stage == 3){
                            StartCoroutine(PauseSoon());
                        }
                    }
                }
            }

            transform.position = new Vector3 (transform.position.x+(xSpeed*Time.deltaTime), transform.position.y+(ySpeed*Time.deltaTime), 0);


            if (transform.position.y > 3.5f){
                transform.position = new Vector3(transform.position.x, 3.5f, transform.position.z);
            } if (transform.position.y < -3.5f){
                transform.position = new Vector3(transform.position.x, -3.5f, transform.position.z);
            }

            if (transform.position.x < 15){
                transform.position = new Vector3(15, transform.position.y, transform.position.z);
                xSpeed = 0;
            } if (transform.position.x > 100){
                transform.position = new Vector3(100, transform.position.y, transform.position.z);
                xSpeed = 0;
            }
        } else {
            pauseScreen.SetActive(true);
            if (stage == 0){
                if (textStage == 0){
                    textSpot.text = "Welcome! First things first, you have the player (on the left), and your current score (on the right)";
                    if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)){
                        textStage++;
                    }
                } else if (textStage == 1){
                    textSpot.text = "To move left/right, use the left and right arrow keys. Press an arrow key to start moving, and press the opposite arrow key to stop. Try it out!";
                    if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)){
                        textStage = 0;
                        stage++;
                        pauseScreen.SetActive(false);
                        playing = true;
                    }
                }
            } else if (stage == 1){
                textSpot.text = "Moving up and down is the same but with the up and down arrow keys. Try it out! (As a reminder: press an arrow key to start moving, and press the opposite arrow key to stop)";
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)){
                    stage++;
                    pauseScreen.SetActive(false);
                    playing = true;
                }
            } else if (stage == 2){
                sprintImage.SetActive(true);
                textSpot.text = "Lastly, sprinting! Sometimes you\'ll need a quick burst of speed. There is a white diamond in the upper left corner that shows if you can sprint. If you can see it, press the spacebar or click the screen to sprint. Try it out!";
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)){
                    stage++;
                    pauseScreen.SetActive(false);
                    playing = true;
                }
            } else {
                sprintImage.SetActive(true);
                textSpot.text = "That\'s all the controls! Feel free to keep trying it out, and when you\'re ready to play, press the button in the bottom right corner";
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)){
                    stage++;
                    pauseScreen.SetActive(false);
                    playing = true;
                }
            }
        }
    }

    public void ResetMovement(){
        xSpeed = 0;
        ySpeed = 0;
    }

    IEnumerator PauseSoon()
    {
        yield return new WaitForSeconds(2.5f);

        playing = false;
    }

    public void startButton(){
        SceneManager.LoadScene("Start");
    }
}
