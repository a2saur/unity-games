using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChar : MonoBehaviour
{
    public Rigidbody2D rb;

    private Vector2 movement;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(SettingsManager.pauseButton) && SettingsManager.playing) {
            SettingsManager.CallPauseMenu();
        }
        
        if (SettingsManager.playing){
            // ---GETTING WASD MOVEMENT
            movement = Vector2.zero;

            // Get input from specific keys using if statements
            if (Input.GetKey(SettingsManager.upArrow)) {
                movement.y = 1;
            } if (Input.GetKey(SettingsManager.downArrow)) {
                movement.y = -1;
            }

            if (Input.GetKey(SettingsManager.rightArrow)) {
                movement.x = 1;
            } if (Input.GetKey(SettingsManager.leftArrow)) {
                movement.x = -1;
            }

            movement.Normalize(); // Prevent faster diagonal movement

        }
    }

    void FixedUpdate()
    {
        if (SettingsManager.playing){
            // Move the player
            rb.linearVelocity = movement * SettingsManager.moveSpeed;
        }
    }

    // Check if the player is grounded using collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if (collision.gameObject.CompareTag("Ground"))
        // {
        //     isGrounded = true;
        //     anim.SetInteger("Anim", 0);
        // }
    }
}
