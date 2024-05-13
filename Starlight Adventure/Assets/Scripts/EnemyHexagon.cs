using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHexagon : MonoBehaviour
{
    public float speed = 3f; // speed of enemy movement
    public float moveDistance = 3f; // distance the enemy will move left and right
    public float moveTime = 2f; // time the enemy will spend moving left and right
    public Transform character; // the gameobject the enemy needs to touch to be destroyed
    public Animator anim;

    private float moveTimer = 0f; // timer for left-right movement
    private bool movingRight = false; // flag to determine direction of movement

    private void Update()
    {
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
}
