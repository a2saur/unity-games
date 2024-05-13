using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // public Camera cameraTarget;
    public GameObject cameraTarget;
    public float offset;

    public GameObject focusTarget;
    public float yOffset;

    public Vector3 camPos = new Vector3 (0, -100, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (camPos.y == -100){
            transform.position = focusTarget.transform.position;
        } else {
            transform.position = camPos; // will need to add smoothing
        }

        // Camera.main.transform.rotation = Quaternion.Euler(transform.localRotation.eulerAngles);
        // Camera.main.transform.position = new Vector3(transform.position.x+(Mathf.Sin(transform.localRotation.eulerAngles.y)*offset), transform.position.y, transform.position.z+(Mathf.Cos(transform.localRotation.eulerAngles.y)*offset));
        
        cameraTarget.transform.rotation = Quaternion.Euler(transform.localRotation.eulerAngles);
        float angle = transform.localRotation.eulerAngles.y * Mathf.Deg2Rad;
        if (angle == 0){
            cameraTarget.transform.position = transform.position + new Vector3 (0, yOffset, offset);
        } else {
            cameraTarget.transform.position = new Vector3(transform.position.x+(Mathf.Sin(angle)*offset), transform.position.y+yOffset, transform.position.z+(Mathf.Cos(angle)*offset));
        }
    }
}
