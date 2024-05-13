using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class CreatureSpawnerCave : MonoBehaviour
{
    public Inventory inventoryObject;

    public int minX = -45;
    public int maxX = 50;
    public int minY = -10;
    public int maxY = 10;
    
    private List<int> creatureIdxs = new List<int>();
    private List<int> spawnLocsX = new List<int>();
    private List<int> spawnLocsY = new List<int>();
    
    public TilemapGeneratingCave tilemapGenerating;
    public Tilemap caveTilemap;

    private float timer = 0.0f;
    public float interval = 1.0f;

    void Start()
    {
        inventoryObject = GameObject.FindGameObjectsWithTag("Inventory")[0].GetComponent<Inventory>();

        for (int i = 0; i < inventoryObject.creatures.Count; i++){
            for (int x = 0; x < inventoryObject.creatures[i].probability; x++){
                if (inventoryObject.creatures[i].CaveSpawn){
                    creatureIdxs.Add(i);
                }
            }
        }

        StartCoroutine(WaitForInitialization());

        Vector3Int tilePosition;
        for (int x = minX; x < maxX; x++)
        {
            for (int y = minY; y < maxY; y++)
            {
                tilePosition = new Vector3Int (x, y, 0);
                if (caveTilemap.GetTile(tilePosition) == null)
                {
                    Debug.Log(tilePosition.x);
                    Debug.Log(tilePosition.y);
                    // The tile is empty (not a wall)
                    // Instantiate(objectPrefab, tilePosition, Quaternion.identity);
                    spawnLocsX.Add(x);
                    spawnLocsY.Add(y);
                }
            }
        }
    }

    IEnumerator WaitForInitialization()
    {
        while (!tilemapGenerating.isInitialized)
        {
            yield return null;
        }
        // access the array here
        caveTilemap = tilemapGenerating.caveTilemap;
    }

    void Update()
    {
        timer += Time.deltaTime; // increment timer by time since last update
        if (timer >= interval)
        {
            // Choose a random tile from the validTiles list
            int idx = Random.Range(0, spawnLocsX.Count);
            Vector3 randomTilelocation = new Vector3(spawnLocsX[idx], spawnLocsY[idx], 0);

            int i = creatureIdxs[Random.Range(0, creatureIdxs.Count)];
            
            Instantiate(inventoryObject.creatures[i], randomTilelocation, Quaternion.identity);
            timer = 0.0f;
        }
    }
}
