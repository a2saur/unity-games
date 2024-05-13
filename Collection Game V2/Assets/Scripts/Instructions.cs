using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Instructions : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public bool running;
    private int index;
    public Inventory inventoryObject;

    // Start is called before the first frame update
    void Start()
    {
        inventoryObject = GameObject.FindGameObjectsWithTag("Inventory")[0].GetComponent<Inventory>();
        if (!inventoryObject.instructionsGiven){
            if (inventoryObject.chapter == 0){
                lines = new string[] {"Welcome!\nTo move, use the arrow keys\n\nIf you are talking to someone, press space or click the screen to continue!","For now, you should probably find a shop, or a town at least"};
            } if (inventoryObject.chapter == 1){
                lines = new string[] {"You got a net!\nPress SPACEBAR to use it", "If you catch some creatures, try bringing them to the shop!"}; // You got a net
            } else if (inventoryObject.chapter >= 2){
                lines = new string[] {"You got a book!\nPress ESCAPE or the BOOK ICON to open it up", "In your book there is...\nPage 1: Information and a map\nPage 2: View your inventory, use wings (if you have them), save, or travel to a new island\nPage 3+: Information on the catalog of creatures"}; // You got a book and map
            }
            StartDialogue();
        } else {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
    }

    public void StartDialogue()
    {
        index = 0;
        running = true;
        textComponent.text = lines[index];
        Time.timeScale = 0f;
        Debug.Log("PAUSED");
    }

    void NextLine()
    {
        if (index < lines.Length -1) {
            index++;
            textComponent.text = lines[index];
        } else {
            running = false;
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            inventoryObject.instructionsGiven = true;
        }
    }
}
