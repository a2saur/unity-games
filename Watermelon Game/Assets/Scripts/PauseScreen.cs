using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseScreen : MonoBehaviour
{
    public bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public TMP_Text scoreText;
    public FruitLists dropper;

    void Start()
    {
        dropper = GameObject.FindGameObjectsWithTag("Dropper")[0].GetComponent<FruitLists>();
        Resume();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }

        scoreText.text = dropper.score.ToString();
    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}