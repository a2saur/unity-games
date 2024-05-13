using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreatureAttributes : MonoBehaviour
{
    public int coins;
    public float probability;
    public string creatureName;
    public string description;

    public bool SnowSpawn;
    public bool GrassSpawn;
    public bool BeachSpawn;
    public bool WaterSpawn;
    public bool CaveSpawn;

    public bool move;
    public bool gravity;
    public Vector3 spawnPoint;
    public Vector3 originalVector = new Vector3(0.1f, 0.1f, 0);
    public float vectorLength = 1f;
    private SpriteRenderer spriteRenderer;
    private bool isRight;
    private float rightTimer;
    private float requiredTime = 1f;

    private float xMove = 0f;
    public float jumpVal = 5f;

    public float moveSpeed = 0f; // The speed of the movement
    public float pauseTime = 0f; // The time to pause between movements

    private Rigidbody2D gravityObject;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPoint = transform.position;

        gravityObject = GetComponent<Rigidbody2D>();
        if (SceneManager.GetActiveScene().name == "Cave"){
            gravityObject.gravityScale = 1.0f;
            xMove = Random.Range(-1, 2)/2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        bool temp = true;
        if (sceneName == "MainScene"){
            if (move){
                GameObject[] foodObjects = GameObject.FindGameObjectsWithTag("Food"); // Find all objects with the specified tag
        
                foreach (GameObject foodObject in foodObjects)
                {
                    float distance = Vector3.Distance(transform.position, foodObject.transform.position); // Calculate the distance between the player and the food object
                    
                    if (distance <= 3f)
                    {
                        temp = false;
                        if (transform.position.x < foodObject.transform.position.x){
                            // point right
                            spriteRenderer.flipX = true;
                        } else {
                            spriteRenderer.flipX = false;
                        }
                        // Calculate the direction from the player to the food object
                        Vector3 direction = foodObject.transform.position - transform.position;
                        direction.Normalize(); // Normalize the direction vector to have a length of 1
                        
                        // Move the player towards the food object using interpolation
                        transform.Translate(direction * 1.5f * Time.deltaTime);
                        break;
                    }
                }
                if (temp){
                    // shift
                    float randomNum = Random.Range(-1, 2)/100f;
                    originalVector.x += randomNum;
                    randomNum = Random.Range(-1, 2)/100f;
                    originalVector.y += randomNum;
                    
                    // rotate
                    float randomAngle = Random.Range(-45, 46) * Mathf.Deg2Rad;

                    Quaternion rotation = Quaternion.Euler(0f, 0f, randomAngle);

                    originalVector = rotation * originalVector;

                    // weigh
                    float distanceFromSpawn = Vector3.Distance(transform.position, spawnPoint);

                    // float weightFactor = Mathf.Lerp(0.1f, 0.0001f, distanceFromSpawn);

                    originalVector = Vector3.Lerp(originalVector, spawnPoint - transform.position, distanceFromSpawn*0.001f);

                    // add
                    // Check if the movement vector is pointing right
                    if (originalVector.x > 0f) {
                        // Start or continue the timer
                        if (!isRight) {
                            isRight = true;
                            rightTimer = 0f;
                        } else {
                            rightTimer += Time.deltaTime;
                            if (rightTimer >= requiredTime) {
                                // Flip the image horizontally
                                spriteRenderer.flipX = true;
                            }
                        }
                    } else {
                        // Reset the timer and image's flip
                        isRight = false;
                        rightTimer = 0f;
                        spriteRenderer.flipX = false;
                    }
                    transform.position += originalVector * Time.deltaTime;
                }
            }
        } else if (SceneManager.GetActiveScene().name == "Cave") {
            if (move) {
                Vector3 movement = new Vector3(xMove, 0, 0).normalized;
                transform.position += movement * moveSpeed * Time.deltaTime;
                
                float randomNum;
                if (xMove > 0){
                    randomNum = Random.Range(-2, 2)/1000f;
                } else if (xMove < 0){
                    randomNum = Random.Range(-1, 3)/1000f;
                } else {
                    randomNum = Random.Range(-1, 2)/1000f;
                }
                xMove += randomNum;

                if (Random.Range(0, 1000) == 5){
                    gravityObject.velocity = new Vector3(gravityObject.velocity.x, jumpVal, 0);
                }

                if (xMove > 0.1f) {
                    isRight = true;
                    spriteRenderer.flipX = true;
                } else {
                    // Reset the timer and image's flip
                    isRight = false;
                    spriteRenderer.flipX = false;
                }
            }
        }
    }

    public Sprite GetGameObjectImage()
    {
        // Access the SpriteRenderer component from the GameObject
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Get the currently displayed sprite from the SpriteRenderer
            Sprite currentSprite = spriteRenderer.sprite;

            return currentSprite;
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found on the CreatureAttributes GameObject.");
            return null;
        }
    }

    void OnCollisionEnter2D(Collision2D hit){
        if (hit.gameObject.tag == "Food") {
            Destroy(hit.gameObject);
        }
    }
}
