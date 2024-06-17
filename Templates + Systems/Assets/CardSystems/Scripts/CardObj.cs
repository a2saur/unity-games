using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardObj : MonoBehaviour
{
    // Reference to the UI element's RectTransform
    public RectTransform uiElement;

    // Reference to the Canvas's GraphicRaycaster
    public GraphicRaycaster graphicRaycaster;

    // Reference to the EventSystem
    public EventSystem eventSystem;

    void Start(){
        // Automatically assign the RectTransform of the UI element this script is attached to
        uiElement = gameObject.GetComponent<RectTransform>();

        // Find and assign the GraphicRaycaster from the Canvas
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        }
        else
        {
            Debug.LogError("Canvas not found in parent hierarchy.");
        }

        // Find and assign the EventSystem in the scene
        eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem == null)
        {
            Debug.LogError("EventSystem not found in the scene.");
        }
    }

    void Update()
    {
        if (IsMouseOverUI())
        {
            // Debug.Log("Mouse is over the UI element.");
            uiElement.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else
        {
            uiElement.localScale = new Vector3(1f, 1f, 1f);
            // Debug.Log("Mouse is not over the UI element.");
        }
    }

    // Method to check if the mouse is over the UI element
    private bool IsMouseOverUI()
    {
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;

        // Create a list to receive all results
        List<RaycastResult> results = new List<RaycastResult>();

        // Raycast using the GraphicRaycaster and pointer data
        graphicRaycaster.Raycast(pointerEventData, results);

        // Check if the UI element is in the results
        foreach (RaycastResult result in results)
        {
            if (result.gameObject == uiElement.gameObject)
            {
                return true;
            }
        }

        return false;
    }
}
