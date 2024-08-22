using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject target;
    private GameObject minWall;
    private GameObject maxWall;
    private float buffer = 3f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        minWall = GameObject.FindGameObjectWithTag("MinWall");
        maxWall = GameObject.FindGameObjectWithTag("MaxWall");
    }

    // Update is called once per frame
    void Update()
    {
        if (target.transform.position.x <= minWall.transform.position.x + buffer){
            transform.position = new Vector3(minWall.transform.position.x+buffer, transform.position.y, transform.position.z);
        } else if (target.transform.position.x >= maxWall.transform.position.x - buffer){
            transform.position = new Vector3(maxWall.transform.position.x-buffer, transform.position.y, transform.position.z);
        } else {
            transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
        }

        if (Controller.transforming){
            GetComponent<Camera>().orthographicSize = 7.5f;
        } else {
            GetComponent<Camera>().orthographicSize = 3.5f;
        }
    }
}
