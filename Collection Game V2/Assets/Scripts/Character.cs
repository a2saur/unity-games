using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed = 5f;

    public int tileSize = 1;//32;

    public string[,] map;
    public TilemapGenerating tilemapGenerating;

    public ParticleSystem particleEffect; // Assign the particle system prefab in the inspector
    private bool effectPlaying = true;

    public GameObject snorkel;
    
    public GameObject food;
    public Sprite[] randomSprites;
    public ParticleSystem foodDrop;

    public GameObject inventoryObject;// List<string> inventory = new List<string>();

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        inventoryObject = GameObject.FindGameObjectsWithTag("Inventory")[0];

        StartCoroutine(WaitForInitialization());
        transform.position = inventoryObject.GetComponent<Inventory>().charLoc + new Vector3(0, -1.5f, 0);

        snorkel.SetActive(false);
    }

    IEnumerator WaitForInitialization()
    {
        while (!tilemapGenerating.isInitialized)
        {
            yield return null;
        }
        map = tilemapGenerating.map;
    }
    
    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0).normalized;
        transform.position += movement * moveSpeed * Time.deltaTime;

        // Determine the direction based on the input values
        if (movement.magnitude > 0)
        {
            if (Mathf.Abs(movement.x) != 0)
            {
                if (movement.x > 0)
                {
                    // Right direction
                    animator.SetInteger("Direction", 2);
                }
                else
                {
                    // Left direction
                    animator.SetInteger("Direction", 4);
                }
            }
            else
            {
                if (movement.y > 0)
                {
                    // Up direction
                    animator.SetInteger("Direction", 1);
                }
                else
                {
                    // Down direction
                    animator.SetInteger("Direction", 3);
                }
            }
        } else {
            animator.SetInteger("Direction", 0);
        }

        // Food
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.F)) {
            if (inventoryObject.GetComponent<Inventory>().food > 0){
                // Instantiate(food, transform.position, Quaternion.identity);
                int randomIndex = Random.Range(0, randomSprites.Length); // Get a random index from the sprite collection
                Sprite randomSprite = randomSprites[randomIndex]; // Get the random sprite

                GameObject newObject = Instantiate(food, transform.position, Quaternion.identity);
                SpriteRenderer spriteRenderer = newObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = randomSprite; // Assign the random sprite to the SpriteRenderer
                }

                inventoryObject.GetComponent<Inventory>().food--;
                foodDrop.transform.position = transform.position;
                foodDrop.Play();
            }
        }

        // tile location
        int tileX = (int) transform.position.x/tileSize;
        int tileY = (int) transform.position.y/tileSize;
        // Debug.Log(map[tileX, tileY]);
        try{
            if (map[tileX, tileY] == "M")
            {
                if (effectPlaying == false){
                    particleEffect.Play();
                    effectPlaying = true;
                    Debug.Log("SNOW");
                }
            } else {
                if (effectPlaying == true){
                    particleEffect.Stop();
                    effectPlaying = false;
                }
            }
            
            if (map[tileX, tileY] == "W"){
                if (inventoryObject.GetComponent<Inventory>().items["Snorkel"]){
                    snorkel.SetActive(true);
                    snorkel.transform.position = transform.position + new Vector3(0, 0, -0.5f);
                } else {
                    transform.position -= movement * moveSpeed * Time.deltaTime;
                }
            } else {
                snorkel.SetActive(false);
            }

            if (map[tileX-1, tileY-1] == null){
                transform.position -= movement * moveSpeed * Time.deltaTime;
            }
        } catch (System.IndexOutOfRangeException e) {
            // Handle the index out of bounds exception here
            // You can display an error message, perform error logging, or take other actions
            transform.position -= movement * moveSpeed * Time.deltaTime;
            // Debug.LogError("Index out of bounds exception: " + e.Message);
        }

        inventoryObject.GetComponent<Inventory>().charLoc = transform.position;
    }

    // void OnCollisionEnter2D(Collision2D hit){
    //     if (hit.gameObject.tag == "Creature") {
    //         // Debug.Log(hit.gameObject.GetComponent<CreatureAttributes>().creatureName);
    //         inventoryObject.GetComponent<Inventory>().inventory.Add(hit.gameObject.GetComponent<CreatureAttributes>().creatureName);
    //         inventoryObject.GetComponent<Inventory>().catalog[hit.gameObject.GetComponent<CreatureAttributes>().creatureName] = true;
    //         Destroy(hit.gameObject);
    //     }
    // }
}