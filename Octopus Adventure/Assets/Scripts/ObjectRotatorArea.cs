using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotatorArea : MonoBehaviour
{
    public float rotationAmount;
    public Vector3 cameraPosition;

    public List<GameObject> toRotate = new List<GameObject>();
    public List<int> counts = new List<int>();

    public List<GameObject> toAntiRotate = new List<GameObject>();
    public List<int> antiCounts = new List<int>();

    public bool hasWall;
    public GameObject wall;

    private int iterations = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        List<int> removeIdx = new List<int>();

        for (int i = 0; i < toRotate.Count; i++){
            toRotate[i].transform.rotation = Quaternion.Euler(new Vector3(0, (rotationAmount/iterations)*(counts[i]), 0));
            if (counts[i] == iterations){
                // Remove
                removeIdx.Add(i);
            } else {
                counts[i]++;
            }
        }

        foreach (int i in removeIdx){
            toRotate.RemoveAt(i);
            counts.RemoveAt(i);
        }

        // Anti Rotation
        List<int> removeAntiIdx = new List<int>();

        for (int i = 0; i < toAntiRotate.Count; i++){
            toAntiRotate[i].transform.rotation = Quaternion.Euler(new Vector3(0, (rotationAmount/iterations)*(antiCounts[i]), 0));
            if (antiCounts[i] == 0){
                // Remove
                removeAntiIdx.Add(i);
            } else {
                antiCounts[i]--;
            }
        }

        for (int i = removeAntiIdx.Count-1; i > -1; i--){
            toAntiRotate.RemoveAt(i);
            antiCounts.RemoveAt(i);
        }
    }

    void OnTriggerEnter(Collider other){
        // Debug.Log(other.gameObject.tag);
        // other.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, rotationAmount, 0));
        toRotate.Add(other.gameObject);
        counts.Add(0);

        if (other.gameObject.tag == "CameraController" && cameraPosition.y != -100){
            other.gameObject.GetComponent<CameraController>().camPos = cameraPosition;
        }

        if (other.gameObject.tag == "Player" && hasWall){
            wall.GetComponent<Animator>().Play("Door (opening)");
        }
    }
    
    void OnTriggerExit(Collider other){
        // other.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        if (other.gameObject.tag != "CameraController"){
            toAntiRotate.Add(other.gameObject);
            antiCounts.Add(iterations);
        }

        if (other.gameObject.tag == "Player" && cameraPosition.y != -100){
            GameObject.FindWithTag("CameraController").GetComponent<CameraController>().camPos = new Vector3(0, -100, 0);
        }

        if (other.gameObject.tag == "Player" && hasWall){
            wall.GetComponent<Animator>().Play("Door (closing)");
        }
    }
}
