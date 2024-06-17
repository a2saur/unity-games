using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This NPC class requires a MessageManager and Player Object to be present
public class NPC : MonoBehaviour
{
    public Conversation[] conversations;
    public Conversation defaultConversation;

    private bool facingRight = false;
    private float speed = 5f;

    private Vector3 rightFacing;
    private Vector3 leftFacing;
    
    public Movement_2D moverTemplate;
    private Movement_2D mover;

    public MessageManager MSMR;
    public GameObject player;
    void Start()
    {
        // Find Objects
        MSMR = GameObject.FindWithTag("MESSAGEMANAGER").GetComponent<MessageManager>();
        player = GameObject.FindWithTag("Player");

        // Set left + right scales for reference
        leftFacing = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        rightFacing = new Vector3(-1*transform.localScale.x, transform.localScale.y, transform.localScale.z);

        // link the conversations to this NPC
        foreach(Conversation conv in conversations){
            conv.setNPC(gameObject.GetComponent<NPC>());
        }
        defaultConversation.setNPC(gameObject.GetComponent<NPC>());

        // create a copy of the movement
        mover = Instantiate(moverTemplate);
        mover.setObject(gameObject);
        mover.startMoving();
    }

    // Update is called once per frame
    void Update()
    {
        if (SettingsManager.playing){
            // Move
            mover.Update();
            if (Vector3.Distance(transform.position, player.transform.position) < SettingsManager.interactableDist){
                // face player
                if (transform.position.x < player.transform.position.x){
                    // face right
                    transform.localScale = Vector3.Lerp (transform.localScale, rightFacing, speed * Time.deltaTime);
                    facingRight = true;
                } else {
                    // face left
                    transform.localScale = Vector3.Lerp (transform.localScale, leftFacing, speed * Time.deltaTime);
                    facingRight = false;
                }

                // check for interact press
                if (Input.GetKeyDown(SettingsManager.interactButton)){
                    if (MSMR.CanStart()){
                        // Starting a conversation: pause the game and then go through the conversations (if none are valid, then use the default convo)
                        SettingsManager.Pause();
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

    // public void Jump(){
    //     Debug.Log("Jumped!");
    //     GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0)*SettingsManager.jumpSpeed, ForceMode.Impulse);
    // }
}
