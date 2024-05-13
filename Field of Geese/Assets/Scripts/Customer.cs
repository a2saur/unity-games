using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Customer : MonoBehaviour
{
    public SetManager SETMANAGER;
    public string customerName;
    public int passingNum;
    public RawImage charImageSpot;
    public Texture charImage;
    public Spawner spawner;
    public GameObject player;
    public GameObject textBox;
    public TMP_Text nameBox;
    public TMP_Text messageBox;
    public GameObject buttons;
    public Buttons buttonController;
    public string[] endMessages;
    private bool done;

    // Start is called before the first frame update
    void Start()
    {
        textBox.SetActive(false);
        buttons.SetActive(false);
        SETMANAGER = GameObject.FindWithTag("SetManager").GetComponent<SetManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (done){
            if (SETMANAGER.LevelScores[customerName] < passingNum){
                // end
                SceneManager.LoadScene("Start");
            } else {
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)){
                    if (customerName != "Kai Batta" && customerName != "Cory Sant" && customerName != "Stuart O. Bread"){
                        Debug.Log("Normal");
                        textBox.SetActive(false);
                        SETMANAGER.isPaused = false;
                        done = false;
                        
                        SETMANAGER.currentLevel = SETMANAGER.NextLevels[SETMANAGER.currentLevel];
                        player.transform.position = new Vector3(SETMANAGER.StartPos[SETMANAGER.currentLevel], player.transform.position.y, player.transform.position.z);
                        player.GetComponent<Baker>().ResetMovement();

                        spawner.whichSpawn = true;
                        spawner.Clear();
                        spawner.SetValues();
                        spawner.Spawn();
                    } else if (customerName == "Kai Batta") {
                        Debug.Log("Bread");
                        if (buttonController.done){
                            textBox.SetActive(false);
                            SETMANAGER.isPaused = false;
                            done = false;
                            
                            SETMANAGER.currentLevel = SETMANAGER.NextLevels[SETMANAGER.currentLevel];
                            player.transform.position = new Vector3(SETMANAGER.StartPos[SETMANAGER.currentLevel], player.transform.position.y, player.transform.position.z);
                            player.GetComponent<Baker>().ResetMovement();

                            spawner.Clear();
                            spawner.SetValues();
                            spawner.Spawn();
                            spawner.whichSpawn = true;
                            buttonController.done = false;
                            buttons.SetActive(false);

                            if (buttonController.choice){
                                SETMANAGER.sandwichCleared();
                                buttonController.choice = false;
                            }
                        } else {
                            messageBox.text = "Hey, there's enough bread for you to make a sandwich, too! Wanna give it a go?";
                            buttons.SetActive(true);
                        }
                    } else if (customerName == "Cory Sant"){
                        Debug.Log("Ducks");
                        if (buttonController.done){
                            if (buttonController.choice){
                                spawner.whichSpawn = false;
                                buttonController.choice = false;
                            }

                            textBox.SetActive(false);
                            SETMANAGER.isPaused = false;
                            done = false;
                            
                            SETMANAGER.currentLevel = SETMANAGER.NextLevels[SETMANAGER.currentLevel];
                            player.transform.position = new Vector3(SETMANAGER.StartPos[SETMANAGER.currentLevel], player.transform.position.y, player.transform.position.z);
                            player.GetComponent<Baker>().ResetMovement();

                            spawner.Clear();
                            spawner.SetValues();
                            spawner.Spawn();
                            buttonController.done = false;
                            buttons.SetActive(false);
                        } else {
                            messageBox.text = "Hey, I heard you\'ve been having trouble with geese lately. I know a path here that never has geese on it, want me to show you?";
                            buttons.SetActive(true);
                        }
                    } else if (customerName == "Stuart O. Bread"){
                        Debug.Log("End");
                        SceneManager.LoadScene("End");
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SETMANAGER.isPaused = true;
            textBox.SetActive(true);
            done = true;

            nameBox.text = customerName;
            messageBox.text = endMessages[SETMANAGER.LevelScores[customerName]];
            charImageSpot.GetComponent<RawImage>().texture = charImage;
        }
    }
}
