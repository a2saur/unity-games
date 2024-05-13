using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChar : MonoBehaviour
{
    public float moveSpeed;

    private Animator animator;

    // public SettingsManager SetMan;
    void Start()
    {
        // SetMan = GameObject.FindWithTag("SettingsManager").GetComponent<SettingsManager>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!SettingsManager.pauseScreen){
            bool moving = false;
            if (Input.GetKey(SettingsManager.keyLabels["rightArrow"])) {
                transform.position += new Vector3(moveSpeed*Time.deltaTime, 0, 0);
                animator.SetInteger("Direction", 1);
                moving = true;
            } if (Input.GetKey(SettingsManager.keyLabels["leftArrow"])) {
                transform.position += new Vector3(-moveSpeed*Time.deltaTime, 0, 0);
                animator.SetInteger("Direction", 2);
                moving = true;
            } if (Input.GetKey(SettingsManager.keyLabels["downArrow"])) {
                transform.position += new Vector3(0, -moveSpeed*Time.deltaTime, 0);
                animator.SetInteger("Direction", 3);
                moving = true;
            } if (Input.GetKey(SettingsManager.keyLabels["upArrow"])) {
                transform.position += new Vector3(0, moveSpeed*Time.deltaTime, 0);
                animator.SetInteger("Direction", 4);
                moving = true;
            } 
            
            if (!moving) {
                animator.SetInteger("Direction", 0);
            }
        }
    }
}
