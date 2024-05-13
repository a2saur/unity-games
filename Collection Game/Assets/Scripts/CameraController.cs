using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    
    // void Start()
    // {
    //     DontDestroyOnLoad(this.gameObject);
    // }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = targetPosition;
    }
}
