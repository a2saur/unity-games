using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using System.Linq;

public class TownGenerating : MonoBehaviour
{
    public Town townBase;
    public int townNumBase;
    public int townNumRange;
    public int tileSize = 1;//32;
    public float xOffset = 0;
    public float yOffset = 0;

    const int numRows = 100;
    const int numCols = 100;

    public GameObject inventoryObject;
    public int randomSeed;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        inventoryObject = GameObject.FindGameObjectsWithTag("Inventory")[0];
        randomSeed = inventoryObject.GetComponent<Inventory>().seed;
        Random.InitState(randomSeed);
        Debug.Log("Town time:"+randomSeed);

        int numTowns = Random.Range(townNumBase-townNumRange, townNumBase+townNumRange);

        int x;
        int y;
        for (int t = 0; t < numTowns; t++){
            if (t == 0){
                x = 7;
                y = 7;
            } else {
                x = Random.Range(0, numRows);
                y = Random.Range(0, numCols);
            }

            AddTown(x, y);
        }
        Debug.Log("Added towns!");
    }

    void AddTown(int x, int y)
    {
        Vector3 location = new Vector3(x*tileSize + xOffset + 1, y*tileSize + yOffset + 1, 0);
        Instantiate(townBase, location, Quaternion.identity);
    }
}