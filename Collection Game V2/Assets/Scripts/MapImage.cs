using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapImage : MonoBehaviour
{
    // The string array representing the image
    public string[] stringArray;

    // The colors to use for each character in the string array
    public Color gColor = Color.green;
    public Color wColor = Color.blue;
    public Color mColor = Color.white;
    public Color bColor = new Color(0.8f, 0.8f, 0.6f); // Beige color

    // The size of the texture to create
    public int textureWidth = 64;
    public int textureHeight = 64;
    
    public string[,] map;
    public TilemapGenerating tilemapGenerating;

    void Start()
    {
        StartCoroutine(WaitForInitialization());
    }

    IEnumerator WaitForInitialization()
    {
        while (!tilemapGenerating.isInitialized)
        {
            yield return null;
        }
        map = tilemapGenerating.map;
        // Create a new texture
        Texture2D texture = new Texture2D(textureWidth, textureHeight);

        // Loop through each character in the string array and set the corresponding pixel color
        for (int y = 0; y < textureHeight; y++)
        {
            for (int x = 0; x < textureWidth; x++)
            {
                string c = map[y, x];
                Color color = GetColorForChar(c);
                texture.SetPixel(x, y, color);
            }
        }

        // Apply the changes to the texture
        texture.Apply();

        // Set the texture on the UI image component
        GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, textureWidth, textureHeight), new Vector2(0.5f, 0.5f));
    }

    // Returns the color to use for a given character in the string array
    private Color GetColorForChar(string c)
    {
        switch (c)
        {
            case "G":
                return gColor;
            case "W":
                return wColor;
            case "M":
                return mColor;
            case "B":
                return bColor;
            default:
                return Color.black;
        }
    }
}
