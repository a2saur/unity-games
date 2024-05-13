using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTown : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    public float minX = -10000;
    public float maxX = 10000;

    public float minY = -10000;
    public float maxY = 10000;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0).normalized;
        transform.position += movement * moveSpeed * Time.deltaTime;
        if (transform.position.x > maxX || transform.position.x < minX || transform.position.y > maxY || transform.position.y < minY){
            transform.position -= movement * moveSpeed * Time.deltaTime;
        }

        if (movement.magnitude > 0)
        {
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                if (movement.x > 0)
                {
                    // Right direction
                    animator.SetInteger("Direction", 2);
                }
                else
                {
                    // Left direction
                    animator.SetInteger("Direction", 4);
                }
            }
            else
            {
                if (movement.y > 0)
                {
                    // Up direction
                    animator.SetInteger("Direction", 1);
                }
                else
                {
                    // Down direction
                    animator.SetInteger("Direction", 3);
                }
            }
        } else {
            animator.SetInteger("Direction", 0);
        }
    }
}