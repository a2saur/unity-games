using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShow : MonoBehaviour
{
    public GameObject bookButton;
    public Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectsWithTag("Inventory")[0].GetComponent<Inventory>();
        if (inventory.chapter > 0) {
            bookButton.SetActive(true);
        } else {
            bookButton.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
