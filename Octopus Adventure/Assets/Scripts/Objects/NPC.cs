using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Conversation[] conversations;
    public Conversation defaultConversation;

    private bool facingRight = false;
    private float speed = 5f;

    private Vector3 rightFacing;
    private Vector3 leftFacing;
    
    public Movement moverTemplate;
    private Movement mover;

    public SetManager SMR;
    public MessageManager MSMR;
    public GameObject player;
    void Start()
    {
        SMR = GameObject.FindWithTag("SETMANAGER").GetComponent<SetManager>();
        MSMR = GameObject.FindWithTag("MESSAGEMANAGER").GetComponent<MessageManager>();
        player = GameObject.FindWithTag("Player");

        leftFacing = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        rightFacing = new Vector3(-1*transform.localScale.x, transform.localScale.y, transform.localScale.z);

        foreach(Conversation conv in conversations){
            conv.setNPC(gameObject.GetComponent<NPC>());
            conv.setSMR();
        }

        defaultConversation.setNPC(gameObject.GetComponent<NPC>());
        mover = Instantiate(moverTemplate);
        mover.setObject(gameObject);
        mover.startMoving();
    }

    // Update is called once per frame
    void Update()
    {
        if (SMR.playing){
            mover.Update(); //maybe it should be outside pause
            if (Vector3.Distance(transform.position, player.transform.position) < SMR.interactableDist){
                // face player
                if (transform.position.x < player.transform.position.x){
                    // face right
                    transform.localScale = Vector3.Lerp (transform.localScale, rightFacing, speed * Time.deltaTime);
                    facingRight = true;
                } else {
                    // face left
                    transform.localScale = Vector3.Lerp (transform.localScale, leftFacing, speed * Time.deltaTime);
                    facingRight = false;
                    // if (facingRight){
                    //     transform.localScale = Vector3.Lerp (transform.localScale, leftFacing, speed * Time.deltaTime);
                    //     if (Vector3.Distance(transform.localScale, leftFacing) < 0.1f){
                    //         transform.localScale = leftFacing;
                    //         facingRight = false;
                    //     }
                    // }
                }

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
            } else {
                if (facingRight){
                    transform.localScale = Vector3.Lerp(transform.localScale, rightFacing, speed * Time.deltaTime);
                } else {
                    transform.localScale = Vector3.Lerp(transform.localScale, leftFacing, speed * Time.deltaTime);
                }
            }
        }
    }

    public void Jump(){
        Debug.Log("Jumped!");
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0)*SMR.jumpSpeed, ForceMode.Impulse);
    }
}
