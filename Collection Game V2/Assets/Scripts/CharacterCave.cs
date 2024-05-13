using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CharacterCave : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpVal = 5f;

    public ParticleSystem[] particleEffects;
    private Rigidbody2D rigidbody;
    private Animator animator;

    public Animator transitionAnimator;
    // Start is called before the first frame update
    void Start()
    {
        transitionAnimator = GameObject.FindGameObjectsWithTag("SceneTransition")[0].GetComponent<Animator>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        if (transform.position.x < -43) {
            transitionAnimator.SetTrigger("SceneTransition");
            StartCoroutine(DelayedSceneChange(1f, "MainScene"));
            // SceneManager.LoadScene("MainScene");
        }
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        
        Vector3 movement = new Vector3(horizontalInput, 0, 0).normalized;
        transform.position += movement * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpVal, 0);
            // Play all particle effects in the array
            foreach (ParticleSystem effect in particleEffects)
            {
                effect.transform.position = transform.position;
                effect.Play();
            }
        }

        // Determine the direction based on the input values
        if (movement.magnitude > 0)
        {
            if (Mathf.Abs(movement.x) != 0)
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

    // void OnCollisionEnter2D(Collision2D hit){
    //     if (hit.gameObject.tag == "Creature") {
    //         // Debug.Log(hit.gameObject.GetComponent<CreatureAttributes>().creatureName);
    //         // inventoryObject.GetComponent<Inventory>().inventory.Add(hit.gameObject.GetComponent<CreatureAttributes>().creatureName);
    //         // inventoryObject.GetComponent<Inventory>().catalog[hit.gameObject.GetComponent<CreatureAttributes>().creatureName] = true;
    //         Destroy(hit.gameObject);
    //     }
    // }

    IEnumerator DelayedSceneChange(float delay, string sceneToLoad){
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(sceneToLoad);
    }
}