using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInFight : MonoBehaviour
{
    public enum EnemyLoc { Ground, Air, Ceiling }
    public enum EnemyMove { Jump, Slide, Hammer }
    public GameObject HealthBar;

    public string enemyName;
    private int currentHP;
    private int currentDEF;
    private int currentATK;
    // TO DO - add experience

    public int maxHP;
    public int DEF;
    public int ATK;
    public EnemyLoc location;
    public string[] description;
    public EnemyMove[] moves;

    private int moveIdx;
    private bool moving = false;
    private bool done = false;
    private float counter;

    public SetManager SMR;
    void Start()
    {
        SMR = GameObject.FindWithTag("SETMANAGER").GetComponent<SetManager>();

        currentHP = maxHP;
        currentDEF = DEF;
        currentATK = ATK;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.GetComponent<EnemyHealthBar>().setHealth(currentHP, maxHP);
        
        if (moving){
            // Have turn
            transform.rotation = Quaternion.identity;
            transform.rotation *= Quaternion.Euler(0, 0, counter*240);
            counter += Time.deltaTime;

            if (counter > 1.5f){
                moving = false;
                done = true;
                GetComponent<Rigidbody>().useGravity = true;
            }
        }

        if (currentHP <= 0){
            currentHP = 0;
            // TO DO - play animation
            gameObject.SetActive(false);
        }
    }

    public bool groundHit(){
        if (location == EnemyLoc.Ground){
            return true;
        } else {
            return false;
        }
    }

    public bool jumpHit(){
        if (location == EnemyLoc.Ground || location == EnemyLoc.Air){
            return true;
        } else {
            return false;
        }
    }

    public string[] getDescription(){
        return description;
    }

    public void startTurn(){
        GetComponent<Rigidbody>().useGravity = false;
        counter = 0;
        moving = true;
        done = false;
    }

    public bool isDone(){
        return done;
    }

    public void Damage(int ATK){
        currentHP -= (ATK-DEF);
    }
}