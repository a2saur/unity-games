using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableOptButton : MonoBehaviour
{
    public TextMeshProUGUI buttonName;
    public TextMeshProUGUI buttonLabel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetButtonName(KeyCode buttonCode){
        buttonName.text = buttonCode.ToString();
    }

    public void SetButtonLabel(string label){
        buttonLabel.text = label;
    }
}
