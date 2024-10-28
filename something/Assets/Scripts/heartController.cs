using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heartController : MonoBehaviour
{
    public int heart;
    public Sprite[] heartFullFrames;
    public Sprite[] heartEmptyFrames;
    public Sprite[] heartLDFrames;
    public float frameRate = 0.1f; // Time per frame (adjust for desired speed)

    public mainChar mc;

    private Image uiImage;
    private int currentFrame;
    private float timer;

    void Start(){
        uiImage = GetComponent<Image>();
        mc = GameObject.FindWithTag("Player").GetComponent<mainChar>();
    }

    void Update()
    {
        // Update the timer by time passed since the last frame
        timer += Time.deltaTime;

        // If the timer exceeds the frameRate, swap to the next frame
        if (timer >= frameRate)
        {
            timer -= frameRate; // Reset timer
            if (heart <= mc.health){
                currentFrame = (currentFrame + 1) % heartFullFrames.Length; // Loop through frames
                uiImage.sprite = heartFullFrames[currentFrame]; // Set the new frame
                uiImage.color = new Color(1, 1, 1, 1);
            } else if (mc.health > 0){
                currentFrame = (currentFrame + 1) % heartEmptyFrames.Length; // Loop through frames
                uiImage.sprite = heartEmptyFrames[currentFrame]; // Set the new frame
                uiImage.color = new Color(0.3f, 0.3f, 0.3f, 1);
            } else if (mc.health == 0){
                currentFrame = (currentFrame + 1) % heartLDFrames.Length; // Loop through frames
                uiImage.sprite = heartLDFrames[currentFrame]; // Set the new frame
                uiImage.color = new Color(1, 1, 1, 1);
            } else {
                uiImage.sprite = heartEmptyFrames[0];
                uiImage.color = new Color(1, 1, 1, 1);
            }
        }
    }
}
