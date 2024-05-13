using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawn;
    public GameObject spawn2;
    public bool whichSpawn = true;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float distBetween;
    public float variance;

    public int minNumCol; // min and maximum number allowed in the same X position
    public int maxNumCol;

    public string spawnerTag;

    private float currentX = 0;

    public SetManager SETMANAGER;

    // Start is called before the first frame update
    void Start()
    {
        SETMANAGER = GameObject.FindWithTag("SetManager").GetComponent<SetManager>();
        Spawn(); 
    }

    public void Spawn(){
        if (whichSpawn){
            currentX = minX;
        } else {
            currentX = minX+3;
        }
        while (currentX < maxX){
            int num = Random.Range(minNumCol, maxNumCol);
            for (int x = 0; x < num; x++){
                Vector3 tempPos = new Vector3(currentX+(Random.Range(-variance*10, variance*10)/10), (Random.Range(minY*10, maxY*10)/10), 0);
                if (whichSpawn){
                    Instantiate(spawn, tempPos, Quaternion.identity);
                } else {
                    Instantiate(spawn2, tempPos, Quaternion.identity);
                }
            }
            currentX += distBetween;
        }
    }

    public void Clear(){
        GameObject[] temps = GameObject.FindGameObjectsWithTag(spawnerTag);
        foreach(GameObject temp in temps){
            Destroy(temp);
        }
    }

    public void SetValues(){
        minX = SETMANAGER.StartPos[SETMANAGER.currentLevel] + 7;
        maxX = SETMANAGER.StartPos[SETMANAGER.NextLevels[SETMANAGER.currentLevel]] - 7;

        distBetween = SETMANAGER.SpawnInfoDistBetween[SETMANAGER.currentLevel];
        minNumCol = SETMANAGER.SpawnInfoMinCol[SETMANAGER.currentLevel];
        maxNumCol = SETMANAGER.SpawnInfoMaxCol[SETMANAGER.currentLevel];
    }
}
