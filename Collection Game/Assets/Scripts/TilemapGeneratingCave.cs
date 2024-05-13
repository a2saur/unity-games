using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGeneratingCave : MonoBehaviour
{
    public int width = 100; // Width of the cave map
    public int height = 30; // Height of the cave map
    public Tilemap caveTilemap; // Reference to the Tilemap component
    public TileBase wallTile; // Reference to the wall tile

    public bool isInitialized = false;

    // public int seed; // Seed for random number generation

    [Range(0, 100)]
    public int wallPercentage; // Percentage of the cave filled with walls

    void Start()
    {
        GenerateCave();
        isInitialized = true;
    }

    void GenerateCave()
    {
        caveTilemap.ClearAllTiles();
        // Random.InitState(seed);

        int[,] caveMap = new int[width, height];

        // Generate the initial random cave layout
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == width - 1 || y == 0 || y == height - 1)
                {
                    caveMap[x, y] = 1; // Make the right and top borders solid walls
                }
                else if (x == 0)
                {
                    caveMap[x, y] = 0; // Open the left side
                }
                else
                {
                    caveMap[x, y] = (Random.Range(0, 100) < wallPercentage) ? 1 : 0;
                }
            }
        }

        // Smooth the cave layout to remove noise and fill gaps
        for (int i = 0; i < 5; i++)
        {
            SmoothCave(caveMap);
        }

        // Connect all regions in the cave map
        ConnectCaveRegions(caveMap);

        // Convert the cave layout to Tilemap tiles (only walls will be converted)
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (caveMap[x, y] == 1)
                {
                    Vector3Int tilePosition = new Vector3Int(x - width / 2, y - height / 2, 0);
                    caveTilemap.SetTile(tilePosition, wallTile);
                }
            }
        }
    }

    void SmoothCave(int[,] map)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int wallCount = GetSurroundingWallCount(map, x, y);
                if (wallCount > 4)
                {
                    map[x, y] = 1;
                }
                else if (wallCount < 4)
                {
                    map[x, y] = 0;
                }
            }
        }
    }

    void ConnectCaveRegions(int[,] map)
    {
        List<List<Vector2Int>> regions = GetRegions(map);

        int mainRegionSize = 0;
        List<Vector2Int> mainRegion = null;

        foreach (var region in regions)
        {
            if (region.Count > mainRegionSize)
            {
                mainRegionSize = region.Count;
                mainRegion = region;
            }
        }

        foreach (var region in regions)
        {
            if (region != mainRegion)
            {
                foreach (var tile in region)
                {
                    map[tile.x, tile.y] = 0;
                }
            }
        }
    }

    List<List<Vector2Int>> GetRegions(int[,] map)
    {
        List<List<Vector2Int>> regions = new List<List<Vector2Int>>();
        int[,] visited = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (visited[x, y] == 0 && map[x, y] == 0)
                {
                    List<Vector2Int> region = GetRegionTiles(map, x, y);
                    regions.Add(region);

                    foreach (var tile in region)
                    {
                        visited[tile.x, tile.y] = 1;
                    }
                }
            }
        }

        return regions;
    }

    List<Vector2Int> GetRegionTiles(int[,] map, int startX, int startY)
    {
        List<Vector2Int> tiles = new List<Vector2Int>();
        int[,] visited = new int[width, height];
        int tileType = map[startX, startY];

        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(new Vector2Int(startX, startY));

        while (queue.Count > 0)
        {
            Vector2Int tile = queue.Dequeue();
            if (visited[tile.x, tile.y] == 0 && map[tile.x, tile.y] == tileType)
            {
                visited[tile.x, tile.y] = 1;
                tiles.Add(tile);

                for (int x = tile.x - 1; x <= tile.x + 1; x++)
                {
                    for (int y = tile.y - 1; y <= tile.y + 1; y++)
                    {
                        if (x >= 0 && x < width && y >= 0 && y < height && (x == tile.x || y == tile.y))
                        {
                            queue.Enqueue(new Vector2Int(x, y));
                        }
                    }
                }
            }
        }

        return tiles;
    }

    int GetSurroundingWallCount(int[,] map, int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighborX = gridX - 1; neighborX <= gridX + 1; neighborX++)
        {
            for (int neighborY = gridY - 1; neighborY <= gridY + 1; neighborY++)
            {
                if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height)
                {
                    if (neighborX != gridX || neighborY != gridY)
                    {
                        wallCount += map[neighborX, neighborY];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }
        return wallCount;
    }
}
