using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Movement", menuName = "Movement System/Movement")]
public class Movement : ScriptableObject
{
    public GameObject toMove;

    public enum MovementOpt { Left, Right, Forward, Backward, Wait, Jump }
    public MovementOpt[] movementOrder;

    public float[] movementNums;
    public Vector3[] movementVectors;

    public bool repeat;

    public float speed;

    private float value;
    private Vector3 valueIncrement;

    private bool running;

    private int idx = 0;

    private bool waitingDone = false;
    private float waitingTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        if (!waitingDone){
            waitingTime = Random.Range(0.0f, 2.5f);
            waitingDone = true;
        } else if (waitingTime > 0) {
            waitingTime -= Time.deltaTime;
        } else {
            if (running){
                bool changing = false;
                if (movementNums.Length > 0){
                    // if moving left/right, the movementNum will be how much to move and the value will be how much has already been moved
                    if (movementOrder[idx] == MovementOpt.Left){
                        if (value > movementNums[idx]){
                            changing = true;
                        } else {
                            toMove.transform.position += new Vector3 (-(speed*Time.deltaTime), 0, 0);
                            value += speed*Time.deltaTime;
                        }
                    } if (movementOrder[idx] == MovementOpt.Right){
                        if (value > movementNums[idx]){
                            changing = true;
                        } else {
                            toMove.transform.position += new Vector3 ((speed*Time.deltaTime), 0, 0);
                            value += speed*Time.deltaTime;
                        }
                    }

                    if (movementOrder[idx] == MovementOpt.Forward){
                        if (value > movementNums[idx]){
                            changing = true;
                        } else {
                            toMove.transform.position += new Vector3 (0, 0, (speed*Time.deltaTime));
                            value += speed*Time.deltaTime;
                        }
                    } if (movementOrder[idx] == MovementOpt.Backward){
                        if (value > movementNums[idx]){
                            changing = true;
                        } else {
                            toMove.transform.position += new Vector3 (0, 0, -(speed*Time.deltaTime));
                            value += speed*Time.deltaTime;
                        }
                    }

                    if (movementOrder[idx] == MovementOpt.Wait){
                        if (value >= movementNums[idx]){
                            changing = true;
                        } else {
                            value += Time.deltaTime;
                        }
                    }
                } else if (movementVectors.Length > 0){
                    if (movementOrder[idx] == MovementOpt.Left || movementOrder[idx] == MovementOpt.Right || movementOrder[idx] == MovementOpt.Forward || movementOrder[idx] == MovementOpt.Backward){
                        // for moving to a spot, the movementVector will be the spot to move to and the valueIncrement will be how much to move each time
                        if (Vector3.Distance(toMove.transform.position, movementVectors[idx]) < valueIncrement.magnitude){
                            changing = true;
                        } else {
                            toMove.transform.position += valueIncrement * Time.deltaTime;
                        }
                    }

                    if (movementOrder[idx] == MovementOpt.Wait){
                        if (value >= movementVectors[idx].magnitude){
                            changing = true;
                        } else {
                            value += Time.deltaTime;
                        }
                    }
                }

                if (movementOrder[idx] == MovementOpt.Jump){
                    SetManager SMR = GameObject.FindWithTag("SETMANAGER").GetComponent<SetManager>();
                    toMove.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0)*SMR.jumpSpeed, ForceMode.Impulse);
                    changing = true;
                }

                if (changing){
                    idx++;
                    if (idx >= movementOrder.Length){
                        if (!repeat){
                            running = false;
                        }
                        idx = 0;
                    }

                    if (movementNums.Length > 0){
                        value = 0;
                    } else if (movementVectors.Length > 0){
                        if (movementOrder[idx] == MovementOpt.Left || movementOrder[idx] == MovementOpt.Right || movementOrder[idx] == MovementOpt.Forward || movementOrder[idx] == MovementOpt.Backward){
                            valueIncrement = (toMove.transform.position-movementVectors[idx])/speed;
                        } if (movementOrder[idx] == MovementOpt.Wait){
                            value = 0;
                        }
                    }
                }
            }
        }
    }

    public void setObject(GameObject npc){
        toMove = npc;
    }

    public void startMoving(){
        running = true;
    }
}
