using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class FightOptionSelector : MonoBehaviour
{
    public GameObject mc;
    public GameObject sc;
    public GameObject[] enemies;
    public float[] character_spots;
    public GameObject arrow;
    public GameObject actionImage;
    public Sprite[] actionSprites;
    public Animator damageAnimator;
    public TextMeshProUGUI damageAnimatorNum;
    public Vector3 actionSpot;
    public GameObject canvasObj;
    public GameObject HealthBarObj;

    public GameObject mc_options;
    public GameObject sc_options_spot;
    public GameObject sub_options;

    public GameObject[] options;
    public string[] optionNames;
    public GameObject[] sc_options;
    public string[] sc_optionNames;
    public Vector3[] spots; // should be 9
    public GameObject selector;
    public TextMeshProUGUI selectionTitle;
    public TextMeshProUGUI selectionOptions;

    private int offset = 0;
    private float speed = 2.5f;
    private bool mc_selecting1 = true;
    private bool mc_selecting2 = false;
    private bool mc_turn_done = false;
    private bool sc_selecting1 = false;
    private bool sc_selecting2 = false;
    private bool sc_turn_done = false;

    public string[] sub_optionNames;
    private string mc_selected1 = "";
    private string mc_selected2 = "";
    private string sc_selected1 = "";
    private string sc_selected2 = "";

    private bool mc_targeted = false;
    private bool sc_targeted = false;
    private int mc_target = 0;
    private int sc_target = 0;

    private int idx = 0;

    private int currentMotion = 0;
    private float counter = 0;
    private bool actionStarted = false;
    private bool arrowTargeting = false;
    private List<int> validEnemies;
    private int enemyAtkIdx = 0;
    private bool enemyStarted = false;

    public SetManager SMR;
    public GameObject alertBox;
    void Start()
    {
        SMR = GameObject.FindWithTag("SETMANAGER").GetComponent<SetManager>();
        alertBox = GameObject.FindWithTag("Alert");
        alertBox.SetActive(false);

        sc.transform.position = new Vector3 (character_spots[0], 0, 0);
        mc.transform.position = new Vector3 (character_spots[1], 0, 0);
        for (int i = 0; i < enemies.Length; i++){
            enemies[i].transform.position = new Vector3 (character_spots[i+2], enemies[i].transform.position.y, 0);
            enemies[i].GetComponent<EnemyInFight>().HealthBar = Instantiate(HealthBarObj);
            enemies[i].GetComponent<EnemyInFight>().HealthBar.transform.SetParent(canvasObj.transform);
            enemies[i].GetComponent<EnemyInFight>().HealthBar.GetComponent<RectTransform>().localPosition = new Vector3 (character_spots[i+2]*48, -125, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Images
        if (mc_selecting1){
            mc_options.SetActive(true);
        } else {
            mc_options.SetActive(false);
        }

        if (sc_selecting1){
            sc_options_spot.SetActive(true);
        } else {
            sc_options_spot.SetActive(false);
        }
        
        if (sc_selecting1 || sc_selecting2 || (sc_selected2 != "" && !sc_turn_done)){
            if (sc_selected2 != "" && !sc_turn_done && sc_targeted) {
                mc.transform.position = new Vector3(character_spots[0], mc.transform.position.y, mc.transform.position.z);
            } else {
                mc.transform.position = new Vector3(character_spots[0], mc.transform.position.y, mc.transform.position.z);
                sc.transform.position = new Vector3(character_spots[1], mc.transform.position.y, mc.transform.position.z);
            }
        } else if (!(mc_selected2 != "" && !mc_turn_done)){
            mc.transform.position = new Vector3(character_spots[1], mc.transform.position.y, mc.transform.position.z);
            sc.transform.position = new Vector3(character_spots[0], mc.transform.position.y, mc.transform.position.z);
        }

        if (mc_selecting2 || sc_selecting2){
            sub_options.SetActive(true);
        } else {
            sub_options.SetActive(false);
        }


        // Targeting
        arrowTargeting = false;
        if (mc_selected1 == "Jump" || mc_selected1 == "Hammer" || sc_selected1 == "Normal" || sc_selected1 == "Specials"){
            if ((mc_selected2 != "" && !mc_turn_done && !mc_targeted) || (sc_selected2 != "" && !sc_turn_done && !sc_targeted)){
                arrowTargeting = true;
            }
        }

        if (arrowTargeting){
            arrow.SetActive(true);
        } else {
            arrow.SetActive(false);
        }

        validEnemies = new List<int>();
        if (!mc_turn_done){
            if (mc_selected1 == "Hammer"){
                for (int i = 0; i < enemies.Length; i++){
                    if (enemies[i].activeSelf){
                        validEnemies.Add(i);
                        break;
                    }
                }
            } if (mc_selected1 == "Jump"){
                for (int i = 0; i < enemies.Length; i++){
                    if (enemies[i].activeSelf && enemies[i].GetComponent<EnemyInFight>().jumpHit()){
                        validEnemies.Add(i);
                    }
                }
            }
        } else {
            if (get_sc_moveType() == "Hammer"){
                for (int i = 0; i < enemies.Length; i++){
                    if (enemies[i].activeSelf){
                        validEnemies.Add(i);
                        break;
                    }
                }
            } if (get_sc_moveType() == "Jump"){
                for (int i = 0; i < enemies.Length; i++){
                    if (enemies[i].activeSelf && enemies[i].GetComponent<EnemyInFight>().jumpHit()){
                        validEnemies.Add(i);
                    }
                }
            }
        }

        // Action Icon
        actionImage.SetActive(false);
        if (currentMotion != 0 && currentMotion != -1){
            actionImage.SetActive(true);
        }

        // Choices
        if (mc_selected2 != "" && !mc_turn_done){
            // mc turn
            if (mc_targeted) {
                // action - move to action spot, then use parabola
                if (currentMotion == 0){
                    // slide
                    float distChange = (character_spots[1] - actionSpot.x);
                    mc.transform.position -= new Vector3(distChange, 0, 0) * Time.deltaTime;
                    // mc.transform.position = new Vector3(mc.transform.position.x-distChange, mc.transform.position.y, mc.transform.position.z);// * Time.deltaTime;
                    if (Mathf.Abs(mc.transform.position.x - actionSpot.x) < 0.25f){
                        if (mc_selected1 == "Jump"){
                            currentMotion = 1;
                        } else if (mc_selected1 == "Hammer"){
                            currentMotion = 3;
                            counter = 0;
                        }
                    }
                } else if (currentMotion == 1){
                    // parabola
                    mc.GetComponent<Rigidbody>().useGravity = false;
                    
                    float distChange = 3f;
                    float ySpot = -((mc.transform.position.x-actionSpot.x)*(mc.transform.position.x-character_spots[validEnemies[mc_target]+2])) + 1;
                    // formula -> y = -(x-actionspot)(x-targetspot) + height

                    mc.transform.position = new Vector3(mc.transform.position.x+(distChange*Time.deltaTime), ySpot, mc.transform.position.z);
                    
                    Vector3 enemyPos = new Vector3(character_spots[validEnemies[mc_target]+2], -0.5f, 0);
                    Vector3 mcPos = new Vector3(mc.transform.position.x, mc.transform.position.y, 0);
                    if (Vector3.Distance(mcPos, enemyPos) < 2.5f){
                        // Debug.Log("Action!");
                        // actionImage.SetActive(true);
                        actionImage.GetComponent<Image>().sprite = actionSprites[1];
                        if (Input.GetKeyDown(SMR.jumpButton)){
                            mc_damageEnemy(SMR.ATK);
                            // Action command successful
                            Debug.Log("Yay!");
                            currentMotion = 2;
                            counter = 0;
                        }
                    } else {
                        actionImage.GetComponent<Image>().sprite = actionSprites[0];
                    }
                    if (mc.transform.position.x > character_spots[validEnemies[mc_target]+2]){
                        mc_damageEnemy(SMR.ATK);
                        currentMotion = -1;
                    }
                } else if (currentMotion == 2){
                    // jump
                    counter += (Time.deltaTime)*2;
                    float ySpot = (-((counter-1)*(counter-1)) + 2)*2;
                    mc.transform.position = new Vector3(mc.transform.position.x, ySpot, mc.transform.position.z);

                    if (ySpot < 1.5f){
                        // Debug.Log("Action!");
                        // actionImage.SetActive(true);
                        actionImage.GetComponent<Image>().sprite = actionSprites[1];
                        if (mc_selected2 == "Super Jump" && Input.GetKeyDown(SMR.jumpButton)){
                            // TO DO - check if infinite jump or single jump + display damage
                            // Action command successful
                            Debug.Log("Yay!");
                            currentMotion = 2;
                            counter = 0;
                            mc_damageEnemy(SMR.ATK);
                        }
                    } else {
                        actionImage.GetComponent<Image>().sprite = actionSprites[0];
                    }

                    if (ySpot < 1){
                        currentMotion = -1;
                        mc_damageEnemy(SMR.ATK);
                    }
                } else if (currentMotion == 3){
                    // Hammer
                    if (actionStarted){
                        counter += Time.deltaTime;
                        if (2.75f < counter && counter < 3.15f){
                            // actionImage.SetActive(true);
                            actionImage.GetComponent<Image>().sprite = actionSprites[3];
                        } else {
                            // actionImage.SetActive(false);
                            actionImage.GetComponent<Image>().sprite = actionSprites[2];
                        } // TO DO - change action image and change from showing/hiding to changing image

                        if (Input.GetKeyUp(SMR.leftArrow)){
                            actionStarted = false;
                            // Check if timing is correct
                            if (2.75f < counter && counter < 3.15f){
                                Debug.Log("Yay");
                                currentMotion = -1;
                                mc_damageEnemy((SMR.ATK)*2);
                            } else {
                                // Missed
                                currentMotion = -1;
                                mc_damageEnemy(SMR.ATK);
                            }
                        }

                        if (counter > 3.15f) {
                            currentMotion = -1;
                        }
                    } else if (Input.GetKeyDown(SMR.leftArrow)){
                        actionStarted = true;
                    }
                } else if (currentMotion == -1){
                    // return
                    Vector3 temp = new Vector3(character_spots[1], 0, 0);
                    mc.transform.position = Vector3.Lerp(mc.transform.position, temp, speed * Time.deltaTime);

                    if (Mathf.Abs(mc.transform.position.x-character_spots[1]) < 0.5f){
                        // turn done
                        if (!sc_turn_done){
                            sc_selecting1 = true;
                        }

                        mc_turn_done = true;
                        mc.GetComponent<Rigidbody>().useGravity = true;
                    }
                }
            } else {
                // select target
                // TO DO - check if figthing enemy (jump or hammer) or using item or something
                if (arrowTargeting){
                    arrow.GetComponent<RectTransform>().localPosition = new Vector3(character_spots[validEnemies[mc_target]+2]*52, 50, 0);
                    if (Input.GetKeyDown(SMR.rightArrow)){
                        if (mc_target < validEnemies.Count-1){
                            mc_target++;
                        }
                    } if (Input.GetKeyDown(SMR.leftArrow)){
                        if (mc_target > 0){
                            mc_target--;
                        }
                    }

                    if (Input.GetKeyDown(SMR.interactButton)){
                        mc_targeted = true;
                        currentMotion = 0;
                        counter = 0;
                        actionStarted = false;
                    }

                    if (Input.GetKeyDown(SMR.backButton)){
                        mc_selected2 = "";
                        mc_selecting2 = true;
                    }
                } else {
                    // do something else
                }
            }
        } else if (sc_selected2 != "" && !sc_turn_done){
            // sc turn
            if (sc_targeted) {
                // action - move to action spot, then use parabola
                if (currentMotion == 0){
                    // slide
                    float distChange = (character_spots[1] - actionSpot.x);
                    sc.transform.position -= new Vector3(distChange, 0, 0) * Time.deltaTime;
                    // sc.transform.position = new Vector3(sc.transform.position.x-distChange, sc.transform.position.y, sc.transform.position.z);// * Time.deltaTime;
                    if (Mathf.Abs(sc.transform.position.x - actionSpot.x) < 0.25f){
                        if (get_sc_moveType() == "Jump"){
                            currentMotion = 1;
                        } else if (get_sc_moveType() == "Hammer"){
                            currentMotion = 3;
                            counter = 0;
                        }
                    }
                } else if (currentMotion == 1){
                    // parabola
                    sc.GetComponent<Rigidbody>().useGravity = false;
                    
                    float distChange = 3f;
                    float ySpot = -((sc.transform.position.x-actionSpot.x)*(sc.transform.position.x-character_spots[validEnemies[sc_target]+2])) + 1;
                    // formula -> y = -(x-actionspot)(x-targetspot) + height

                    sc.transform.position = new Vector3(sc.transform.position.x+(distChange*Time.deltaTime), ySpot, sc.transform.position.z);
                    
                    Vector3 enemyPos = new Vector3(character_spots[validEnemies[sc_target]+2], -0.5f, 0);
                    Vector3 scPos = new Vector3(sc.transform.position.x, sc.transform.position.y, 0);
                    if (Vector3.Distance(scPos, enemyPos) < 2.5f){
                        actionImage.GetComponent<Image>().sprite = actionSprites[1];
                        if (Input.GetKeyDown(SMR.jumpButton)){
                            // Action command successful
                            Debug.Log("Yay!");
                            currentMotion = 2;
                            counter = 0;
                            sc_damageEnemy(SMR.ATK);
                        }
                    } else {
                        actionImage.GetComponent<Image>().sprite = actionSprites[0];
                    }
                    if (sc.transform.position.x > character_spots[validEnemies[sc_target]+2]){
                        sc_damageEnemy(SMR.ATK);
                        currentMotion = -1;
                    }
                } else if (currentMotion == 2){
                    // jump
                    counter += (Time.deltaTime)*2;
                    float ySpot = (-((counter-1)*(counter-1)) + 2)*2;
                    sc.transform.position = new Vector3(sc.transform.position.x, ySpot, sc.transform.position.z);

                    if (ySpot < 1.5f){
                        // Debug.Log("Action!");
                        // actionImage.SetActive(true);
                        actionImage.GetComponent<Image>().sprite = actionSprites[1];
                        if (sc_selected2 == "Super Jump" && Input.GetKeyDown(SMR.jumpButton)){ // TO DO - change this for actual moves for sc
                            // Action command successful
                            sc_damageEnemy(SMR.ATK);
                            Debug.Log("Yay!");
                            currentMotion = 2;
                            counter = 0;
                        }
                    } else {
                        actionImage.GetComponent<Image>().sprite = actionSprites[0];
                    }

                    if (ySpot < 1){
                        sc_damageEnemy(SMR.ATK);
                        currentMotion = -1;
                    }
                } else if (currentMotion == 3){
                    // Hammer
                    if (actionStarted){
                        counter += Time.deltaTime;
                        if (2.75f < counter && counter < 3.15f){
                            // actionImage.SetActive(true);
                            actionImage.GetComponent<Image>().sprite = actionSprites[3];
                        } else {
                            // actionImage.SetActive(false);
                            actionImage.GetComponent<Image>().sprite = actionSprites[2];
                        } // TO DO - change action image and change from showing/hiding to changing image

                        if (Input.GetKeyUp(SMR.leftArrow)){
                            actionStarted = false;
                            // Check if timing is correct
                            if (2.75f < counter && counter < 3.15f){
                                Debug.Log("Yay");
                                sc_damageEnemy((SMR.ATK)*2);
                                currentMotion = -1;
                            } else {
                                // Missed
                                sc_damageEnemy(SMR.ATK);
                                currentMotion = -1;
                            }
                        }

                        if (counter > 3.15f) {
                            currentMotion = -1;
                        }
                    } else if (Input.GetKeyDown(SMR.leftArrow)){
                        actionStarted = true;
                    }
                } else if (currentMotion == -1){
                    // return
                    Vector3 temp = new Vector3(character_spots[1], 0, 0);
                    sc.transform.position = Vector3.Lerp(sc.transform.position, temp, speed * Time.deltaTime);

                    if (Mathf.Abs(sc.transform.position.x-character_spots[1]) < 0.5f){
                        // turn done
                        if (!mc_turn_done){
                            mc_selecting1 = true;
                        }

                        sc_turn_done = true;
                        sc.GetComponent<Rigidbody>().useGravity = true;
                    }
                }
            } else {
                // select target
                // TO DO - check if figthing enemy (jump or hammer) or using item or something
                if (arrowTargeting){
                    arrow.GetComponent<RectTransform>().localPosition = new Vector3(character_spots[validEnemies[sc_target]+2]*52, 50, 0);
                    if (Input.GetKeyDown(SMR.rightArrow)){
                        if (sc_target < validEnemies.Count-1){
                            sc_target++;
                        }
                    } if (Input.GetKeyDown(SMR.leftArrow)){
                        if (sc_target > 0){
                            sc_target--;
                        }
                    }

                    if (Input.GetKeyDown(SMR.interactButton)){
                        sc_targeted = true;
                        currentMotion = 0;
                        counter = 0;
                        actionStarted = false;
                    }

                    if (Input.GetKeyDown(SMR.backButton)){
                        sc_selected2 = "";
                        sc_selecting2 = true;
                    }
                } else {
                    // do something else
                }
            }
        } else if (mc_selecting1) {
            // Debug.Log(offset);
            for (int i = 0; i < options.Length; i++){
                // options[i].GetComponent<RectTransform>().localPosition = spots[i+offset+2];
                options[i].GetComponent<RectTransform>().localPosition = Vector3.Lerp(options[i].GetComponent<RectTransform>().localPosition, spots[i+offset+2], speed * Time.deltaTime);
            }

            if (Input.GetKeyDown(SMR.rightArrow)){
                if (offset > -2){
                    offset--;
                }
            } if (Input.GetKeyDown(SMR.leftArrow)){
                if (offset < 2){
                    offset++;
                }
            }

            if (Input.GetKeyDown(SMR.interactButton)){
                mc_selecting1 = false;
                mc_selecting2 = true;

                mc_selected1 = optionNames[2-offset];
            }

            if (Input.GetKeyDown(SMR.swapButton)){
                if (!sc_turn_done){
                    mc_selecting1 = false;
                    sc_selecting1 = true;
                    offset = 0;
                } else {
                    // TO DO - show alert saying that that character has already gone
                    alertBox.SetActive(true);
                    alertBox.GetComponent<Alert>().SetAlert("That character has already gone!");
                }
            }
        } else if (mc_selecting2) {
            // Setting Text
            selectionTitle.text = mc_selected1;
            if (mc_selected1 == "Strategies") {
                sub_optionNames = new string[]{
                    // "Change Member",
                    "Do Nothing",
                    "Run Away",
                };
            } else if (mc_selected1 == "Items") {
                // sub_optionNames = SMR.items; TO DO - get names of items
                sub_optionNames = new string[]{
                    "Items"
                };
            } else if (mc_selected1 == "Jump") {
                sub_optionNames = new string[]{
                    "Jump",
                    "Multi Jump",
                    "Super Jump",
                };
            } else if (mc_selected1 == "Hammer") {
                sub_optionNames = new string[]{
                    "Thwack",
                    "Quake Thwack",
                };
            } else if (mc_selected1 == "Stars") {
                sub_optionNames = new string[]{
                    "Special"
                };
            }

            // Choosing Buttons
            if (sub_optionNames.Length == 0){
                // TO DO - add alert if there isn't any options
                alertBox.SetActive(true);
                alertBox.GetComponent<Alert>().SetAlert("There aren\'t any available options");
            } else {
                if (Input.GetKeyDown(SMR.upArrow)){
                    if (idx > 0){
                        idx--;
                    }
                } if (Input.GetKeyDown(SMR.downArrow)){
                    if (idx < sub_optionNames.Length-1){
                        idx++;
                    }
                }
            }

            // Setting Text
            string buttonTextTemp = "";
            for (int i = 0; i < sub_optionNames.Length; i++){
                if (i == idx){
                    buttonTextTemp += "> ";
                } else {
                    buttonTextTemp += "  ";
                }
                buttonTextTemp += sub_optionNames[i];
                buttonTextTemp += "\n";
            }
            selectionOptions.text = buttonTextTemp;

            if (Input.GetKeyDown(SMR.interactButton)){
                mc_selecting2 = false;
                // if (!sc_turn_done){
                //     sc_selecting1 = true;
                // }
                mc_selected2 = sub_optionNames[idx];
                
                idx = 0;
                offset = 0;
                mc_target = 0;
            }

            if (Input.GetKeyDown(SMR.backButton)){
                mc_selecting1 = true;
                mc_selecting2 = false;
            }
        } else if (sc_selecting1) {
            for (int i = 0; i < sc_options.Length; i++){
                // options[i].GetComponent<RectTransform>().localPosition = spots[i+offset+2];
                sc_options[i].GetComponent<RectTransform>().localPosition = Vector3.Lerp(sc_options[i].GetComponent<RectTransform>().localPosition, spots[i+offset+2], speed * Time.deltaTime);
            }

            if (Input.GetKeyDown(SMR.rightArrow)){
                if (offset > -1){
                    offset--;
                }
            } if (Input.GetKeyDown(SMR.leftArrow)){
                if (offset < 2){
                    offset++;
                }
            }

            if (Input.GetKeyDown(SMR.interactButton)){
                sc_selecting1 = false;
                sc_selecting2 = true;

                sc_selected1 = sc_optionNames[2-offset];
            }

            if (Input.GetKeyDown(SMR.swapButton)){
                if (!mc_turn_done){
                    sc_selecting1 = false;
                    mc_selecting1 = true;
                    offset = 0;
                } else {
                    // TO DO - show alert that character has already gone
                    alertBox.SetActive(true);
                    alertBox.GetComponent<Alert>().SetAlert("That character has already gone!");
                }
            }
        } else if (sc_selecting2) {
            // Setting Text
            selectionTitle.text = sc_selected1;
            if (sc_selected1 == "Strategies") {
                sub_optionNames = new string[]{
                    "Do Nothing",
                    // "Run Away",
                };
            } else if (sc_selected1 == "Normal") {
                // sub_optionNames = SMR.items; TO DO - get names of items
                sub_optionNames = new string[]{
                    "Shell Throw",
                    "Shell Hitter",
                    "Info Grab",
                    "Hide",
                    "Charge",
                };
            } else if (sc_selected1 == "Specials") {
                sub_optionNames = new string[]{
                    "Power Shell Hitter",
                    "Power Shell Throw",
                    "Shell Barage",
                    "Super Shell Barage",
                    "Shell Cover",
                };
            } else if (sc_selected1 == "Stars") {
                sub_optionNames = new string[]{
                    "Special"
                };
            }

            // Choosing Buttons
            if (sub_optionNames.Length == 0){
                // TO DO - add alert if there isn't any options
                alertBox.SetActive(true);
                    alertBox.GetComponent<Alert>().SetAlert("There aren\'t any available options!");
            } else {
                if (Input.GetKeyDown(SMR.upArrow)){
                    if (idx > 0){
                        idx--;
                    }
                } if (Input.GetKeyDown(SMR.downArrow)){
                    if (idx < sub_optionNames.Length-1){
                        idx++;
                    }
                }
            }

            // Setting Text
            string buttonTextTemp = "";
            for (int i = 0; i < sub_optionNames.Length; i++){
                if (i == idx){
                    buttonTextTemp += "> ";
                } else {
                    buttonTextTemp += "  ";
                }
                buttonTextTemp += sub_optionNames[i];
                buttonTextTemp += "\n";
            }
            selectionOptions.text = buttonTextTemp;

            if (Input.GetKeyDown(SMR.interactButton)){
                sc_selecting2 = false;
                // if (!mc_turn_done){
                //     mc_selecting1 = true;
                // }
                sc_selected2 = sub_optionNames[idx];

                idx = 0;
                offset = 0;
                sc_target = 0;
            }

            if (Input.GetKeyDown(SMR.backButton)){
                sc_selecting1 = true;
                sc_selecting2 = false;
            }
        } else {
            // enemy turn
            if (!enemies[enemyAtkIdx].activeSelf){
                enemyAtkIdx++;
            }

            if (!enemyStarted){
                enemies[enemyAtkIdx].GetComponent<EnemyInFight>().startTurn();
                enemyStarted = true;
            } else {
                if (enemies[enemyAtkIdx].GetComponent<EnemyInFight>().isDone()){
                    enemyAtkIdx++;
                    enemyStarted = false;
                }
            }
            
            if (enemyAtkIdx >= enemies.Length){
                // TO DO - change this to non-defeated enemies

                enemyAtkIdx = 0;
                mc_selecting1 = true;
                mc_selecting2 = false;
                mc_turn_done = false;
                sc_selecting1 = false;
                sc_selecting2 = false;
                sc_turn_done = false;

                mc_selected1 = "";
                mc_selected2 = "";
                sc_selected1 = "";
                sc_selected2 = "";

                mc_targeted = false;
                sc_targeted = false;
                mc_target = 0;
                sc_target = 0;
            }
        }
    }

    string get_sc_moveType(){
        if (sc_selected2 == "Shell Throw"){
            return "Jump";
        } if (sc_selected2 == "Shell Hitter"){
            return "Hammer";
        } if (sc_selected2 == "Power Shell Throw"){
            return "Jump";
        } if (sc_selected2 == "Power Shell Hitter"){
            return "Hammer";
        }
        // TO DO - add options

        return "Other";
    }

    void mc_damageEnemy(int dmgAmount){
        enemies[validEnemies[mc_target]].GetComponent<EnemyInFight>().Damage(dmgAmount);
        damageAnimator.GetComponent<RectTransform>().localPosition = new Vector3(character_spots[validEnemies[mc_target]+2]*52, 50, 0);
        damageAnimatorNum.text = dmgAmount.ToString();
        damageAnimator.Play("DamageNumber");
    }

    void sc_damageEnemy(int dmgAmount){
        enemies[validEnemies[sc_target]].GetComponent<EnemyInFight>().Damage(dmgAmount);
        damageAnimatorNum.text = dmgAmount.ToString();
        damageAnimator.GetComponent<RectTransform>().localPosition = new Vector3(character_spots[validEnemies[sc_target]+2]*52, 50, 0);
        damageAnimator.Play("DamageNumber");
    }
}