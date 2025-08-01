using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RobotControllerSingular : MonoBehaviour
{
    public Button restartButton;
    public Button goButton;
    public InstructionDisplayController IDC;

    public int lastDirection = 0;
    public int currentInstructionIdx = 0;
    public bool holdingBox = false;

    public bool recording = true;
    public List<int> instructions;

    private Animator animator;
    private float wait;
    private Vector3[] moves;
    private Vector3 originalPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPos = transform.position;
        animator = GetComponent<Animator>();
        instructions = new List<int>();

        restartButton.onClick.AddListener(Restart);
        goButton.onClick.AddListener(Go);

        moves = new Vector3[4];
        moves[0] = new Vector3(0, -1, 0);
        moves[1] = new Vector3(1, 0, 0);
        moves[2] = new Vector3(0, 1, 0);
        moves[3] = new Vector3(-1, 0, 0);

        CheckPickUpDropOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (SettingsManager.playing){
            if (recording){
                if (instructions.Count < 5){
                    if (Input.GetKeyDown(KeyCode.DownArrow)) {
                        instructions.Add(0);
                        IDC.SetDirections(instructions);
                    } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                        instructions.Add(1);
                        IDC.SetDirections(instructions);
                    } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                        instructions.Add(2);
                        IDC.SetDirections(instructions);
                    } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                        instructions.Add(3);
                        IDC.SetDirections(instructions);
                    }
                }
            } else {
                wait -= Time.deltaTime;
                if (wait < 0){
                    IDC.SetHighlight(currentInstructionIdx);
                    lastDirection = instructions[currentInstructionIdx];
                    
                    Vector3 nextPosition = transform.position + moves[instructions[currentInstructionIdx]];

                    // check if hit obstacle
                    GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
                    for (int i = 0; i < obstacles.Length; i++){
                        if (obstacles[i].GetComponent<Collider2D>().OverlapPoint(nextPosition)){
                            // Hit! Reverse
                            if (lastDirection == 0 || lastDirection == 2){
                                // down or up
                                FlipVertical();
                            } else if (lastDirection == 1 || lastDirection == 3){
                                // left or right
                                FlipHorizontal();
                            }
                            IDC.SetDirections(instructions);
                            lastDirection = instructions[currentInstructionIdx];
                            nextPosition = transform.position + moves[instructions[currentInstructionIdx]];
                            break;
                        }
                    }
                    // move
                    animator.SetInteger("Direction", lastDirection);
                    animator.SetBool("Box", holdingBox);
                    transform.position = nextPosition;
                    CheckPickUpDropOff();

                    wait = 1-(SettingsManager.robotSpeed/10);
                    if (wait == 0){
                        wait = 0.05f;
                    }
                    currentInstructionIdx++;
                    if (currentInstructionIdx >= instructions.Count){
                        currentInstructionIdx = 0;
                    }
                }
            }
        }
    }

    public void Go(){
        recording = false;
        wait = 0;
        currentInstructionIdx = 0;
    }

    public void Restart(){
        recording = true;
        instructions = new List<int>();
        IDC.SetDirections(instructions);
        transform.position = originalPos;
    }

    public void FlipHorizontal(){
        List<int> newInstructions = new List<int>();
        for (int i = 0; i < instructions.Count; i++){
            if (instructions[i] == 1 || instructions[i] == 3){
                newInstructions.Add((instructions[i]+2)%4);
            } else {
                newInstructions.Add(instructions[i]);
            }
        }
        instructions = newInstructions;
    }

    public void FlipVertical(){
        List<int> newInstructions = new List<int>();
        for (int i = 0; i < instructions.Count; i++){
            if (instructions[i] == 0 || instructions[i] == 2){
                newInstructions.Add((instructions[i]+2)%4);
            } else {
                newInstructions.Add(instructions[i]);
            }
        }
        instructions = newInstructions;
    }

    public void CheckPickUpDropOff(){
        // TODO check if on pickup point
        GameObject[] pickupSpots = GameObject.FindGameObjectsWithTag("Pick-up");
        for (int i = 0; i < pickupSpots.Length; i++){
            if (pickupSpots[i].GetComponent<Collider2D>().OverlapPoint(transform.position)){
                // grab package
                holdingBox = true;
                break;
            }
        }
        // TODO check if on drop-off point
        GameObject[] dropoffSpots = GameObject.FindGameObjectsWithTag("Drop-off");
        for (int i = 0; i < dropoffSpots.Length; i++){
            if (dropoffSpots[i].GetComponent<Collider2D>().OverlapPoint(transform.position)){
                // grab package
                holdingBox = false;
                break;
            }
        }
    }
}