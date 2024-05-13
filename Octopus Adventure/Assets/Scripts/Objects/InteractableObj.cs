using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObj : MonoBehaviour
{
    public Conversation[] conversations;
    public Conversation defaultConversation;

    public SetManager SMR;
    public MessageManagerInteractable MSMR;
    public GameObject player;
    void Start()
    {
        SMR = GameObject.FindWithTag("SETMANAGER").GetComponent<SetManager>();
        MSMR = GameObject.FindWithTag("MESSAGEMANAGER").GetComponent<MessageManagerInteractable>();
        player = GameObject.FindWithTag("Player");

        foreach(Conversation conv in conversations){
            conv.setNPC(gameObject.GetComponent<InteractableObj>());
            conv.setSMR();
        }

        defaultConversation.setNPC(gameObject.GetComponent<InteractableObj>());
    }

    // Update is called once per frame
    void Update()
    {
        if (SMR.playing){
            if (Vector3.Distance(transform.position, player.transform.position) < SMR.interactableDist){
                // check for interact press
                if (Input.GetKeyDown(SMR.interactButton)){
                    if (MSMR.CanStart()){
                        SMR.Pause();
                        // to do - check which conversation is valid
                        bool defaultConv = true;
                        foreach (Conversation conv in conversations){
                            if (conv.CriteriaMet()){
                                defaultConv = false;
                                MSMR.StartText(conv.getLines(), conv.getEndActions());
                            }
                        }

                        if (defaultConv){
                            MSMR.StartText(defaultConversation.getLines(), defaultConversation.getEndActions());
                        }
                    }
                }
            }
        }
    }
}
