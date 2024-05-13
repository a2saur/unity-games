using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SandwichMaking : MonoBehaviour
{
    public GameObject[] ingredients;
    public int currentIngredient;
    public Vector3 startPos;

    public float movement = 0.1f;
    public bool movingRight = true;
    public bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        currentIngredient = 0;
        for (int i = 0; i < ingredients.Length; i++){
            ingredients[i].GetComponent<Rigidbody2D>().gravityScale = 0;
            ingredients[i].SetActive(false);
        }
        ingredients[currentIngredient].SetActive(true);
        ingredients[currentIngredient].transform.position = new Vector3(startPos.x, startPos.y, ingredients[currentIngredient].transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (done){
            // 
        } else {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)){
                // drop
                ingredients[currentIngredient].GetComponent<Rigidbody2D>().gravityScale = 1;
                currentIngredient++;
                if (currentIngredient < ingredients.Length){
                    ingredients[currentIngredient].SetActive(true);
                    ingredients[currentIngredient].transform.position = new Vector3(startPos.x, startPos.y, ingredients[currentIngredient].transform.position.z);
                } else {
                    StartCoroutine(WaitForDone());
                    done = true;
                }
            } else {
                if (movingRight) {
                    ingredients[currentIngredient].transform.position = new Vector3(ingredients[currentIngredient].transform.position.x+movement, ingredients[currentIngredient].transform.position.y, ingredients[currentIngredient].transform.position.z);
                    if (ingredients[currentIngredient].transform.position.x > 5){
                        movingRight = false;
                    }
                } else {
                    ingredients[currentIngredient].transform.position = new Vector3(ingredients[currentIngredient].transform.position.x-movement, ingredients[currentIngredient].transform.position.y, ingredients[currentIngredient].transform.position.z);
                    if (ingredients[currentIngredient].transform.position.x < -5){
                        movingRight = true;
                    }
                }
            }
        }
    }

    IEnumerator WaitForDone()
    {
        yield return new WaitForSeconds(3);

        bool allGood = true;
        for (int i = 0; i < ingredients.Length; i++){
            if (ingredients[i].transform.position.y < -3){
                allGood = false;
            }
        }

        if (allGood){
            // switch scene
            Scene firstScene = SceneManager.GetSceneByName("SampleScene");
            GameObject[] rootObjects = firstScene.GetRootGameObjects();
            foreach (var rootObj in rootObjects)
            {
                // Set root object and all its children to inactive
                rootObj.SetActive(true);
            }
            // SceneManager.LoadScene("SampleScene");
            SceneManager.UnloadScene("SandwichMaking");
        } else {
            // restart
            currentIngredient = 0;
            for (int i = 0; i < ingredients.Length; i++){
                ingredients[i].GetComponent<Rigidbody2D>().gravityScale = 0;
                ingredients[i].transform.rotation = Quaternion.identity;
                ingredients[i].SetActive(false);
            }
            ingredients[currentIngredient].SetActive(true);
            ingredients[currentIngredient].transform.position = new Vector3(startPos.x, startPos.y, ingredients[currentIngredient].transform.position.z);
            movingRight = true;

            movement -= 0.025f;
            if (movement < 0){
                movement = 0;
            }
            done = false;
        }
    }
}
