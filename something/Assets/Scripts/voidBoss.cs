using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voidBoss : MonoBehaviour
{
    public int phase = 1;
    public GameObject basicAttack;
    public GameObject pointAttack;
    public GameObject followAttack;

    private float timer = 0;
    private float counter = 0;
    private float creationTimer = 0;
    private float creationInterval = 0.25f;
    private int phaseOneMaxTime = 15;
    private int phaseTwoMaxTime = 15;
    private int phaseThreeMaxTime = 15;
    private bool secPassed;
    private bool creation;

    private Animator anim;
    private Collider2D col;

    private List<GameObject> attacks = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("fallen")){
            col.enabled = true;
        } else {
            col.enabled = false;
        }
        
        // Timer Handling
        timer += Time.deltaTime;
        counter += Time.deltaTime;
        if (timer > 1){
            secPassed = true;
            timer = 0;
        } else {
            secPassed = false;
        }

        creationTimer += Time.deltaTime;
        if (phase == 1 && counter > phaseOneMaxTime){ // loop every 20 seconds
            counter = 0;
        } if (phase == 2 && counter > phaseTwoMaxTime){ // loop every 20 seconds
            counter = 0;
        } if (phase == 3 && counter > phaseThreeMaxTime){ // loop every 20 seconds
            counter = 0;
        }
        
        // Scale handling
        if (phase == 1){
            transform.localScale = new Vector3(1, 1, 1);
        } if (phase == 2){
            transform.localScale = new Vector3(2, 2, 2);
        } if (phase == 3){
            transform.localScale = new Vector3(5, 5, 5);
            transform.position = new Vector3(transform.position.x, transform.position.y, 1.5f);
        }

        // Create attacks
        if (creationTimer > creationInterval){
            creationTimer = 0;
            creation = true;
        } else {
            creation = false;
        }

        transform.position = new Vector3 (TimePositionWiggly2(counter+5)+2.5f, TimePositionUpWDownW(counter)+1, transform.position.z);
        if (phase == 1){
            if ((int) counter < 10){
                anim.SetBool("fallen", false);
                if (creation){
                    // spout basic attacks
                    // create attack
                    GameObject attack = Instantiate(basicAttack, transform.position, Quaternion.identity);
                    attack.GetComponent<bossNormalAttack>().SetDirectionTime(timer, (float) phaseOneMaxTime);
                    attacks.Add(attack);
                }
            } else {
                // wait
                anim.SetBool("fallen", true);
            }
        } if (phase == 2){
            if ((int) counter < 10){
                anim.SetBool("fallen", false);
                if ((int) counter % 3 == 0 && creation){
                    // spout pointing attacks
                    // create attack
                    for (int i = 0; i < 2; i++){
                        GameObject attack = Instantiate(pointAttack, OffsetVector(transform.position, 2), Quaternion.identity);
                        attacks.Add(attack);
                }
            }
            } else {
                // wait
                anim.SetBool("fallen", true);
            }
        } if (phase == 3){
            if ((int) counter < 10){
                anim.SetBool("fallen", false);
                if ((int) counter % 3 == 0 && secPassed){
                    // spout follow attacks
                    // create attack
                    for (int i = 0; i < 3; i++){
                        GameObject attack = Instantiate(followAttack, OffsetVector(transform.position, 2), Quaternion.identity);
                        attacks.Add(attack);
                    }
                } else if ((int) counter % 5 == 0 && secPassed){
                    // spout follow attacks
                    // create attack
                    for (int i = 0; i < 5; i++){
                        GameObject attack = Instantiate(pointAttack, OffsetVector(transform.position, 2), Quaternion.identity);
                        attacks.Add(attack);
                    }
            }
            } else {
                // wait, vulnerablec
                anim.SetBool("fallen", true);
            }
        }

        // Check if attacks are far away
        for (int i = attacks.Count - 1; i >= 0; i--) // Loop backward to avoid index issues when removing
        {
            GameObject obj = attacks[i];
            float distance = Vector3.Distance(transform.position, obj.transform.position);

            // If the distance is 5 or more, remove and destroy the object
            if (distance >= 10f){
                attacks.RemoveAt(i); // Remove from the list
                Destroy(obj);            // Destroy the object in the scene
            } else if (obj.transform.localScale.x < 0.05){
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

    // -1 : 1
    // sin(x) + 0.5cos(x) + 2cos(x + sin(x))
    float TimePositionSlopeSlant(float x){
        float s = Mathf.Sin(x);
        float c = Mathf.Cos(x);
        float ans = s + c + Mathf.Cos(x + s);
        return (0.5f*ans);
    }

    // 1 : 2
    // sin(cos(cos^2(x))) * 2
    float TimePositionFlatDips(float x){
        float ans = Mathf.Sin(Mathf.Cos(Mathf.Cos(x)*Mathf.Cos(x)))*2;
        return ans;
    }

    // 0 : 1
    // sin(x/2)+1
    float TimePositionSin(float x){
        float ans = Mathf.Sin(x/2);
        return ans;
    }

    // -1 : 3
    // sin(x)*cos(2x)+cos(x/2)+1
    float TimePositionWiggly(float x){
        float ans = Mathf.Sin(x)*Mathf.Cos(2*x)+Mathf.Cos(x/2)+1;
        return ans;
    }

    // 1 : 2
    // (sin(e^x)*cos(x))/x
    float TimePositionAAABlink(float x){
        float ans = (Mathf.Sin(Mathf.Exp(x))*Mathf.Cos(x))/x;
        if (ans > 0){
            return 2;
        } else {
            return 1;
        }
    }

    // ???
    // abs | (sin(e^x)*cos(x))/x |
    float TimePositionAAA(float x){
        float ans = (Mathf.Sin(Mathf.Exp(x))*Mathf.Cos(x))/x;
        ans += 1;
        if (ans > 2){
            return 2;
        } else if (ans < -2){
            return -2;
        } else {
            return ans;
        }
    }

    // -1 : 1
    // sin(x + cos(x * 2)) - cos(x / 2)
    float TimePositionWiggly2(float x){
        float ans = Mathf.Sin(x + Mathf.Cos(x*2)) - Mathf.Cos(x/2);
        return ans/2;
    }

    // -1 : 1
    // sin(x + cos(2x))
    float TimePositionUpWDownW(float x){
        float ans = Mathf.Sin(x + Mathf.Cos(x*2));
        return ans;
    }
}
