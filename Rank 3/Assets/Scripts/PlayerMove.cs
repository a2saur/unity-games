using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public GameObject gate;
    public GameObject strand;
    public string nextScene;

    private bool onGround = true;
    private Rigidbody2D rb;
    private Animator animator;
    private int anim = 0;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -7.5f){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (!strand.activeSelf){
            gate.GetComponent<SpriteRenderer>().color = new Color(0.25f, 0.8f, 1, 1);
        } else {
            gate.GetComponent<SpriteRenderer>().color = new Color(0.25f, 0.8f, 1, 0.5f);
        }

        animator.SetInteger("anim-mode", anim);
        if (!Controller.transforming){
            if (facingRight && transform.localScale.x < 0){
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            } if (!facingRight && transform.localScale.x > 0){
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
            if (Input.GetKey(Controller.rightArrow)){
                anim = 1;
                transform.position += new Vector3(Controller.moveSpeed*Time.deltaTime, 0, 0);
                facingRight = true;
            } else if (Input.GetKey(Controller.leftArrow)){
                anim = 1;
                transform.position -= new Vector3(Controller.moveSpeed*Time.deltaTime, 0, 0);
                facingRight = false;
            } else {
                anim = 0;
            } if (Input.GetKeyDown(Controller.jumpButton) && onGround){
                rb.AddForce(new Vector2(0, Controller.jumpSpeed), ForceMode2D.Impulse);
                onGround = false;
            }

            if (Input.GetKeyDown(Controller.interactButton)){
                GameObject[] tpoints = GameObject.FindGameObjectsWithTag("transformPoint");
                foreach (GameObject tp in tpoints){
                    if (Vector3.Distance(tp.transform.position, transform.position) < Controller.interactableDist){
                        Controller.transforming = true;
                        rb.velocity = new Vector2(0, 0);
                        rb.gravityScale = 0;
                    }
                }
            }

            if (rb.velocity.y < 0.01f && rb.velocity.y > -0.01f){
                onGround = true;
            }
        } else {
            anim = 2;
            // transforming
            if (Input.GetKeyDown(Controller.interactButton)){
                Controller.transforming = false;
                rb.gravityScale = 1;
            }

            GameObject[] tobjects = GameObject.FindGameObjectsWithTag("transformObject");
            foreach (GameObject to in tobjects){
                // 
            }
        }
    }

    void OnCollisionEnter2D(Collision2D obj){
        if (obj.gameObject == gate){
            if (!strand.activeSelf){
                SceneManager.LoadScene(nextScene);
            }
        }
    }
}
