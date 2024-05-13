using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    public Transform centerObject;  // The object around which the circle will be created
    public float radius = 3f;       // Radius of the circle
    public float speed = 2f;        // Speed of rotation
    private float timeToDisappear = 1f;
    private float timer = 0f;

    private Renderer objectRenderer;  // Renderer component of the object

    public GameObject inventoryObject;// List<string> inventory = new List<string>();

    void Start()
    {
        inventoryObject = GameObject.FindGameObjectsWithTag("Inventory")[0];

        objectRenderer = GetComponent<Renderer>();
        objectRenderer.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // If the space key is pressed, toggle the visibility
            objectRenderer.enabled = true;

            // If the object is set to be visible, start a timer to make it disappear after 1 second
            if (objectRenderer.enabled)
            {
                timer = timeToDisappear;
            }
        }

        // If the object is visible and the timer is running, decrease the timer
        if (objectRenderer.enabled && timer > 0f)
        {
            timer -= Time.deltaTime;

            // Move in a circle around the centerObject in 2D
            float angle = Time.time * speed;
            Vector3 newPosition = centerObject.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            transform.position = new Vector3(newPosition.x, newPosition.y, -0.5f);

            transform.Rotate(Vector3.forward * 450 * Time.deltaTime);

            // Look at the centerObject
            // transform.LookAt(centerObject);

            // If the timer reaches 0, hide the object
            if (timer <= 0f)
            {
                objectRenderer.enabled = false;
                transform.position = new Vector3(-100, -100, 15);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D hit){
        if (hit.gameObject.tag == "Creature") {
            // Debug.Log(hit.gameObject.GetComponent<CreatureAttributes>().creatureName);
            inventoryObject.GetComponent<Inventory>().inventory.Add(hit.gameObject.GetComponent<CreatureAttributes>().creatureName);
            inventoryObject.GetComponent<Inventory>().catalog[hit.gameObject.GetComponent<CreatureAttributes>().creatureName] = true;
            Destroy(hit.gameObject);
        }
    }
}