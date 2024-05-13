using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparkles : MonoBehaviour
{
    // Public variables
    public Transform mainChar;
    public Animator animator;
    private SpriteRenderer sparkleRenderer;

    // Start is called before the first frame update
    void Start()
    {
        sparkleRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(mainChar.transform.position.x, mainChar.transform.position.y, mainChar.transform.position.z-0.5f);
        sparkleRenderer.flipX = mainChar.GetComponent<SpriteRenderer>().flipX;
        if (Input.GetKey(KeyCode.F)) {
            // animator.SetBool("Sparkles", true);
            animator.Play("Sparkle");
        }
    }
}
