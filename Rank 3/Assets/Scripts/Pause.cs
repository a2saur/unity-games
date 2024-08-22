using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        Controller.paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Controller.pauseButton)){
            Controller.paused = !Controller.paused;
        }

        if (Controller.paused){
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        } else {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    public void Resume(){
        Controller.paused = false;
    }
}
