using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    public GameObject[] interactOptButtons;

    public GameObject playerObj;
    public MessageManager msgMan;
    public GameObject commandWheel;

    private bool interactable = false; // if anything should happen when you press a button
    private GameObject interactableObj = null; // object to interact with
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        msgMan = GameObject.FindGameObjectWithTag("MessageManager").GetComponent<MessageManager>();
        commandWheel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (SettingsManager.playing){

            if (!interactable){
                foreach (GameObject obj in interactOptButtons){
                    obj.SetActive(false);
                }
                
                // nothing is being interacted with, check for interaction
                float minDist = 10000;
                float objDist;
                GameObject[] objs;
                foreach (string tag_interactable in SettingsManager.tags_interactable){
                    objs = GameObject.FindGameObjectsWithTag(tag_interactable);
                    foreach (GameObject obj in objs)
                    {
                        // check if interactable
                        objDist = Mathf.Abs(Vector3.Distance(playerObj.transform.position, obj.transform.position));
                        if (objDist < SettingsManager.thresholdInteractDist){
                            if (objDist < minDist){
                                minDist = objDist;
                                interactableObj = obj;
                                interactable = true;
                            }
                        }
                    }
                }
            } else {
                InteractableObj interactObjInfo = interactableObj.GetComponent<InteractableObj>();
                List<string> interactOpts = interactObjInfo.getApplicableKeyNames();
                InteractableOptButton intOptbutton;
                for (int i = 0; i < interactOpts.Count; i++){
                    if (interactOpts[i] == "Interact"){
                        interactOptButtons[i].SetActive(true);
                        intOptbutton = interactOptButtons[i].GetComponent<InteractableOptButton>();
                        intOptbutton.SetButtonName(SettingsManager.interactButton);

                        if (interactObjInfo.getInteractionType() == "Dialogue"){
                            intOptbutton.SetButtonLabel("Talk");
                            if (Input.GetKey(SettingsManager.interactButton)) {
                                // Get dialogue
                                msgMan.StartDialogue(interactObjInfo.getDialogue());
                            }
                        } else {
                            intOptbutton.SetButtonLabel("Interact");
                            // Other interaction?
                        }
                    } else if (interactOpts[i] == "Command"){
                        interactOptButtons[i].SetActive(true);
                        intOptbutton = interactOptButtons[i].GetComponent<InteractableOptButton>();
                        intOptbutton.SetButtonName(SettingsManager.commandButton);
                        intOptbutton.SetButtonLabel("Command");
                        if (Input.GetKey(SettingsManager.commandButton)) {
                            // Command?
                            commandWheel.SetActive(true);
                        }
                    }
                }
                
                // check if no longer interactable
                float objDist = Mathf.Abs(Vector3.Distance(playerObj.transform.position, interactableObj.transform.position));
                if (objDist >= SettingsManager.thresholdInteractDist){
                    interactableObj = null;
                    interactable = false;
                }
            }
        } else {
            foreach (GameObject obj in interactOptButtons){
                obj.SetActive(false);
            }
        }
    }
}
