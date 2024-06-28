using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaController : MonoBehaviour
{
    public GameObject[] baseDrinks;
    public GameObject[] flavoringDrinks;
    public GameObject drinkHighlighter;
    public GameObject baseDrink;
    public GameObject flavoringDrink;
    public GameObject[] toppingButtons;
    public GameObject[] floatingToppings;
    public GameObject[] decorations;
    public ParticleSystem particleEffect;

    private int step = 0;
    private bool filling = false;
    private float maxDrinkHeight = 0;
    private float fillAmount = 0.25f;

    void Start()
    {
        for (int i = 0; i < baseDrinks.Length; i++){
            baseDrinks[i].transform.position = new Vector3((i * 0.7f)-1.05f, 0.8f, 0);
        }

        for (int i = 0; i < flavoringDrinks.Length; i++){
            flavoringDrinks[i].transform.position = new Vector3((i * 0.7f)-1.05f, 0.8f, 0);
            flavoringDrinks[i].SetActive(false);
        }

        for (int i = 0; i < toppingButtons.Length; i++){
            toppingButtons[i].SetActive(false);
        }

        for (int i = 0; i < decorations.Length; i++){
            decorations[i].SetActive(false);
        }

        drinkHighlighter.SetActive(false);
        baseDrink.SetActive(false);
        baseDrink.transform.position = new Vector3 (0, -0.6f, 0);
    }
    
    void Update()
    {
        if (step == 0){ // Drink Selection
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

                if (hit.collider != null) {
                    GameObject clickedObject = hit.collider.gameObject;
                    if (clickedObject.tag == "drink-button"){
                        drinkHighlighter.SetActive(true);
                        drinkHighlighter.transform.position = clickedObject.transform.position + new Vector3(0, 0.01f, 0.1f);

                        // hide drink buttons
                        for (int i = 0; i < baseDrinks.Length; i++){
                            baseDrinks[i].SetActive(false);
                        }
                        clickedObject.SetActive(true);

                        // Set base drink image
                        baseDrink.SetActive(true);
                        baseDrink.GetComponent<SpriteRenderer>().sprite = clickedObject.GetComponent<SpriteRenderer>().sprite;
                        step++;
                    }
                }
            }
        } if (step == 1) { // Filling Drink
            if (Input.GetMouseButtonDown(0)){
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

                if (hit.collider != null) {
                    GameObject clickedObject = hit.collider.gameObject;
                    if (clickedObject.tag == "cup"){
                        // Start filling
                        filling = true;
                        Debug.Log("Started filling");
                    }
                }
            } if (Input.GetMouseButtonUp(0) && filling){
                // Stop filling
                filling = false;
                Debug.Log("Stop filling");
                step++;

                drinkHighlighter.SetActive(false);
                // hide drink buttons
                for (int i = 0; i < baseDrinks.Length; i++){
                    baseDrinks[i].SetActive(false);
                }
                for (int i = 0; i < flavoringDrinks.Length; i++){
                    flavoringDrinks[i].SetActive(true);
                }
            }

            if (filling) {
                if (baseDrink.transform.position.y < maxDrinkHeight){
                    baseDrink.transform.position = new Vector3(0, baseDrink.transform.position.y+(fillAmount*Time.deltaTime), 0);
                }
            }
        } if (step == 2) { // select additional flavor
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

                if (hit.collider != null) {
                    GameObject clickedObject = hit.collider.gameObject;
                    if (clickedObject.tag == "drink-button"){
                        // hide drink buttons
                        for (int i = 0; i < flavoringDrinks.Length; i++){
                            flavoringDrinks[i].SetActive(false);
                        }

                        // Set base drink image
                        flavoringDrink.SetActive(true);
                        flavoringDrink.GetComponent<SpriteRenderer>().sprite = clickedObject.GetComponent<SpriteRenderer>().sprite;
                        flavoringDrink.transform.position = baseDrink.transform.position;

                        // show topping buttons
                        for (int i = 0; i < toppingButtons.Length; i++){
                            toppingButtons[i].SetActive(true);
                        }
                        for (int i = 0; i < floatingToppings.Length; i++){
                            floatingToppings[i].transform.position = flavoringDrink.transform.position + new Vector3 (0, 0, -0.1f);
                        }
                        step++;
                    }
                }
            }
        } if (step == 3) { // select additional toppings
            // 
        } if (step == 4) { // finish!
            particleEffect.Play();
            for (int i = 0; i < decorations.Length; i++){
                decorations[i].SetActive(true);
            }
            for (int i = 0; i < toppingButtons.Length; i++){
                toppingButtons[i].SetActive(false);
            } 
            step++;
        }
    }

    public void NextStep(){
        step++;
    }

    public void Reset(){
        for (int i = 0; i < baseDrinks.Length; i++){
            baseDrinks[i].transform.position = new Vector3((i * 0.7f)-1.05f, 0.8f, 0);
            baseDrinks[i].SetActive(true);
        }

        for (int i = 0; i < flavoringDrinks.Length; i++){
            flavoringDrinks[i].transform.position = new Vector3((i * 0.7f)-1.05f, 0.8f, 0);
            flavoringDrinks[i].SetActive(false);
        }

        for (int i = 0; i < toppingButtons.Length; i++){
            toppingButtons[i].SetActive(false);
        }

        for (int i = 0; i < decorations.Length; i++){
            decorations[i].SetActive(false);
        }

        drinkHighlighter.SetActive(false);

        baseDrink.SetActive(false);
        baseDrink.transform.position = new Vector3 (0, -0.6f, 0);

        flavoringDrink.SetActive(false);
        flavoringDrink.transform.position = new Vector3 (0, -0.6f, 0);
        step = 0;
    }
}