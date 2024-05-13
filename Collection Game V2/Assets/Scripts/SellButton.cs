using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellButton : MonoBehaviour
{
    public Inventory inventory; // Reference to the Inventory object
    public string name;
    public int price; // Price to deduct from the inventory's coins
    public float flashDuration = 0.5f; // Duration of the flash effect in seconds
    public Color flashColor = Color.red; // Color to flash when there are not enough coins

    private Button button;
    private bool isFlashing = false;
    private Color originalColor;

    private void Start()
    {
        inventory = GameObject.FindGameObjectsWithTag("Inventory")[0].GetComponent<Inventory>();

        if (inventory.items[name]){
            button.interactable = false; // Disable the button
        } else {
            button = GetComponent<Button>(); // Get reference to the attached button component
            button.onClick.AddListener(OnClick); // Attach the OnClick method to the button's click event

            originalColor = button.image.color; // Store the original color of the button
        }
    }

    private void Update()
    {
        if (isFlashing)
        {
            float t = Mathf.PingPong(Time.time, flashDuration) / flashDuration; // Calculate the lerp parameter
            button.image.color = Color.Lerp(originalColor, flashColor, t); // Apply the lerped color to the button
        }
    }

    private void OnClick()
    {
        if (inventory.coins >= price)
        {
            inventory.coins -= price; // Deduct the price from the inventory's coins
            inventory.items[name] = true; // Deduct the price from the inventory's coins
            button.interactable = false; // Disable the button
        }
        else
        {
            // Debug.Log("Not enough coins!"); // Output a message if there are not enough coins
            StartCoroutine(FlashButton()); // Start flashing the button
        }
    }

    private System.Collections.IEnumerator FlashButton()
    {
        isFlashing = true; // Set the flag to start flashing the button

        yield return new WaitForSeconds(flashDuration); // Wait for the specified duration

        isFlashing = false; // Set the flag to stop flashing the button
        button.image.color = originalColor; // Restore the original color of the button
    }
}
