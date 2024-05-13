using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed = 5.0f;
    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get the horizontal and vertical input values
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move the character based on input
        transform.position += new Vector3(horizontalInput, verticalInput, 0) * speed * Time.deltaTime;

        // Set the animator parameter based on speed
        animator.SetFloat("Movement", horizontalInput);
    }
}