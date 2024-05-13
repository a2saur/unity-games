using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public GameObject projectileObject;
    public float speed = 3f; // speed of enemy movement
    public float moveDistance = 3f; // distance the enemy will move left and right
    public float moveTime = 2f; // time the enemy will spend moving left and right
    public Transform character; // the gameobject the enemy needs to touch to be destroyed
    public Animator anim;
    public int health = 3;

    private float moveTimer = 0f; // timer for left-right movement
    private bool movingRight = false; // flag to determine direction of movement
    private bool isInvincible = false; // flag to determine if the enemy is currently invincible
    private float invincibilityTime = 1.0f; // duration of invincibility after being hit
    private float invincibilityTimer = 0f; // timer for invincibility

    private void Update()
    {
        if (isInvincible)
        {
            // Blink the enemy to show that it's invincible
            // Here, you can change the enemy's material color or use other visual effects to show that it's invincible
            float alpha = Mathf.PingPong(Time.time * 10f, 1f);
            GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, alpha);

            invincibilityTimer += Time.deltaTime;
            if (invincibilityTimer >= invincibilityTime)
            {
                isInvincible = false;
                invincibilityTimer = 0f;
                GetComponent<Renderer>().material.color = Color.white;
            }
        }

        // move the enemy left or right
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveTime)
        {
            movingRight = !movingRight;
            moveTimer = 0f;
        }

        float moveDirection = movingRight ? 1f : -1f;
        transform.Translate(Vector2.right * moveDirection * speed * Time.deltaTime);

        // check if the enemy has collided with the sparkles object
        if (Vector2.Distance(transform.position, character.transform.position) < 2.0f && !isInvincible)
        {
            // Then, check if the "sparkle" animation is currently playing
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sparkle"))
            {
                // The "sparkle" animation is playing
                // Debug.Log("The object is currently playing the sparkle animation");
                // Destroy(gameObject);
                health--;
                if (health <= 0){
                    Destroy(projectileObject);
                    Destroy(gameObject);
                }
                else
                {
                    isInvincible = true;
                }
            }
            // if so, destroy the enemy
        }
    }
}
