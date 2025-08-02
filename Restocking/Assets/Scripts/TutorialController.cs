using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;


public class TutorialController : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject waitingSymbol;
    public TMP_Text dialogueTextBox;
    public Image charImg;

    public RobotsManager RM;

    public string[] dialogue;
    public Sprite[] dialogueImgs;
    public GameObject[] steps;

    private int idx;
    private bool running;
    private bool waiting;
    private float defaultTextSpeed = 0.025f;
    private float currentTextSpeed = 0.025f;
    public int currentSpot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SettingsManager.dialogueOff = false;
        idx = 0;
        currentSpot = -1;

        dialoguePanel.SetActive(true);
        charImg.sprite = dialogueImgs[idx];

        running = true;
        waiting = false;

        for (int i = 0; i < steps.Length; i++){
            steps[i].SetActive(false);
        }

        dialogueTextBox.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    // Update is called once per frame
    void Update()
    {
        if (SettingsManager.playing){
            if (currentSpot == -1){
                if (running){
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
                        if (dialogueTextBox.text == dialogue[idx]){
                            NextLine();
                        } else {
                            currentTextSpeed = defaultTextSpeed/4;
                        }
                    }

                    if (waiting == true){
                        waitingSymbol.SetActive(true);
                    } else {
                        waitingSymbol.SetActive(false);
                    }
                }
            } else {
                if (currentSpot == 0){
                    if (RM.selectedRobot != -1){
                        steps[currentSpot].SetActive(false);
                        if (currentSpot+1 < steps.Length){
                            currentSpot++;
                            steps[currentSpot].SetActive(true);
                        }
                    }
                } else if (currentSpot == 1){
                    if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow)){
                        steps[currentSpot].SetActive(false);
                        if (currentSpot+1 < steps.Length){
                            currentSpot++;
                            steps[currentSpot].SetActive(true);
                        }
                    }
                } else if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
                    steps[currentSpot].SetActive(false);
                    if (currentSpot+1 < steps.Length){
                        currentSpot++;
                        steps[currentSpot].SetActive(true);
                    }
                }
            }
        }
    }
    
    IEnumerator TypeLine()
    {
        int i = 0;
        bool closing = false;
        foreach (char c in dialogue[idx].ToCharArray()){
            dialogueTextBox.text += c;
            i ++;
            if (closing && c == '>'){
                closing = false;
            } if (!closing){
                if (c == '<'){
                    closing = true;
                } else {
                    if (c == '!' || c == '.' || c == '?'){
                        yield return new WaitForSeconds(currentTextSpeed*3);
                    } else {
                        yield return new WaitForSeconds(currentTextSpeed);
                    }
                }
            }
        }

        waiting = true;
    }

    void NextLine()
    {
        if (idx < dialogue.Length -1) {
            idx++;
            
            waiting = false;
            currentTextSpeed = defaultTextSpeed;
            dialogueTextBox.text = string.Empty;
            charImg.sprite = dialogueImgs[idx];

            StartCoroutine(TypeLine());
        } else {
            running = false;
            waiting = false;
            dialoguePanel.SetActive(false);
            SettingsManager.dialogueOff = true;
            currentSpot++;
            steps[currentSpot].SetActive(true);
        }
    }
}
