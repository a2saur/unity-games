using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Dropper : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        if (-2.15f < mousePos.x && mousePos.x < 2.15f){
            transform.position = new Vector3(mousePos.x, transform.position.y, transform.position.z);
        } else if (mousePos.x > 2.15f) {
            transform.position = new Vector3(2.15f, transform.position.y, transform.position.z);
        } else if (mousePos.x < -2.15f) {
            transform.position = new Vector3(-2.15f, transform.position.y, transform.position.z);
        }
        // Debug.Log(mousePos.x);
        // Debug.Log(mousePos.y);
    }
}