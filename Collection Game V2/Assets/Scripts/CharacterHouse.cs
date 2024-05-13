using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterHouse : MonoBehaviour
{
    public float moveSpeed = 5f;

    public GameObject residentObject;
    public ResidentAttributes resident;
    public GameObject dialogueBox;

    private Animator animator;

    private bool dialogue;

    public Animator transitionAnimator;
    // Start is called before the first frame update
    void Start()
    {
        transitionAnimator = GameObject.FindGameObjectsWithTag("SceneTransition")[0].GetComponent<Animator>();
        animator = GetComponent<Animator>();
        residentObject = GameObject.FindGameObjectsWithTag("Resident")[0];
        resident = residentObject.GetComponent<ResidentAttributes>();
    }

    private void Update()
    {
        if (!dialogue){
            if (Input.GetKeyDown(KeyCode.Space) && Vector3.Distance(residentObject.transform.position, transform.position) < 1.5f){
                dialogueBox.SetActive(true);
                dialogueBox.GetComponent<Dialogue>().lines = resident.quotes[Random.Range(0, resident.quotes.Length)].Split("|");
                dialogueBox.GetComponent<Dialogue>().StartDialogue();
                
                dialogue = true;
                resident.dialogue = true;
            }
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            
            Vector3 movement = new Vector3(horizontalInput, verticalInput, 0).normalized;
            transform.position += movement * moveSpeed * Time.deltaTime;
            if (transform.position.x < -3.75f) {
                transform.position = new Vector3(-3.75f, transform.position.y, 0);
            } else if (transform.position.x > 3.75f) {
                transform.position = new Vector3(3.75f, transform.position.y, 0);
            }
            if (transform.position.y > 3.75f) {
                transform.position = new Vector3(transform.position.x, 3.75f, 0);
            } else if (transform.position.y < -2.75f) {
                transitionAnimator.SetTrigger("SceneTransition");
                StartCoroutine(DelayedSceneChange(1f, "TownTest"));
                // SceneManager.LoadScene("TownTest");
            }

            if (movement.magnitude > 0)
            {
                if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
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
        } else {
            if (dialogueBox.GetComponent<Dialogue>().running == false){
                dialogue = false;
                resident.dialogue = false;
            }
        }
    }

    IEnumerator DelayedSceneChange(float delay, string sceneToLoad){
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(sceneToLoad);
    }
}