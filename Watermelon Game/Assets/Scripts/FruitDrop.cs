using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FruitDrop : MonoBehaviour
{
    public List<GameObject> fruits;
    public List<float> sizes;
    public int currentIndex = 0;

    void Start()
    {
        currentIndex = Random.Range(0, 5);
        SpriteRenderer sourceRenderer = fruits[currentIndex].GetComponent<SpriteRenderer>();
        SpriteRenderer targetRenderer = gameObject.GetComponent<SpriteRenderer>();

        // Check if the source and target SpriteRenderers exist.
        if (sourceRenderer != null && targetRenderer != null)
        {
            // Copy the sprite from the sourceRenderer to the targetRenderer.
            targetRenderer.sprite = sourceRenderer.sprite;
            transform.localScale = new Vector3(sizes[currentIndex], sizes[currentIndex], sizes[currentIndex]);
        }
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        if (-2.15f < mousePos.x && mousePos.x < 2.15f){
            transform.position = new Vector3(mousePos.x, transform.position.y, transform.position.z);
        } else if (mousePos.x > 2f) {
            transform.position = new Vector3(2.15f, transform.position.y, transform.position.z);
        } else if (mousePos.x < -2f) {
            transform.position = new Vector3(-2.15f, transform.position.y, transform.position.z);
        }
        // Debug.Log(mousePos.x);
        // Debug.Log(mousePos.y);

        if (Input.GetMouseButtonDown(0)){
            Instantiate(fruits[currentIndex], transform.position, Quaternion.identity);
            
            currentIndex = Random.Range(0, 5);
            SpriteRenderer sourceRenderer = fruits[currentIndex].GetComponent<SpriteRenderer>();
            SpriteRenderer targetRenderer = gameObject.GetComponent<SpriteRenderer>();

            // Check if the source and target SpriteRenderers exist.
            if (sourceRenderer != null && targetRenderer != null)
            {
                // Copy the sprite from the sourceRenderer to the targetRenderer.
                targetRenderer.sprite = sourceRenderer.sprite;
                transform.localScale = new Vector3(sizes[currentIndex], sizes[currentIndex], sizes[currentIndex]);
            }
        }
    }
}