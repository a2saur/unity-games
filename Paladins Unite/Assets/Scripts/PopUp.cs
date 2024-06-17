using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpController : MonoBehaviour
{
    // Reference to the Text component of the popup
    public TMP_Text popupText;
    // Reference to the GameObject that represents the popup
    public GameObject popupObject;

    void Start()
    {
        // Ensure the popup is initially hidden
        popupObject.SetActive(false);
    }

    // Public function to display the popup with a message
    public void PopUp(string message)
    {
        // Set the popup text
        popupText.text = message;
        // Show the popup
        popupObject.SetActive(true);
        // Start the coroutine to hide the popup after 3 seconds
        StartCoroutine(HidePopupAfterDelay(3f));
    }

    // Coroutine to hide the popup after a delay
    private IEnumerator HidePopupAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);
        // Hide the popup
        popupObject.SetActive(false);
    }
}
