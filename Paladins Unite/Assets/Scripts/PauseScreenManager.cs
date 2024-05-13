using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseScreenManager : MonoBehaviour
{
    public GameObject pausePanel;

    public GameObject[] buttonList;
    public int selectedIdx = 0;

    void Start(){
        pausePanel.SetActive(SettingsManager.pauseScreen);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(SettingsManager.keyLabels["pauseButton"])) {
            SettingsManager.pauseScreen = !SettingsManager.pauseScreen;
            pausePanel.SetActive(SettingsManager.pauseScreen);

            if (SettingsManager.pauseScreen){
                selectedIdx = 0;
            }
        }


        if (SettingsManager.pauseScreen){
            if (selectedIdx >= buttonList.Length){
                selectedIdx = 0;
            } else if (selectedIdx < 0) {
                selectedIdx = buttonList.Length-1;
            } else {
                buttonList[selectedIdx].GetComponent<Button>().Select();
            }

            if (Input.GetKeyDown(SettingsManager.keyLabels["downArrow"]) || Input.GetKeyDown(SettingsManager.keyLabels["rightArrow"])) {
                selectedIdx++;
            } if (Input.GetKeyDown(SettingsManager.keyLabels["upArrow"]) || Input.GetKeyDown(SettingsManager.keyLabels["leftArrow"])) {
                selectedIdx--;
            }
        }
    }

    public void ResumeScreen(){
        SettingsManager.pauseScreen = false;
        pausePanel.SetActive(false);
    }
}
