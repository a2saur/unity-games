using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Public variables
    public float SPEED = 4f; 
    public int MAXJUMP = 1;
    public Animator animator;

    public Image[] hearts; // The list of heart images
    public int maxHealth; // The maximum health the player can have
    public int currentHealth; // The current health of the player
    public int invincibleCountdown;

    // Private variables
    private int jumpCount = 0;
    private Rigidbody2D rb;
    private float horizontalMovement;
    private SpriteRenderer charRenderer;


    // Start is called before the first frame update
    void Start() {
        invincibleCountdown = 0;
        maxHealth = hearts.Length;
        currentHealth = maxHealth;
        // Initialize rigidbody
        rb = gameObject.GetComponent<Rigidbody2D>();
        charRenderer = GetComponent<SpriteRenderer>();
        charRenderer.flipX = false;
        rb.angularVelocity = -1500;
    }

    // Update is called once per frame
    void Update() {
        // Moving
        Vector3 pos = transform.position;

        // Move right
        if (Input.GetKey(KeyCode.RightArrow)) {
            pos.x += SPEED * Time.deltaTime;
            // gameObject.transform.localScale = new Vector3(-0.1f, 0.1f, 1f);
            charRenderer.flipX = false;
            horizontalMovement = SPEED;
        } 

        // Move left
        if (Input.GetKey(KeyCode.LeftArrow)) {
            pos.x -= SPEED * Time.deltaTime;
            // gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
            charRenderer.flipX = true;
            horizontalMovement = -SPEED;
        }

        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)){
            horizontalMovement = 0;
        }

        // Check for invincibility
        if (invincibleCountdown > 0) {
            invincibleCountdown -= Mathf.RoundToInt(Time.deltaTime * 60); // Reduce by 1 per second
            charRenderer.enabled = !charRenderer.enabled; // Toggle sprite renderer visibility to create blinking effect
        } else {
            charRenderer.enabled = true; // Make sure sprite renderer is visible if invincible period is over
        }

        // Set position
        transform.position = pos;

        // Set animation speed
        animator.SetFloat("speed", Mathf.Abs(horizontalMovement));
    }

    void FixedUpdate() {
        // Jumping
        if (Input.GetKey(KeyCode.Space)){
            if (jumpCount < MAXJUMP){
                rb.AddForce(Vector2.up * SPEED * 100);
                jumpCount++;
            }
        }
    }

    void OnCollisionEnter2D (Collision2D hit) {
        // Reset jump count if hit floor
        if (hit.gameObject.tag == "Floor") {
            jumpCount = 0;
            // if (transform.rotation.z > 25 || transform.rotation.z < -25 || rb.angularVelocity > 25 || rb.angularVelocity < -25)
            transform.rotation = Quaternion.identity;
            rb.angularVelocity = 0;
        } else if (hit.gameObject.tag == "Enemy") {
            if (invincibleCountdown == 0) {
                TakeDamage(1); // Or however much damage you want the character to take
                invincibleCountdown = 5; // Or however long you want the invincible period to last (in seconds)
            }
        }
    }

    void UpdateHealthBar()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].enabled = true; // Show the heart image
            }
            else
            {
                hearts[i].enabled = false; // Hide the heart image
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Make sure health doesn't go below 0 or above maxHealth
        UpdateHealthBar();
    }
}