using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InstructionDisplayController : MonoBehaviour
{
    public GameObject[] directionSpots;
    public Sprite[] images;
    public Sprite[] highlightedImages;
    public List<int> currentInstructions;
    public int currentHighlighted = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < directionSpots.Length; i++){
            directionSpots[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDirections(List<int> instructions){
        for (int i = 0; i < instructions.Count; i++){
            directionSpots[i].SetActive(true);
            directionSpots[i].GetComponent<Image>().sprite = images[instructions[i]];
        } for (int i = instructions.Count; i < directionSpots.Length; i++){
            directionSpots[i].SetActive(false);
        }

        currentHighlighted = -1;
        currentInstructions = instructions;
    }

    public void SetHighlight(int highlight){
        if (currentHighlighted != -1){
            directionSpots[currentHighlighted].GetComponent<Image>().sprite = images[currentInstructions[currentHighlighted]];
        }

        if (highlight != -1){
            directionSpots[highlight].GetComponent<Image>().sprite = highlightedImages[currentInstructions[highlight]];
            currentHighlighted = highlight;
        }
    }
}
