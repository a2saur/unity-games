using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;

public class Town : MonoBehaviour
{
    public HouseAttributes houseBase;
    public string TownName;
    public List<HouseAttributes> houses;
    public bool inTown;

    private static string alphabet = "abcdefghijklmnopqrstuvwxyz";

    public GameObject inventoryObject;
    public int randomSeed;

    public Animator transitionAnimator;
    // Start is called before the first frame update
    void Start()
    {
        transitionAnimator = GameObject.FindGameObjectsWithTag("SceneTransition")[0].GetComponent<Animator>();
        inventoryObject = GameObject.FindGameObjectsWithTag("Inventory")[0];
        // randomSeed = inventoryObject.GetComponent<Inventory>().seed;
        // UnityEngine.Random.InitState(randomSeed);

        DontDestroyOnLoad(this.gameObject);

        TownName = CreateName();
        inTown = false;

        int idx;
        for (int i = 0; i < 5; i++){
            idx = UnityEngine.Random.Range(0, inventoryObject.GetComponent<Inventory>().houseOptions.Count);
            houses.Add(inventoryObject.GetComponent<Inventory>().houseOptions[idx]);
            Debug.Log(idx);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "MainScene") {
            // Not supposed to be displayed
            GetComponent<Renderer>().enabled = false;
        } else {
            GetComponent<Renderer>().enabled = true;
        }
    }

    void OnCollisionEnter2D(Collision2D hit){
        if (hit.gameObject.tag == "Player") {
            if (inTown){
                inTown = false;
                transitionAnimator.SetTrigger("SceneTransition");
                StartCoroutine(DelayedSceneChange(1f, "MainScene"));
                // SceneManager.LoadScene("MainScene");
                this.gameObject.tag = "Untagged";
            } else {
                // SceneManager.LoadScene("TownTest");
                transitionAnimator.SetTrigger("SceneTransition");
                StartCoroutine(DelayedSceneChange(1f, "TownTest"));
                inTown = true;
                this.gameObject.tag = "ActiveTown";
            }
        }
    }

    public static string CreateName()
    {
        int nameLen = UnityEngine.Random.Range(4, 9);

        string[] examples = new string[] {
            "sika", "yukon", "adak", "vira", "vasa", "lupa", "orca", "sarps",
            "aren", "oslo", "bergen", "tasil", "keflak", "akran", "neska",
            "sedis", "lonsor", "sleiden", "vervi", "rukan", "byrn", "kavik", // original
            "reyka", "eyra", "hafn", "naner", "keflit", "gardor", "starfell",
            "mojal", "jyavek", "wanseik", "valor"
        };

        Dictionary<char, List<char>> letterProb = new Dictionary<char, List<char>>();

        foreach (string word in examples) {
            for (int x = 0; x < word.Length; x++) {
                try {
                    letterProb[word[x]].Add(word[x + 1]);
                } catch (KeyNotFoundException) {
                    try {
                        letterProb[word[x]] = new List<char>() { word[x + 1] };
                    } catch (IndexOutOfRangeException) {
                        // Do nothing
                    }
                } catch (IndexOutOfRangeException) {
                    // Do nothing
                }
            }
        }

        string newPlace = "";

        for (int x = 0; x < nameLen; x++) {
            try {
                newPlace += letterProb[newPlace[newPlace.Length - 1]][UnityEngine.Random.Range(0, letterProb[newPlace[newPlace.Length - 1]].Count)];
            } catch (IndexOutOfRangeException) {
                newPlace += alphabet[UnityEngine.Random.Range(0, alphabet.Length)];
            } catch (KeyNotFoundException) {
                newPlace += alphabet[UnityEngine.Random.Range(0, alphabet.Length)];
            }
        }

        Debug.Log("Named!");

        return char.ToUpper(newPlace[0]) + newPlace.Substring(1);
    }

    IEnumerator DelayedSceneChange(float delay, string sceneToLoad){
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(sceneToLoad);
    }
}