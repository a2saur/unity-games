using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Regular movement speed
    public float dashSpeed = 10f; // Dash speed
    public float dashDuration = 0.2f; // Duration of the dash
    public float dashCooldown = 1f; // Cooldown between dashes
    public float gravity = 20f; // Gravity for character's rigidbody

    private Rigidbody rb;
    private bool isDashing = false;
    private float lastDashTime = -1f;
    private Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    private void Update()
    {
        // Handle player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Handle dashing
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > lastDashTime + dashCooldown)
        {
            lastDashTime = Time.time;
            isDashing = true;
            StartCoroutine(Dash());
        }

        // Rotate the player to face the movement direction
        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }
    }

    private IEnumerator Dash()
    {
        float startTime = Time.time;
        float endTime = startTime + dashDuration;
        Vector3 dashDirection = moveDirection; // Dash in the current movement direction

        while (Time.time < endTime)
        {
            rb.velocity = dashDirection * dashSpeed;
            yield return null;
        }

        rb.velocity = Vector3.zero;
        isDashing = false;
    }

    private void FixedUpdate()
    {
        // Apply gravity when not dashing
        if (!isDashing)
        {
            rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
            rb.AddForce(Vector3.down * gravity, ForceMode.Force);
        }
    }
}
