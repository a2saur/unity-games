using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public GameObject dialogueBox;
    public Inventory inventory;

    public Animator transitionAnimator;
    // Start is called before the first frame update
    void Start()
    {
        transitionAnimator = GameObject.FindGameObjectsWithTag("SceneTransition")[0].GetComponent<Animator>();
        inventory = GameObject.FindGameObjectsWithTag("Inventory")[0].GetComponent<Inventory>();
        if (inventory.chapter == 2) {
            inventory.chapter = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.chapter == 0) {
            dialogueBox.GetComponent<Dialogue>().StopDialogue();
            dialogueBox.SetActive(true);
            dialogueBox.GetComponent<Dialogue>().lines = new string[] {"Hello!", "Haven\'t seen you here before!", "Nice to meet you! My name is Kotu", "Well, let me explain how things work;", "There are various creatures around here, which you can collect!", "You can collect creatures with a net (press spacebar)!", "I\'ve given you one now", "If you bring creatures to me, you can sell them to earn some coins!", "I have useful items to aid you on your travels (which of course cost some coins)", "How about this?", "Go out and find 1 creature, and then bring it back here and I\'ll give you something useful!"};
            dialogueBox.GetComponent<Dialogue>().StartDialogue();
            inventory.chapter = 0.5f;

        } if (inventory.chapter == 0.5f) {
            if (!dialogueBox.activeSelf) {
                inventory.chapter = 1;
                inventory.instructionsGiven = false;
            }
        } if (inventory.chapter == 1 && inventory.inventory.Count > 0) {
            dialogueBox.GetComponent<Dialogue>().StopDialogue();
            dialogueBox.SetActive(true);
            dialogueBox.GetComponent<Dialogue>().lines = new string[] {"Ah, I see you already have a creature!", "And here's a useful item for you: a map!", "I think every good adventurer should have a map, so as to not get lost.", "Well, actually...", "Now that I think about it, it\'s a bit tedious to just carry around a map...", "Ah, I know!", "Here\'s a book!", "Actually, a *magical* book", "It will keep track of what creatures you\'ve discovered,", "allow you to look at your inventory,", "save your progress,", "and of course, I put the map in there (along with a little note in case you forget)!"};
            dialogueBox.GetComponent<Dialogue>().StartDialogue();
            inventory.chapter = 1.5f;
        } if (inventory.chapter == 1.5f) {
            if (!dialogueBox.activeSelf) {
                inventory.chapter = 2;
                inventory.instructionsGiven = false;
            }
        } if (inventory.chapter == 3) {
            dialogueBox.GetComponent<Dialogue>().StopDialogue();
            dialogueBox.SetActive(true);
            if (inventory.food > 0){
                dialogueBox.GetComponent<Dialogue>().lines = new string[] {"Hello again!", "Oh! Do you have food?", "Smells good!", "You can use it to lure creatures that are far away from you", "To drop some food, you can press F or Shift", "Although, once the food is on the ground, you can't move it"};
            } else {
                dialogueBox.GetComponent<Dialogue>().lines = new string[] {"Hello again!", "Oh, another thing I forgot to mention", "If you have food, you can use it to lure creatures that are far away from you", "To drop some food, you can press F or Shift", "You should find some snacks and try it out!", "I don\'t sell food here, but you could probably find someplace that does!"};
            }
            dialogueBox.GetComponent<Dialogue>().StartDialogue();
            inventory.chapter = 3.5f;
        } if (inventory.chapter == 3.5f) {
            if (!dialogueBox.activeSelf) {
                inventory.chapter = 4;
            }
        }
    }

    public void Advice(){
        if (inventory.chapter == 1) {
            dialogueBox.GetComponent<Dialogue>().StopDialogue();
            dialogueBox.SetActive(true);
            dialogueBox.GetComponent<Dialogue>().lines = new string[] {"You should try to catch a creature!", "You can use the net with the spacebar to catch creatures", "If you catch one, then come back here and I\'ll give you something useful!"};
            dialogueBox.GetComponent<Dialogue>().StartDialogue();
        } else if (inventory.chapter == 2) {
            dialogueBox.GetComponent<Dialogue>().StopDialogue();
            dialogueBox.SetActive(true);
            dialogueBox.GetComponent<Dialogue>().lines = new string[] {"If you have food, you can use it to lure creatures that are far away from you", "To drop some food, you can press F or Shift", "You should find some snacks and try it out!", "I don\'t sell food here, but you could probably find someplace that does!"};
            dialogueBox.GetComponent<Dialogue>().StartDialogue();
            inventory.chapter = 3.5f;
        } else {
            dialogueBox.GetComponent<Dialogue>().StopDialogue();
            dialogueBox.SetActive(true);
            if (inventory.advice == 0){
                dialogueBox.GetComponent<Dialogue>().lines = new string[] {"Try out using food to catch creatures", "When you have food, you can drop some by pressing F or Shift", "There should be a food shop somewhere...", "Sorry, I don\'t know exactly where it is"};
                inventory.advice = 1;
            } else if (inventory.advice == 1){
                dialogueBox.GetComponent<Dialogue>().lines = new string[] {"Catch some more creatures!", "Once you have a bunch, you can sell them and buy something from me", "A snorkel allows you to go into the water,", "Wings will let you fast travel to towns,", "That\'s all I have at the moment"};
                inventory.advice = 2;
            } else if (inventory.advice == 2){
                dialogueBox.GetComponent<Dialogue>().lines = new string[] {"If you see gray spots marked by a star, try going to it!", "It\'ll take you to a cave, where you might find new creatures"};
                inventory.advice = 3;
            } else if (inventory.advice == 3){
                dialogueBox.GetComponent<Dialogue>().lines = new string[] {"Explore!", "You can see what creatures you\'ve found in your book!"};
                inventory.advice = 0;
            }
            dialogueBox.GetComponent<Dialogue>().StartDialogue();
        }
    }

    public void Goodbye()
    {
        transitionAnimator.SetTrigger("SceneTransition");
        StartCoroutine(DelayedSceneChange(1f, "TownTest"));
        // SceneManager.LoadScene("TownTest");
    }

    public void WhoAreYou()
    {
        dialogueBox.SetActive(true);
        dialogueBox.GetComponent<Dialogue>().lines = new string[] {"I am the shopkeeper", "You can call me Kotu", "I will buy any creatures you find", "We also have items in stock that may aid your travels"};
        dialogueBox.GetComponent<Dialogue>().StartDialogue();
    }

    public void WhatNext()
    {
        // 
    }

    IEnumerator DelayedSceneChange(float delay, string sceneToLoad){
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(sceneToLoad);
    }
}
