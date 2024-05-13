using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public TextMeshProUGUI HPTxt;
    public TextMeshProUGUI SPTxt;
    public TextMeshProUGUI XPTxt;
    public TextMeshProUGUI coinsTxt;

    public SetManager SMR;
    void Start()
    {
        SMR = GameObject.FindWithTag("SETMANAGER").GetComponent<SetManager>();
    }

    // Update is called once per frame
    void Update()
    {
        HPTxt.text = SMR.currentHP.ToString() + "/" + SMR.maxHP.ToString();
        SPTxt.text = SMR.currentSP.ToString() + "/" + SMR.maxSP.ToString();
        XPTxt.text = "x" + SMR.XP.ToString();
        coinsTxt.text = "x" + SMR.coins.ToString();
    }
}
