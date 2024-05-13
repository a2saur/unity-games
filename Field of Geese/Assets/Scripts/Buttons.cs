using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public SetManager SETMANAGER;
    public GameObject buttons;
    public bool done = false;
    public bool choice = false;

    // Start is called before the first frame update
    void Start()
    {
        SETMANAGER = GameObject.FindWithTag("SetManager").GetComponent<SetManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NoThanks(){
        buttons.SetActive(false);
        done = true;
        choice = false;
    }

    public void Sure(){
        if (SETMANAGER.currentLevel == "Kai Batta"){
            buttons.SetActive(false);
            SceneManager.LoadScene("SandwichMaking", LoadSceneMode.Additive);

            Scene firstScene = SceneManager.GetSceneByName("SampleScene");
            GameObject[] rootObjects = firstScene.GetRootGameObjects();
            foreach (var rootObj in rootObjects)
            {
                // Set root object and all its children to inactive
                rootObj.SetActive(false);
            }
        }
        choice = true;
        done = true;
    }
}
