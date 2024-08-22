using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strand : MonoBehaviour
{
    public int strandNum;

    // Start is called before the first frame update
    void Start()
    {
        if (Controller.numStrands >= strandNum){
            // already collected
            GetComponent<SpriteRenderer>().color = new Color(0.25f,1,0.75f,0.5f); // cyan
        } else {
            GetComponent<SpriteRenderer>().color = new Color(0.25f,1,0.75f,1); // cyan
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D obj){
        if (obj.gameObject.tag == "Player"){
            // collect
            if (Controller.numStrands < strandNum){
                Controller.numStrands += 1;
            }
            gameObject.SetActive(false);
        }
    }
}
