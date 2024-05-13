using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseScreenManagerOld : MonoBehaviour
{
    public GameObject pausePanel;
    public string currentButtonLevel = "Base";
    public int selectedIdx = 0;
    public GameObject[] buttons;
    public string keyToChange = "";

    public TMP_Text[] keyBindingLabels;
    public string[] keyBindingTitles;

    private int keyWait = 5;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(SettingsManager.keyLabels["pauseButton"])) {
            SettingsManager.pauseScreen = !SettingsManager.pauseScreen;
            pausePanel.SetActive(SettingsManager.pauseScreen);
        }

        if (SettingsManager.pauseScreen){
            if (keyToChange == ""){
                buttons = GameObject.FindGameObjectsWithTag(currentButtonLevel+"Button");
                if (selectedIdx >= buttons.Length){
                    selectedIdx = 0;
                } else if (selectedIdx < 0) {
                    selectedIdx = buttons.Length-1;
                } else {
                    buttons[selectedIdx].GetComponent<Button>().Select();
                }

                if (currentButtonLevel == "Base"){
                    if (Input.GetKeyDown(SettingsManager.keyLabels["rightArrow"])) {
                        selectedIdx++;
                    } if (Input.GetKeyDown(SettingsManager.keyLabels["leftArrow"])) {
                        Debug.Log(SettingsManager.keyLabels["leftArrow"]);
                        selectedIdx--;
                    }
                } else {
                    if (Input.GetKeyDown(SettingsManager.keyLabels["downArrow"])) {
                        selectedIdx++;
                    } if (Input.GetKeyDown(SettingsManager.keyLabels["upArrow"])) {
                        selectedIdx--;
                    }
                }
                
                if (Input.GetKeyDown(SettingsManager.keyLabels["selectButton"])){
                    buttons[selectedIdx].GetComponent<Button>().onClick.Invoke();
                }

                if (Input.GetKeyDown(SettingsManager.keyLabels["backButton"])){
                    if (currentButtonLevel == "Base"){
                        // Resume
                    } else {
                        currentButtonLevel = "Base";
                    }
                }

                for (int i = 0; i < keyBindingTitles.Length; i++){
                    keyBindingLabels[i].text = SettingsManager.keyIcons[SettingsManager.keyLabels[keyBindingTitles[i]]];
                }
            } else {
                if (keyWait > 0){
                    keyWait--;
                } else {
                    for (int i = 0; i < keyBindingTitles.Length; i++){
                        if (keyBindingTitles[i] == keyToChange){
                            keyBindingLabels[i].text = "...";
                        }
                    }
                    foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
                    {
                        if (Input.GetKeyDown(kcode) && SettingsManager.keyIcons.ContainsKey(kcode)){
                            SettingsManager.keyLabels[keyToChange] = kcode;
                            keyToChange = "";
                            keyWait = 5;
                            break;
                        }
                    }
                }
            }
        }
    }

    public void PauseScreen(){
        selectedIdx = 0;
        currentButtonLevel = "Base";
    }

    public void ResumeScreen(){
        // 
    }

    public void SetButtonLevel(string newLevel){
        // disable screen
        if (currentButtonLevel != "Base"){
            GameObject.FindWithTag(currentButtonLevel+"Screen").GetComponent<Hideable>().Hide();
        }
        currentButtonLevel = newLevel;
        // Set screen active
        GameObject.FindWithTag(newLevel+"Screen").GetComponent<Hideable>().Show();
    }

    
    public void ChangeKey(string keyLabel){
        keyToChange = keyLabel;
    }
}
