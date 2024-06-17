using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    // Reference to the normal cursor texture
    public Texture2D normalCursorTexture;
    // Reference to the clicked cursor texture
    public Texture2D clickedCursorTexture;
    // Reference to the cursor's hotspot
    public Vector2 cursorHotspot = Vector2.zero;
    // Duration for which the clicked cursor will be shown
    public float clickedCursorDuration = 0.25f;

    void Start()
    {
        // Set the normal cursor at the start
        SetNormalCursor();
    }

    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Change the cursor to the clicked cursor
            SetClickedCursor();
            // Start the coroutine to reset the cursor after the specified duration
            StartCoroutine(ResetCursorAfterDelay());
        }
    }

    // Method to set the normal cursor
    public void SetNormalCursor()
    {
        Cursor.SetCursor(normalCursorTexture, cursorHotspot, CursorMode.Auto);
    }

    // Method to set the clicked cursor
    public void SetClickedCursor()
    {
        Cursor.SetCursor(clickedCursorTexture, cursorHotspot, CursorMode.Auto);
    }

    // Coroutine to reset the cursor after the specified delay
    private IEnumerator ResetCursorAfterDelay()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(clickedCursorDuration);
        // Reset the cursor to the normal one
        SetNormalCursor();
    }
}
