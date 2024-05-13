using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public string FruitType;
    public string NextFruitType;
    public FruitLists dropper;
    public PauseScreen pauseMenu;
    public GameObject poofTransform;
    public ParticleSystem poof;
    public GameObject explosionTransform;
    public ParticleSystem explosion;
    public bool col = false;

    private int number;
    private Dictionary<string, int> scoring = new Dictionary<string, int>() {
        {"Cherry", 1},
        {"Strawberry", 3},
        {"Grape", 5},
        {"Persimmon", 10},
        {"Orange", 15},
        {"Apple", 21},
        {"Cantaloupe", 28},
        {"Peach", 36},
        {"Pineapple", 45},
        {"Melon", 55},
        {"Watermellon", 1000},
        };

    private float yThreshold = 2.0f; // The Y-axis threshold above which the function should be called.
    private float requiredDuration = 2.0f; // The duration in seconds the object should be above the threshold.
    private float elapsedTime = 0.0f; // Tracks the time above the threshold.

    private float shakeForce = 0.0005f; // The force to apply for the subtle motion.
    private float updateInterval = 1.0f; // Interval between force updates.
    private Rigidbody2D rb; // Reference to the Rigidbody2D component.

    // Start is called before the first frame update
    void Start()
    {
        dropper = GameObject.FindGameObjectsWithTag("Dropper")[0].GetComponent<FruitLists>();
        pauseMenu = GameObject.FindGameObjectsWithTag("Dropper")[0].GetComponent<PauseScreen>();

        poofTransform = GameObject.FindGameObjectsWithTag("EffectTransform")[0];
        poof = poofTransform.GetComponentInChildren<ParticleSystem>();
        
        explosionTransform = GameObject.FindGameObjectsWithTag("Explosion")[0];
        explosion = explosionTransform.GetComponentInChildren<ParticleSystem>();

        number = Random.Range(0, 1000);

        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ApplyContinuousForce());
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the object is above the Y-axis threshold.
        if (transform.position.y > yThreshold)
        {
            elapsedTime += Time.deltaTime;

            // If the object has been above the threshold for the required duration, call your function.
            if (elapsedTime >= requiredDuration)
            {
                explosion.Play();
                pauseMenu.Pause(); // Replace with the actual function you want to call.
            }
        }
        else
        {
            // Reset the timer if the object falls below the threshold.
            elapsedTime = 0.0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fruit"){
            // Debug.Log("A");
            if (collision.gameObject.GetComponent<Fruit>().FruitType == FruitType){
                // average two positions
                Vector3 avgPos = new Vector3(((transform.position.x+collision.gameObject.transform.position.x)/2), ((transform.position.y+collision.gameObject.transform.position.y)/2), ((transform.position.z+collision.gameObject.transform.position.z)/2));
                // destroy other fruit
                if (col){
                    // other one handled collision, pass
                } else {
                    col = true;
                    collision.gameObject.GetComponent<Fruit>().col = true;
                    // play poofs and update score
                    dropper.score += scoring[FruitType];
                    poofTransform.transform.position = avgPos;
                    poof.Play();

                    // change this to next fruit
                    int x = 0;
                    foreach(GameObject fruit in dropper.fruits)
                    {
                        if (fruit.GetComponent<Fruit>().FruitType == NextFruitType){
                            Instantiate(fruit, avgPos, Quaternion.identity);
                            Debug.Log("Found Fruit");
                            break;
                        }
                        x++;
                    }
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.tag == "Fruit"){
    //         // Debug.Log("A");
    //         if (collision.gameObject.GetComponent<Fruit>().FruitType == FruitType){
    //             Debug.Log("Same");
    //             Debug.Log(number);
    //             Debug.Log(collision.gameObject.GetComponent<Fruit>().number);
    //             Debug.Log("--------------------");

    //             // average two positions
    //             Vector3 avgPos = new Vector3(((transform.position.x+collision.gameObject.transform.position.x)/2), ((transform.position.y+collision.gameObject.transform.position.y)/2), ((transform.position.z+collision.gameObject.transform.position.z)/2));
    //             // destroy other fruit
    //             if (number < collision.gameObject.GetComponent<Fruit>().number){
    //                 // play poofs and update score
    //                 dropper.score += scoring[FruitType];
    //                 poofTransform.transform.position = avgPos;
    //                 poof.Play();

    //                 // change this to next fruit
    //                 int x = 0;
    //                 foreach(GameObject fruit in dropper.fruits)
    //                 {
    //                     if (fruit.GetComponent<Fruit>().FruitType == NextFruitType){
    //                         Instantiate(fruit, avgPos, Quaternion.identity);
    //                         Debug.Log("Found Fruit");
    //                         // SpriteRenderer sourceRenderer = fruit.GetComponent<SpriteRenderer>();
    //                         // SpriteRenderer targetRenderer = gameObject.GetComponent<SpriteRenderer>();

    //                         // Debug.Log("Changing Size");
    //                         // targetRenderer.sprite = sourceRenderer.sprite;
    //                         // transform.localScale = new Vector3(dropper.sizes[x], dropper.sizes[x], dropper.sizes[x]);
    //                         // transform.position = avgPos;

    //                         // Debug.Log(FruitType);
    //                         // FruitType = fruit.GetComponent<Fruit>().FruitType;
    //                         // NextFruitType = fruit.GetComponent<Fruit>().NextFruitType;
    //                         // Debug.Log(FruitType);
                            
    //                         break;
    //                     }
    //                     x++;
    //                 }
    //                 Destroy(collision.gameObject);
    //             } else {
    //                 // play poofs and update score
    //                 // dropper.score += scoring[FruitType];
    //                 // poofTransform.transform.position = avgPos;
    //                 // poof.Play();
    //                 // change other to next fruit
    //                 // int x = 0;
    //                 // foreach(GameObject fruit in dropper.fruits)
    //                 // {
    //                 //     if (fruit.GetComponent<Fruit>().FruitType == NextFruitType){
    //                         // Instantiate(fruit, new Vector3(dropper.sizes[x], dropper.sizes[x], dropper.sizes[x]), Quaternion.identity);
    //                 //         Debug.Log("Found Fruit");
    //                 //         SpriteRenderer sourceRenderer = fruit.GetComponent<SpriteRenderer>();
    //                 //         SpriteRenderer targetRenderer = collision.gameObject.GetComponent<SpriteRenderer>();

    //                 //         Debug.Log("Changing Size");
    //                 //         targetRenderer.sprite = sourceRenderer.sprite;
    //                 //         collision.transform.localScale = new Vector3(dropper.sizes[x], dropper.sizes[x], dropper.sizes[x]);
    //                 //         collision.transform.position = avgPos;

    //                 //         collision.gameObject.GetComponent<Fruit>().FruitType = fruit.GetComponent<Fruit>().FruitType;
    //                 //         collision.gameObject.GetComponent<Fruit>().NextFruitType = fruit.GetComponent<Fruit>().NextFruitType;
                            
    //                 //         break;
    //                 //     }
    //                 //     x++;
    //                 // }

    //                 Destroy(gameObject);
    //             }
    //         }
    //     }
    }

    IEnumerator ApplyContinuousForce()
    {
        while (true)
        {
            // Generate a random force for the subtle motion.
            // Vector2 randomForce = new Vector2(Random.Range(-shakeForce, shakeForce), Random.Range(-shakeForce, shakeForce));
            Vector2 randomForce = new Vector2(0.0f, Random.Range(0, shakeForce));

            // Apply the random force to the Rigidbody2D.
            rb.AddForce(randomForce, ForceMode2D.Impulse);

            // Wait for the specified interval before applying the next force.
            yield return new WaitForSeconds(updateInterval);
        }
    }
}
