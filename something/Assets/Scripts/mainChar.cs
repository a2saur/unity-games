// Assumes there is a SettingsManager (rightArrow, leftArrow, jumpButton, moveSpeed, jumpForce)
// Assumes a 2d rigidbody, 2d collider, an animator, and a ground block labeled "Ground"
// For the animator, a float "Anim" controls the animation (default direction is left)
    // 0  - idle
    // -1 - falling
    // 1  - right
    // 2  - left
    // 5  - dash
    // 6  - slash attack
    // 7  - spin attack

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainChar : MonoBehaviour
{
    public int health = 5;

    private bool isGrounded;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isDashing;
    private float dashCooldown = 0;
    private float dashTime;
    private bool isAttacking;
    private float attackCooldown = 0;
    private float attackTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Last ditch effort
        if (health == 0){
            SettingsManager.moveSpeed = SettingsManager.defaultMoveSpeed * 2;
        } else {
            SettingsManager.moveSpeed = SettingsManager.defaultMoveSpeed;
        }

        if (Input.GetKeyDown(SettingsManager.spinAttackButton) && !isAttacking && attackCooldown <= 0)
        {
            isAttacking = true;
            attackTime = SettingsManager.attackDuration; // Set the timer for the dash duration
            anim.SetInteger("Anim", 7);
        } else if (Input.GetKeyDown(SettingsManager.slashAttackButton) && !isAttacking && attackCooldown <= 0)
        {
            isAttacking = true;
            attackTime = SettingsManager.attackDuration; // Set the timer for the dash duration
            anim.SetInteger("Anim", 6);
        } 

        if (isAttacking){
            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                isAttacking = false;
                attackCooldown = SettingsManager.attackCooldown;
                anim.SetInteger("Anim", 0);
            }
        } else {
            if (attackCooldown > 0){
                attackCooldown -= Time.deltaTime;
            }
            
            if (Input.GetKeyDown(SettingsManager.dashButton) && !isDashing && dashCooldown <= 0)
            {
                isDashing = true;
                dashTime = SettingsManager.dashDuration; // Set the timer for the dash duration
                anim.SetInteger("Anim", 5);
            }

            if (isDashing)
            {
                dashTime -= Time.deltaTime;

                // Move the player at dash speed in the direction they are facing
                float dashDirection = transform.localScale.x > 0 ? -1 : 1; // -1 for left, 1 for right
                // rb.velocity = new Vector2(dashDirection * SettingsManager.moveSpeed * SettingsManager.dashSpeedMultiplier, rb.velocity.y);
                float dX = dashDirection * SettingsManager.moveSpeed * SettingsManager.dashSpeedMultiplier * Time.deltaTime;
                transform.position = new Vector3(transform.position.x+dX, transform.position.y, 0);

                // Stop dashing after the dash duration has passed
                if (dashTime <= 0)
                {
                    isDashing = false;
                    dashCooldown = SettingsManager.dashCooldown;
                    anim.SetInteger("Anim", 0);
                }
            } else {
                if (dashCooldown > 0){
                    dashCooldown -= Time.deltaTime;
                }
                if (Input.GetKey(SettingsManager.rightArrow)){
                    rb.velocity = new Vector2(SettingsManager.moveSpeed, rb.velocity.y);
                    transform.localScale = new Vector3(-1, 1, 1); // Flip
                    anim.SetInteger("Anim", 1);
                } else if (Input.GetKey(SettingsManager.leftArrow)){
                    rb.velocity = new Vector2(-SettingsManager.moveSpeed, rb.velocity.y);
                    transform.localScale = new Vector3(1, 1, 1); // Default
                    anim.SetInteger("Anim", 2);
                }

                // Handle jumping when up arrow is pressed and player is grounded
                if (Input.GetKeyDown(SettingsManager.jumpButton) && isGrounded)
                {
                    rb.velocity = new Vector2(rb.velocity.x, SettingsManager.jumpForce);
                    isGrounded = false;
                }
                
                
                // Falling?
                if (!isGrounded && rb.velocity.y < 0.1f)
                {
                    anim.SetInteger("Anim", -1);
                }
            }
        }
    }

    // Check if the player is grounded using collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetInteger("Anim", 0);
        }
        
        // if enemy and not attacking or dashing (invulnerable), take damage
        if (collision.gameObject.CompareTag("Projectile"))
        {
            if (isAttacking){
                // destroy enemy
                collision.gameObject.transform.localScale = new Vector3(0, 0, 0);
                collision.gameObject.GetComponent<Collider2D>().enabled = false;
            } else if (isDashing){
                // nothing happens
            } else {
                // ouch
                health--;
                collision.gameObject.transform.localScale = new Vector3(0, 0, 0);
                collision.gameObject.GetComponent<Collider2D>().enabled = false;
                // collision.gameObject.GetComponent<Projectile>().Deactivate();
            }
        }
    }
}
