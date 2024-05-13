using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Linq;

public class FarmingTilemap : MonoBehaviour
{
    public int rowLen = 45; //18;
    public int colLen = 26; //10;
    public float xOffset = -24.5f; //8.5f;
    public float yOffset = 15.5f; //-5.5f;
    public float updateInterval = 30f;

    public Tilemap tilemap; // reference to the tilemap component
    public TileBase[] cropTiles; // Array of tiles for each stage of crop growth
    public int[] cropAges; // Array of crop ages, where cropAges[i] is the age of the crop in tile cropTiles[i]
    
    public Transform dayscale;

    public Tilemap additionalTilemap;
    public TileBase additionalTile;

    public int currentIndex;
    public int[] wateredIndex;

    private float timeSinceLastUpdate;

    private void Start()
    {
        currentIndex = 0;
        timeSinceLastUpdate = 0f;
        UpdateTileData(); // Call the method to update the tile data on start
    }

    private void Update()
    {
        dayscale.transform.position = new Vector3(dayscale.transform.position.x, (timeSinceLastUpdate*(300/updateInterval))+75, 0); // 10: 300/timeInterval(30)
        // offset = -8.5, 4.5
        // int roundX = (int) System.Math.Round(transform.position.x-xOffset);
        // int roundY = (int) System.Math.Round(-1 * (transform.position.y-yOffset));
        // roundY = colLen - roundY;
        // currentIndex = roundX + roundY*rowLen;
        int cellX = (int)Math.Floor((transform.position.x - tilemap.origin.x) / tilemap.cellSize.x);
        int cellY = (int)Math.Floor((transform.position.y - tilemap.origin.y) / tilemap.cellSize.y);
        currentIndex = cellY * rowLen + cellX;
        if (Input.GetKeyDown(KeyCode.Space)) // Check if the space bar is pressed
        {
            Vector3Int playerPosition = tilemap.WorldToCell(transform.position);
            additionalTilemap.SetTile(playerPosition, additionalTile);
            try
            {
                wateredIndex[currentIndex] = 1;
                int currentAge = cropAges[currentIndex];
                if (currentAge == 4)
                {
                    cropAges[currentIndex] = 0;
                    // ACTION
                }
            }
            catch (System.IndexOutOfRangeException)
            {
                // Handle the exception here
                Debug.LogError("Index out of range: " + currentIndex);
            }
            // UpdateTileData(); // Call the method to update the tile data
        }

        timeSinceLastUpdate += Time.deltaTime;
        if (timeSinceLastUpdate >= updateInterval)
        {
            timeSinceLastUpdate = 0f;
            additionalTilemap.ClearAllTiles();
            // for (int i = 0; i < wateredIndex.Length; i++)
            // {
                // cropAges[wateredIndex[i]]++;
            // }
            UpdateTileData();
        }
    }

    public void UpdateTileData()
    {
        Vector3Int playerPosition = tilemap.WorldToCell(transform.position);
        additionalTilemap.SetTile(playerPosition, additionalTile);
        
        foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin) // Loop through all positions in the tilemap
        {
            // Check if the tile at the current position is a crop tile
            TileBase currentTile = tilemap.GetTile(position); // Get the tile at the current position
            if (currentTile != null && Array.IndexOf(cropTiles, currentTile) >= 0) // If the tile is a crop tile
            {
                int x = position.x - tilemap.cellBounds.min.x;
                int y = position.y - tilemap.cellBounds.min.y;
                int index = y * rowLen + x;
                
                if (wateredIndex[index] == 1){
                    // Get the current age of the crop
                    int currentAge = cropAges[index];

                    // Update the tile to the appropriate stage of growth based on age
                    TileBase newTile = cropTiles[Mathf.Clamp(currentAge, 0, cropTiles.Length - 1)]; // Get the new tile for the current stage of growth
                    tilemap.SetTile(position, newTile); // Set the new tile at the current position on the tilemap

                    // Increase the crop age by one for the next update
                    cropAges[index] = currentAge + 1;
                    wateredIndex[index] = -1;
                }
            }
        }
    }

    // // Update the tile data for each cell on the tilemap based on crop age
    // public void UpdateTileData()
    // {
    //     Vector3Int playerPosition = tilemap.WorldToCell(transform.position);
    //     additionalTilemap.SetTile(playerPosition, additionalTile);
    //     int cropIndex = 0;
    //     foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin) // Loop through all positions in the tilemap
    //     {
    //         // Check if the tile at the current position is a crop tile
    //         TileBase currentTile = tilemap.GetTile(position); // Get the tile at the current position
    //         if (cropIndex >= 0) // If the tile is a crop tile
    //         {
    //             if (wateredIndex[cropIndex] == 1){
    //                 // Get the current age of the crop
    //                 int currentAge = cropAges[cropIndex];

    //                 // Update the tile to the appropriate stage of growth based on age
    //                 TileBase newTile = cropTiles[Mathf.Clamp(currentAge, 0, cropTiles.Length - 1)]; // Get the new tile for the current stage of growth
    //                 tilemap.SetTile(position, newTile); // Set the new tile at the current position on the tilemap

    //                 // Increase the crop age by one for the next update
    //                 cropAges[cropIndex] = currentAge + 1;
    //                 wateredIndex[cropIndex] = -1;
    //             }
    //         }
    //         cropIndex++;
    //     }
    // }
}