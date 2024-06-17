using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject roof;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            roof.SetActive(false);
        }
    }
    
    void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            roof.SetActive(true);
            // wall.GetComponent<Animator>().Play("Door (opening)");
        }
    }
}
