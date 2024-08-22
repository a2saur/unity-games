using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // public void startButton(){
    //     SceneManager.LoadScene("Level0");
    // }

    public void levelSelect(){
        SceneManager.LoadScene("LevelSelect");
    }

    // public void instructions(){
    //     SceneManager.LoadScene("Instructions");
    // }

    public void endButton(){
        Application.Quit();
    }
}
