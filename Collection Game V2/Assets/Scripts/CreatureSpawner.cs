using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public Inventory inventoryObject;
    public Transform character;
    public string[,] map;
    public int tileSize = 1;//32;
    public float xOffset = 0;
    public float yOffset = 0;
    
    private List<int> mountainIdxs = new List<int>();
    private List<int> grassIdxs = new List<int>();
    private List<int> beachIdxs = new List<int>();
    private List<int> waterIdxs = new List<int>();
    
    // private TilemapGenerating tilemapGenerating;
    public TilemapGenerating tilemapGenerating;

    private int minCharDist = 5;
    private int maxCharDist = 25;
    private float timer = 0.0f;
    private float interval = 1.0f;

    void Start()
    {
        inventoryObject = GameObject.FindGameObjectsWithTag("Inventory")[0].GetComponent<Inventory>();

        // tilemapGenerating = FindObjectOfType<TilemapGenerating>();

        for (int i = 0; i < inventoryObject.creatures.Count; i++){
            for (int x = 0; x < inventoryObject.creatures[i].probability; x++){
                if (inventoryObject.creatures[i].SnowSpawn){
                    mountainIdxs.Add(i);
                } if (inventoryObject.creatures[i].GrassSpawn){
                    grassIdxs.Add(i);
                } if (inventoryObject.creatures[i].BeachSpawn){
                    beachIdxs.Add(i);
                } if (inventoryObject.creatures[i].WaterSpawn){
                    waterIdxs.Add(i);
                }
            }
        }

        StartCoroutine(WaitForInitialization());
    }

    IEnumerator WaitForInitialization()
    {
        while (!tilemapGenerating.isInitialized)
        {
            yield return null;
        }
        // access the array here
        map = tilemapGenerating.map;
    }

    void Update()
    {
        // Debug.Log(map[0, 0]);

        timer += Time.deltaTime; // increment timer by time since last update
        if (timer >= interval)
        {
            List<int> validTilesX = new List<int>();
            List<int> validTilesY = new List<int>();
            timer = 0.0f; // reset timer
            // --spawn creature--
            // get surrounding tiles
            Vector3 tilePosition;
            for (int x = 0; x < map.GetLength(0); x++) {
                for (int y = 0; y < map.GetLength(1); y++) {
                    tilePosition = new Vector3 ((x*tileSize), (y*tileSize), character.transform.position.z);
                    // Calculate the distance between the tile and the character
                    float distance = Vector3.Distance(character.transform.position, tilePosition);

                    // Check if the distance is within the valid range
                    if (distance >= minCharDist && distance <= maxCharDist) {
                        // If so, add the tile's position to the validTiles list
                        validTilesX.Add(x);
                        validTilesY.Add(y);
                    }
                }
            }

            // Choose a random tile from the validTiles list
            // Debug.Log(validTilesX.Count);
            // Debug.Log(validTilesY.Count);
            int idx = Random.Range(0, validTilesX.Count);
            Vector3 randomTilelocation = new Vector3(validTilesX[idx]*tileSize + xOffset + 1, validTilesY[idx]*tileSize + yOffset + 1, 0);

            int i;
            // choose random creature
            if (map[validTilesX[idx], validTilesY[idx]] == "M"){
                i = mountainIdxs[Random.Range(0, mountainIdxs.Count)];
            } else if (map[validTilesX[idx], validTilesY[idx]] == "G"){
                i = grassIdxs[Random.Range(0, grassIdxs.Count)];
            } else if (map[validTilesX[idx], validTilesY[idx]] == "B"){
                i = beachIdxs[Random.Range(0, beachIdxs.Count)];
            } else if (map[validTilesX[idx], validTilesY[idx]] == "W"){
                i = waterIdxs[Random.Range(0, waterIdxs.Count)];
            } else {
                i = 0;
            }
            
            // spawn inventoryObject.creatures[i] at randomTilelocation
            // Debug.Log(randomTilelocation);
            Instantiate(inventoryObject.creatures[i], randomTilelocation, Quaternion.identity);
        }
    }
}
