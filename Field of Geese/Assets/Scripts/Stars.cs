using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stars : MonoBehaviour
{
    public Texture stars4;
    public Texture stars3;
    public Texture stars2;
    public Texture stars1;
    public Texture stars0;
    public SetManager SETMANAGER;
    private Dictionary<int, Texture> stars;
    // Start is called before the first frame update
    void Start()
    {
        stars = new Dictionary<int, Texture>() {
            {0, stars0},
            {1, stars1},
            {2, stars2},
            {3, stars3},
            {4, stars4},
        };
        SETMANAGER = GameObject.FindWithTag("SetManager").GetComponent<SetManager>();
    }

    void Update()
    {
        GetComponent<RawImage>().texture = stars[SETMANAGER.LevelScores[SETMANAGER.currentLevel]];
        if (SETMANAGER.LevelScores[SETMANAGER.currentLevel] <= 0){
            SceneManager.LoadScene("GameOver"); // Change to the next scene
        }
    }
}
