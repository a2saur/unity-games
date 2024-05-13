using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using System.Linq;

public class TilemapGenerating : MonoBehaviour
{
    public Tilemap tilemap;
    public string[,] map;
    public int[,] numMap;
    public string[] letterList;
    public TileBase[] tileList;

    // public int xOffset;
    // public int yOffset;

    const int SHIFT = 1;
    const int numRows = 100;
    const int numCols = 100;

    private Dictionary<string, TileBase> letterTileDict;

    public bool isInitialized = false;

    // void Start()
    // {
    //     inventoryObject = GameObject.FindGameObjectsWithTag("Inventory")[0];
    //     Random.seed = inventoryObject.GetComponent<Inventory>().seed;
    public GameObject inventoryObject;
    public int randomSeed;

    void Start()
    {
        inventoryObject = GameObject.FindGameObjectsWithTag("Inventory")[0];
        randomSeed = inventoryObject.GetComponent<Inventory>().seed;
        Random.InitState(randomSeed);
        Debug.Log("Okay!");

        numMap = new int[numRows, numCols];
        map = new string[numRows, numCols];
        // Create the letter-tile dictionary
        letterTileDict = letterList.Zip(tileList, (letter, tile) => new { letter, tile })
                                    .ToDictionary(pair => pair.letter, pair => pair.tile);

        Debug.Log("Map");
        GenerateMap();
        Debug.Log("Transferring");
        TransferMap();
        Debug.Log("Creating tiles");
        GenerateTilemap();
        isInitialized = true;
    }

    int random_up_down(int minVal, int maxVal, int prob){
        int v = Random.Range(0, 100);
        if (v < prob){
            return minVal;
        } else {
            return maxVal;
        }
    }

    void GenerateTilemap()
    {
        // int numRows = mapList.GetLength(0);
        // int numCols = mapList.GetLength(1);

        for (int x = 0; x < numRows; x++)
        {
            for (int y = 0; y < numCols; y++)
            {
                string letter = map[x, y];
                TileBase tileToUse;
                if (letterTileDict.TryGetValue(letter, out tileToUse))
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tileToUse);
                }
            }
        }
    }

    void GenerateMap()
    {
        int x = Random.Range(0, numRows);
        int y = Random.Range(0, numCols);
        int h = Random.Range(25, 50);

        // AddIsland(Random.Range((int) numRows/100, (int) numRows/10), Random.Range((int) numCols/100, (int) numCols/10), x, y, h);
        AddIsland(x, y, h);
    }

    void AddIsland(int x, int y, int h)
    {
        if (x < 1 || x >= numRows-1){
            return;
        } else if (y < 1 || y >= numCols-1){
            return;
        }
        numMap[x, y] = h;
        for (int dx = -1; dx < 2; dx++){
            for (int dy = -1; dy < 2; dy++){
                if (numMap[x+dx, y+dy] < h-SHIFT && h-SHIFT > 0){
                    AddIsland(x+dx, y+dy, h-random_up_down(-SHIFT, SHIFT, 4));
                }
            }
        }
    }

    void TransferMap()
    {
        // biomes = {
        //     "M":[27, 50],
        //     "G":[17, 26],
        //     "B":[14, 16],
        //     "W":[-20, 13],
        // }
        for (int x = 0; x < numRows; x++)
        {
            for (int y = 0; y < numCols; y++)
            {
                // if (27 < numMap[x, y] && numMap[x, y] < 50){
                if (27 <= numMap[x, y]){
                    map[x, y] = "M";
                } else if (17 <= numMap[x, y] && numMap[x, y] <= 26){
                    map[x, y] = "G";
                } else if (14 <= numMap[x, y] && numMap[x, y] <= 16){
                    map[x, y] = "B";
                } else if (numMap[x, y] <= 13){
                    map[x, y] = "W";
                } else {
                    // Debug.Log(numMap[x, y]);
                    map[x, y] = "A";
                }
            }
        }

        for (int ix = 0; ix < 25; ix++){
            for (int iy = 0; iy < 25; iy++){
                if (ix < 3 || ix > 22 || iy < 3 || iy > 22){
                        // beach
                        map[ix, iy] = "W";
                } else if (ix < 7 || ix > 17 || iy < 7 || iy > 17){
                        // beach
                        map[ix, iy] = "B";
                } else if (ix < 12 || ix > 15 || iy < 12 || iy > 15){
                        // grass
                        map[ix, iy] = "G";
                } else {
                    // mountain
                    map[ix, iy] = "M";
                }
            }
        }
    }
}
