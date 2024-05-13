using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResidentAttributes : MonoBehaviour
{
    public string residentName;
    public string[] quotes;

    public bool move;
    public float speed;
    public bool dialogue;

    public Vector3 targetPos; // The starting position of the object
    private float targetPauseTime;
    private float pauseTime;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = new Vector3 (Random.Range(-3.75f, 3.75f), Random.Range(-3.75f, 2.75f), 0);
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (targetPos.x > transform.position.x){
            spriteRenderer.flipX = false;
        } else {
            spriteRenderer.flipX = true;
        }
        pauseTime = 100;
        targetPauseTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogue) {
            if (targetPauseTime < pauseTime) {
                transform.position -= (transform.position-targetPos) * speed * Time.deltaTime;
                if (Vector3.Distance(targetPos, transform.position) < 0.5f){
                    targetPos = new Vector3 (Random.Range(-3.75f, 3.75f), Random.Range(-3.75f, 2.75f), 0);
                    targetPauseTime = Random.Range(1, 5);
                    pauseTime = 1;
                    if (targetPos.x < transform.position.x){
                        spriteRenderer.flipX = false;
                    } else {
                        spriteRenderer.flipX = true;
                    }
                }
            } else {
                pauseTime += Time.deltaTime;
            }
        }
    }
}
