using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voidBoss : MonoBehaviour
{
    public int phase = 1;
    public GameObject basicAttack;
    public GameObject pointAttack;

    private float timer = 0;
    private float creationTimer = 0;
    private float creationInterval = 0.25f;
    private int phaseOneMaxTime = 15;
    private int phaseTwoMaxTime = 15;

    private List<GameObject> attacks = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Timer Handling
        timer += Time.deltaTime;
        creationTimer += Time.deltaTime;
        if (phase == 1 && timer > phaseOneMaxTime){ // loop every 20 seconds
            timer = 0;
        } if (phase == 2 && timer > phaseTwoMaxTime){ // loop every 20 seconds
            timer = 0;
        }
        
        // Scale handling
        if (phase == 1){
            transform.localScale = new Vector3(1, 1, 1);
        } if (phase == 2){
            transform.localScale = new Vector3(2, 2, 2);
        } if (phase == 3){
            transform.localScale = new Vector3(5, 5, 5);
        }

        // Create attacks
        if (creationTimer > creationInterval){
            creationTimer = 0;
            if (phase == 1){
                if ((int)timer < 10){
                    // spout basic attacks
                    // create attack
                    GameObject attack = Instantiate(basicAttack, transform.position, Quaternion.identity);
                    attack.GetComponent<bossNormalAttack>().SetDirectionTime(timer, (float) phaseOneMaxTime);
                    attacks.Add(attack);
                } else {
                    // wait
                }
            } if (phase == 2){
                if ((int)timer < 10 && (int)timer % 5 == 0){
                    // spout basic attacks
                    // create attack
                    for (int i = 0; i < 2; i++){
                        GameObject attack = Instantiate(pointAttack, OffsetVector(transform.position, 2), Quaternion.identity);
                        attacks.Add(attack);
                    }
                } else {
                    // wait
                }
            }
        }

        // Check if attacks are far away
        for (int i = attacks.Count - 1; i >= 0; i--) // Loop backward to avoid index issues when removing
        {
            GameObject obj = attacks[i];
            float distance = Vector3.Distance(transform.position, obj.transform.position);

            // If the distance is 5 or more, remove and destroy the object
            if (distance >= 10f)
            {
                attacks.RemoveAt(i); // Remove from the list
                Destroy(obj);            // Destroy the object in the scene
            }
        }
    }

    float RoundVal(float value, int roundingPlace){
        return Mathf.Round(value * roundingPlace) / roundingPlace;
    }

    Vector3 OffsetVector(Vector3 initial, float offset){
        float dx = Random.Range(0, offset*2)-offset;
        float dy = Random.Range(0, offset*2)-offset;

        return new Vector3(initial.x+dx, initial.y+dy, initial.z);
    }
}
