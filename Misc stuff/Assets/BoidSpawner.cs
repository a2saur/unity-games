using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public int numBoids = 50;
    public GameObject boid;
    public float maxX = 8;
    public float maxY = 6;

    void Start()
    {
        float x;
        float y;
        for (int i = 0; i < numBoids; i++){
            x = (Random.value*maxX*2)-maxX;
            y = (Random.value*maxY*2)-maxY;
            Instantiate(boid, new Vector3(x, y, 0), Quaternion.identity);
        }
    }
}