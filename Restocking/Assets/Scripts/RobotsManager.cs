using UnityEngine;
using System.Collections.Generic;

public class RobotsManager : MonoBehaviour
{
    public RobotController[] robots;
    // public bool recording;
    public int selectedRobot;
    public InstructionDisplayController IDC;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // recording = true;
        selectedRobot = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null) {
                GameObject clickedObj = hit.collider.gameObject;
                if (clickedObj.CompareTag("Robot")){
                    // clicked on a robot!
                    int selectedRobotID = clickedObj.GetComponent<RobotController>().robotID;

                    for (int i = 0; i < robots.Length; i++){
                        if (robots[i].robotID == selectedRobotID){
                            // found it!
                            selectedRobot = i;
                            SelectRobot(selectedRobot);
                            break;
                        }
                    }
                } else {
                    if (selectedRobot != -1){
                        robots[selectedRobot].Deselect();
                        selectedRobot = -1;
                        IDC.SetDirections(new List<int>());
                    }
                }
            } else {
                if (selectedRobot != -1){
                    robots[selectedRobot].Deselect();
                    selectedRobot = -1;
                    IDC.SetDirections(new List<int>());
                }
            }
        }

    }

    public void Go(){
        bool go = false;
        for (int i = 0; i < robots.Length; i++){
            if (robots[i].instructions.Count > 0){
                go = true;
            }
        }
        
        if (go){
            for (int i = 0; i < robots.Length; i++){
                robots[i].Go();
            }
        }
    }

    public void Restart(){
        for (int i = 0; i < robots.Length; i++){
            robots[i].Restart();
        }
    }

    private void SelectRobot(int idx){
        for (int i = 0; i < robots.Length; i++){
            if (i == idx){
                robots[i].Select();
            } else {
                robots[i].Deselect();
            }
        }
    }
}
