using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Attack attackEffect;
    private float attackDelay;

    private float maxJump = 0.2f;
    private float curJumpDur = -1;

    private int lookingDir = 0;
    private int facingDir = 1;
    private int movingDir = 0;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(SettingsManager.attackButton) && attackDelay <= 0){
            attackDelay = SettingsManager.maxAttackDelay;
            attackEffect.StartAttack(facingDir, lookingDir);
        } else {
            attackDelay -= Time.deltaTime;
        }


        /* --- Jumping --- */
        if (Input.GetKeyDown(SettingsManager.jumpButton)){
            rb.AddForce(new Vector3(0, SettingsManager.initialJumpForce, 0));
        }

        if (Input.GetKeyUp(SettingsManager.jumpButton) && rb.linearVelocity.y > 0){
            SetYVel(rb.linearVelocity.y/2);
        }

        // check that the player isn't falling too fast
        if (rb.linearVelocity.y < -SettingsManager.maxFallSpeed){
            SetYVel(-SettingsManager.maxFallSpeed);
        }

        if (rb.linearVelocity.y < 1){
            rb.gravityScale = 3;
        } else {
            rb.gravityScale = 1.5f;
        }

        /* --- Moving --- */
        // Left & right
        if (Input.GetKeyDown(SettingsManager.rightButton)) {
            movingDir = 1;
            facingDir = 1;
        } if (Input.GetKeyDown(SettingsManager.leftButton)) {
            movingDir = -1;
            facingDir = -1;
        } if (Input.GetKeyUp(SettingsManager.rightButton) && movingDir == 1){
            movingDir = 0;
        } if (Input.GetKeyUp(SettingsManager.leftButton) && movingDir == -1){
            movingDir = 0;
        }
        SetXVel(SettingsManager.moveSpeed*movingDir);

        // Up & Down
        if (Input.GetKeyDown(SettingsManager.upButton)) {
            lookingDir = 1;
        } if (Input.GetKeyDown(SettingsManager.downButton)) {
            lookingDir = -1;
        } if (Input.GetKeyUp(SettingsManager.upButton) && lookingDir == 1){
            lookingDir = 0;
        } if (Input.GetKeyUp(SettingsManager.downButton) && lookingDir == -1){
            lookingDir = 0;
        }
    }

    private void SetXVel(float xVel){
        rb.linearVelocity = new Vector2(xVel, rb.linearVelocity.y);
    }

    private void SetYVel(float yVel){
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, yVel);
    }
}
