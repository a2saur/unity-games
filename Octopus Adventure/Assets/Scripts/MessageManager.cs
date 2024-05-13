using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class MessageManager : MonoBehaviour
{
    public GameObject messagePanel;
    public TextMeshProUGUI messageBox;
    public GameObject interactable;
    public GameObject waitingSymbol;
    public string[] lines;
    public UnityEvent endActions;
    public float currentTextSpeed;

    public bool running = false;
    public bool waiting = false;
    public bool done = false;

    private int index;
    private float cooldown = 0.5f;
    private float cooldownCounter;

    public SetManager SMR;
    public GameObject player;
    void Start()
    {
        SMR = GameObject.FindWithTag("SETMANAGER").GetComponent<SetManager>();
        player = GameObject.FindWithTag("Player");

        interactable.SetActive(false);

        messagePanel.SetActive(false);
        messageBox.text = string.Empty;
        currentTextSpeed = SMR.defaultTextSpeed;
    }

    void Update()
    {
        if (SMR.playing){
            if (cooldownCounter < 0){
                GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
                bool closeBy = false;
                foreach (GameObject npc in npcs)
                {
                    if (Vector3.Distance(npc.transform.position, player.transform.position) < SMR.interactableDist){
                        interactable.transform.position = npc.transform.position + new Vector3(0, 1.5f, 0);
                        closeBy = true;
                        break;
                    }
                }

                if (closeBy && !interactable.activeSelf){
                    interactable.SetActive(true);
                } else if (!closeBy && interactable.activeSelf){
                    interactable.SetActive(false);
                }
            } else {
                cooldownCounter -= Time.deltaTime;
            }
        } else if (running){
            // finish line/next line
            if (Input.GetKeyDown(SMR.interactButton)){
                if (messageBox.text == lines[index]){
                    NextLine();
                } else {
                    currentTextSpeed = SMR.defaultTextSpeed/4;
                }
            } if (Input.GetKeyDown(SMR.backButton)) {
                if (messageBox.text == lines[index]){
                    NextLine();
                    currentTextSpeed = SMR.defaultTextSpeed/10;
                } else {
                    currentTextSpeed = SMR.defaultTextSpeed/10;
                }
            }

            if (waiting == true){
                waitingSymbol.SetActive(true);
            } else {
                waitingSymbol.SetActive(false);
            }
        }
    }

    public void StartText(string[] newLines)
    {
        lines = newLines;
        messagePanel.SetActive(true);
        interactable.SetActive(false);

        index = 0;
        running = true;
        waiting = false;
        done = false;

        cooldownCounter = cooldown;

        messageBox.text = string.Empty;
        StartCoroutine(TypeLine());
    }
    
    public void StartText(string[] newLines, UnityEvent newEndActions)
    {
        lines = newLines;
        endActions = newEndActions;
        messagePanel.SetActive(true);
        interactable.SetActive(false);

        index = 0;
        running = true;
        waiting = false;
        done = false;

        cooldownCounter = cooldown;

        messageBox.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        int i = 0;
        bool closing = false;
        foreach (char c in lines[index].ToCharArray()){
            messageBox.text += c;
            i ++;
            if (closing && c == '>'){
                closing = false;
            } if (!closing){
                if (c == '<'){
                    closing = true;
                } else {
                    yield return new WaitForSeconds(currentTextSpeed);
                }
            }
        }

        waiting = true;
    }

    void NextLine()
    {
        if (index < lines.Length -1) {
            index++;
            
            waiting = false;
            currentTextSpeed = SMR.defaultTextSpeed;
            messageBox.text = string.Empty;

            StartCoroutine(TypeLine());
        } else {
            running = false;
            waiting = false;
            done = true;
            messagePanel.SetActive(false);

            if (endActions != null){
                endActions.Invoke();
            } 
            SMR.Resume();
        }
    }

    public bool CanStart(){
        if (cooldownCounter < 0 && !running){
            return true;
        } else {
            return false;
        }
    }
}
