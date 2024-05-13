using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffect : MonoBehaviour
{
    public string effect;
    
    public float blinkInterval = 0.5f; // Time interval between each blink (in seconds)
    
    private TextMeshProUGUI textComponent;
    private float timer;
    private bool isVisible;

    private void Start()
    {
        if (effect == "blink"){
            textComponent = GetComponent<TextMeshProUGUI>();
            timer = 0f;
            isVisible = true;
        }
    }

    private void Update()
    {
        if (effect == "blink"){
            timer += Time.deltaTime;

            if (timer >= blinkInterval)
            {
                timer = 0f;
                isVisible = !isVisible;
                textComponent.enabled = isVisible;
            }
        }
    }
}