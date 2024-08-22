using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int levelNum;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        if (Controller.numStrands >= levelNum){
            button.interactable = true;
        } else {
            button.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectLvl(){
        SceneManager.LoadScene("Level"+levelNum.ToString());
    }
}
