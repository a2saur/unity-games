using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObject : MonoBehaviour
{
    public float interactionDistance = 2f; // Distance at which the object can be grabbed
    public Material highlightMaterial; // Material to highlight the object
    private Material originalMaterial; // Original material of the object
    private Renderer objectRenderer; // Renderer component of the object
    private Transform originalParent; // The original parent of the object
    private Rigidbody rb; // Rigidbody component of the object
    private bool holding;

    private void Start()
    {
        holding = false;
        // Cache the original material, get the renderer component, and store the original parent
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
        originalParent = transform.parent;

        // Cache the original material and get the renderer component
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;

        // Get the Rigidbody component of the object
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Check if the player is within interaction distance and facing the object
        if (IsPlayerClose() && IsPlayerFacing())
        {
            // Highlight the object
            HighlightObject();

            // Check for interaction input (e.g., "E" key)
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (holding) {
                    ReleaseObject();
                    holding = false;
                } else {
                    // Perform grab logic (you can implement this based on your game's needs)
                    GrabObject();
                    holding = true;
                }
            }
        }
        else
        {
            // Reset to the original material if the player is not close or facing the object
            ResetMaterial();
        }
    }

    private bool IsPlayerClose()
    {
        // Check if the player is within interaction distance
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.Instance.transform.position);
        return distanceToPlayer <= interactionDistance;
    }

    private bool IsPlayerFacing()
    {
        // Check if the player is facing the object
        Vector3 toObject = transform.position - PlayerController.Instance.transform.position;
        float angle = Vector3.Angle(PlayerController.Instance.transform.forward, toObject);
        return Mathf.Abs(angle) < 45f; // Adjust the angle threshold as needed
    }

    private void HighlightObject()
    {
        // Highlight the object using the specified material
        objectRenderer.material = highlightMaterial;
    }

    private void ResetMaterial()
    {
        // Reset the material to the original material
        objectRenderer.material = originalMaterial;
    }

    private void GrabObject()
    {
        // Set the player as the parent of the grabable object
        transform.SetParent(PlayerController.Instance.transform);

        // Disable the collider of the grabable object (optional, depending on your game logic)
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Enable the Rigidbody of the grabable object (optional, depending on your game logic)
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    private void ReleaseObject()
    {
        // Release the object from the player's control
        transform.SetParent(originalParent);

        // Enable the collider of the grabable object (optional, depending on your game logic)
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
        }

        // Enable the Rigidbody of the grabable object (optional, depending on your game logic)
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        // You can perform additional actions here based on your game's requirements
        // For example, you might want to play a sound, trigger an animation, etc.
        Debug.Log("Object released!");
    }
}
