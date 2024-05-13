using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryWait : MonoBehaviour
{
    public float storyWait;
    public Inventory inventoryObject;

    // Start is called before the first frame update
    void Start()
    {
        inventoryObject = GameObject.FindGameObjectsWithTag("Inventory")[0].GetComponent<Inventory>();
        if (inventoryObject.chapter > storyWait){
            this.gameObject.SetActive(true);
        } else {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
