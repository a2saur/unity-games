using UnityEngine;

public class EnemyDiamond : MonoBehaviour
{
    public float jumpForce = 5f;        // the force applied to make the enemy jump
    public float flipSpeed = 5f;        // the speed at which the enemy flips
    public float jumpInterval = 2f;     // the time interval between each jump
    public Transform character; // the gameobject the enemy needs to touch to be destroyed
    public Animator anim;

    private Rigidbody2D rb;             // the rigidbody component of the enemy
    private bool isJumping = false;     // flag to check if the enemy is jumping
    private bool isFlippingForward = true; // flag to check if the enemy is flipping forward
    private float jumpTimer = 0f;       // timer to count the time between each jump

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   // get the rigidbody component of the enemy
    }

    void Update()
    {
        if (!isJumping)
        {
            // count the time between each jump
            jumpTimer += Time.deltaTime;
            // check if the time interval has passed to make the enemy jump again
            if (jumpTimer >= jumpInterval)
            {
                isJumping = true;
                jumpTimer = 0f;
                // apply a vertical force to make the enemy jump
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        else
        {
            // apply a torque force to make the enemy flip forward or backward, depending on the flag
            if (isFlippingForward)
            {
                rb.AddTorque(flipSpeed * Time.deltaTime);
            }
            else
            {
                rb.AddTorque(-flipSpeed * Time.deltaTime);
            }
        }

        // check if the enemy has collided with the sparkles object
        if (Vector2.Distance(transform.position, character.transform.position) < 2.0f)
        {
            // Then, check if the "sparkle" animation is currently playing
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sparkle"))
            {
                // The "sparkle" animation is playing
                Debug.Log("The object is currently playing the sparkle animation");
                Destroy(gameObject);
            }
            // if so, destroy the enemy
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            // reset the flag and the rigidbody's rotation when the enemy hits the ground
            isJumping = false;
            rb.angularVelocity = 0f;
            rb.rotation = 0f;
            // swap the flipping direction flag
            isFlippingForward = !isFlippingForward;
        }
    }
}
