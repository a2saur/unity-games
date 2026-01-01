using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Attack attackEffect;
    private float attackDelay;
    
    private int lookingDir = 0;
    private int facingDir = 1;
    private float movingDir = 0;

    private Rigidbody2D rb;

    public InputActionReference playerControls;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movingDir = ctx.ReadValue<float>();

        if (movingDir < -0.5){
            facingDir = -1;
        } else if (movingDir > 0.5){
            facingDir = 1;
        }
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.started) {
            // started jump
            rb.AddForce(new Vector3(0, SettingsManager.initialJumpForce, 0));
        } else if (ctx.canceled) {
            // ended jump
            if (rb.linearVelocity.y > 0){
                SetYVel(rb.linearVelocity.y/2);
            }
        }
    }
    
    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && attackDelay <= 0){
            // attack!
            attackDelay = SettingsManager.maxAttackDelay;
            attackEffect.StartAttack(facingDir, lookingDir);
        }
    }

    // Update is called once per frame
    void Update()
    {
        attackDelay -= Time.deltaTime;

        // check that the player isn't falling too fast
        if (rb.linearVelocity.y < -SettingsManager.maxFallSpeed){
            SetYVel(-SettingsManager.maxFallSpeed);
        }

        if (rb.linearVelocity.y < 1){
            rb.gravityScale = 3;
        } else {
            rb.gravityScale = 1.5f;
        }

        // Move player
        SetXVel(SettingsManager.moveSpeed*movingDir);

        // TODO: Up & Down arrows
    }

    private void SetXVel(float xVel){
        rb.linearVelocity = new Vector2(xVel, rb.linearVelocity.y);
    }

    private void SetYVel(float yVel){
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, yVel);
    }
}
