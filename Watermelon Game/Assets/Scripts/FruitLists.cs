using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FruitLists : MonoBehaviour
{
    public List<GameObject> fruits;
    public List<float> sizes;
    public int score = 0;
    public TMP_Text textbox;
    public PauseScreen pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.FindGameObjectsWithTag("Dropper")[0].GetComponent<PauseScreen>();
    }

    // Update is called once per frame
    void Update()
    {
        textbox.text = "Score\n"+score.ToString();
    }

    public void Restart(){
        score = 0;
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Fruit");
        foreach(GameObject go in gos) {
            Destroy(go);
        }
        pauseMenu.Resume();
    }
}
