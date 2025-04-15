using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The prefab to spawn
    public int spawnCount = 20;      // Number of objects to spawn
    public Vector3 spawnAreaSize = new Vector3(10, 10, 0); // Width and height of the spawn area

    void Start()
    {
        for (int i = 0; i < spawnCount; i++){
            // Generate a random position within the specified area
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
                0
            );

            // Spawn the object at the calculated position
            Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        }
    }
}
