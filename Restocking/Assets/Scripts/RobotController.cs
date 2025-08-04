using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RobotController : MonoBehaviour
{
    public Button restartButton;
    public Button goButton;
    public InstructionDisplayController IDC;
    public LevelCompletionManager LCM;
    public int robotID;

    public int lastDirection = 1;
    public int currentInstructionIdx = 0;
    public bool holdingBox = false;

    public bool selected = false;
    public bool recording = false;
    public List<int> instructions;
    public List<bool> reversedInstructions;

    public GameObject[] selectionSpots;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float wait;
    private Vector3[] moves;
    private Vector3 originalPos;

    public SFXController sfxControl;
    public SFXWalkingController sfxWalkControl;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        recording = true;

        originalPos = transform.position;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
        instructions = new List<int>();
        reversedInstructions = new List<bool>();

        // restartButton.onClick.AddListener(Restart);
        // goButton.onClick.AddListener(Go);

        moves = new Vector3[4];
        moves[0] = new Vector3(0, -1, 0);
        moves[1] = new Vector3(1, 0, 0);
        moves[2] = new Vector3(0, 1, 0);
        moves[3] = new Vector3(-1, 0, 0);

        lastDirection = 1;
        CheckPickUpDropOff();

        for (int i = 0; i < selectionSpots.Length; i++){
            selectionSpots[i].SetActive(false);
        }

        sfxControl = GameObject.FindWithTag("SFXControl").GetComponent<SFXController>();
        sfxWalkControl = GameObject.FindWithTag("SFXWalkControl").GetComponent<SFXWalkingController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SettingsManager.playing && SettingsManager.dialogueOff){
            if (recording){
                if (selected){
                    if (instructions.Count < 5){
                        if (Input.GetKeyDown(KeyCode.DownArrow)) {
                            instructions.Add(0);
                            reversedInstructions.Add(false);
                            IDC.SetDirections(instructions, reversedInstructions);
                        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                            instructions.Add(1);
                            reversedInstructions.Add(false);
                            IDC.SetDirections(instructions, reversedInstructions);
                        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                            instructions.Add(2);
                            reversedInstructions.Add(false);
                            IDC.SetDirections(instructions, reversedInstructions);
                        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                            instructions.Add(3);
                            reversedInstructions.Add(false);
                            IDC.SetDirections(instructions, reversedInstructions);
                        }
                    }
                    for (int i = 0; i < selectionSpots.Length; i++){
                        if (i == (instructions.Count)){
                            selectionSpots[i].SetActive(true);
                        } else {
                            selectionSpots[i].SetActive(false);
                        }
                    }
                }
            } else {
                for (int i = 0; i < selectionSpots.Length; i++){
                    selectionSpots[i].SetActive(false);
                }
                if (instructions.Count > 0){
                    wait -= Time.deltaTime;
                    if (wait < 0){
                        if (selected){
                            IDC.SetHighlight(currentInstructionIdx);
                        }
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
                                if (selected){
                                    IDC.SetDirections(instructions, reversedInstructions);
                                }
                                lastDirection = instructions[currentInstructionIdx];
                                nextPosition = transform.position + moves[instructions[currentInstructionIdx]];
                                break;
                            }
                        }

                        obstacles = GameObject.FindGameObjectsWithTag("Robot");
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
                                if (selected){
                                    IDC.SetDirections(instructions, reversedInstructions);
                                }
                                lastDirection = instructions[currentInstructionIdx];
                                nextPosition = transform.position + moves[instructions[currentInstructionIdx]];
                                break;
                            }
                        }
                        // move
                        animator.SetInteger("Direction", lastDirection);
                        animator.SetBool("Box", holdingBox);
                        CheckPickUpDropOff();
                        transform.position = nextPosition;

                        wait = 1.1f-(((float) SettingsManager.robotSpeed)/10.0f);
                        if (wait == 0){
                            wait = 0.1f;
                        }
                        currentInstructionIdx++;
                        if (currentInstructionIdx >= instructions.Count){
                            currentInstructionIdx = 0;
                        }
                    }
                }

            }

            if (selected){
                spriteRenderer.color = new Color(0.43f, 1f, 0.92f);
            } else if (instructions.Count > 0){
                spriteRenderer.color = new Color(1f, 1f, 1f);
            } else {
                spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
            }
        }
    }

    public void Go(){
        recording = false;
        wait = 0;
        currentInstructionIdx = 0;
        if (instructions.Count > 0){
            sfxWalkControl.StartWalking();
        }
    }

    public void Restart(){
        recording = true;
        lastDirection = 1;
        holdingBox = false;

        animator.SetInteger("Direction", lastDirection);
        animator.SetBool("Box", holdingBox);

        instructions = new List<int>();
        reversedInstructions = new List<bool>();
        IDC.SetDirections(instructions, reversedInstructions);
        transform.position = originalPos;
        sfxWalkControl.StopWalking();
    }

    public void Select(){
        selected = true;
        IDC.SetDirections(instructions, reversedInstructions);
    }

    public void Deselect(){
        selected = false;
    }

    public void FlipHorizontal(){
        List<int> newInstructions = new List<int>();
        for (int i = 0; i < instructions.Count; i++){
            if (instructions[i] == 1 || instructions[i] == 3){
                newInstructions.Add((instructions[i]+2)%4);
                reversedInstructions[i] = !reversedInstructions[i];
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
                reversedInstructions[i] = !reversedInstructions[i];
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
                if (!holdingBox){
                    holdingBox = true;
                    sfxControl.PlayBoxUp();
                }
                break;
            }
        }

        // TODO check if on drop-off point
        GameObject[] dropoffSpots = GameObject.FindGameObjectsWithTag("Drop-off");
        for (int i = 0; i < dropoffSpots.Length; i++){
            if (dropoffSpots[i].GetComponent<Collider2D>().OverlapPoint(transform.position)){
                // grab package
                if (holdingBox){
                    holdingBox = false;
                    LCM.DroppedOffBox();
                    sfxControl.PlayBoxDown();
                }
                break;
            }
        }
    }
}