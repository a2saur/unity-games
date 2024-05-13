using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using System.Linq;

public class CaveGeneratorOnMap : MonoBehaviour
{
    public GameObject caveBase;
    public int caveNumBase;
    public int caveNumRange;
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
        Random.InitState(randomSeed+1);
        Debug.Log("cave time:"+randomSeed);

        int numcaves = Random.Range(caveNumBase-caveNumRange, caveNumBase+caveNumRange);

        int x;
        int y;
        for (int t = 0; t < numcaves; t++){
            x = Random.Range(0, numRows);
            y = Random.Range(0, numCols);

            Addcave(x, y);
        }
        Debug.Log("Added caves!");
    }

    void Addcave(int x, int y)
    {
        Vector3 location = new Vector3(x*tileSize + xOffset + 1, y*tileSize + yOffset + 1, 0);
        Instantiate(caveBase, location, Quaternion.identity);
    }
}