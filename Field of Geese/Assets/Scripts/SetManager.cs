using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetManager : MonoBehaviour
{
    public bool isPaused = false;
    public string currentLevel = "Sandy Wich";
    public Dictionary<string, string> NextLevels = new Dictionary<string, string>() {
        {"Sandy Wich","Kai Batta"},
        {"Kai Batta","Cory Sant"},
        {"Cory Sant","Bree Osh"},
        {"Bree Osh","Stuart O. Bread"},
        {"Stuart O. Bread","End Scene"},
        {"End Scene","End Scene"},
    };

    public Dictionary<string, int> LevelScores = new Dictionary<string, int>() {
        {"Sandy Wich",3},
        {"Kai Batta",3},
        {"Cory Sant",3},
        {"Bree Osh",3},
        {"Stuart O. Bread",3},
        {"End Scene",3},
    };

    public Dictionary<string, float> StartPos = new Dictionary<string, float>() {
        {"Sandy Wich",-5},
        {"Kai Batta",45},
        {"Cory Sant",95},
        {"Bree Osh",155},
        {"Stuart O. Bread",205},
        {"End Scene",255}
    };
    
    public Dictionary<string, float> SpawnInfoDistBetween = new Dictionary<string, float>() {
        {"Sandy Wich",10},
        {"Kai Batta",9},
        {"Cory Sant",8},
        {"Bree Osh",7},
        {"Stuart O. Bread",7},
        {"End Scene",3}
    };

    public Dictionary<string, int> SpawnInfoMinCol = new Dictionary<string, int>() {
        {"Sandy Wich",1},
        {"Kai Batta",1},
        {"Cory Sant",1},
        {"Bree Osh",1},
        {"Stuart O. Bread",2},
        {"End Scene",2}
    };

    public Dictionary<string, int> SpawnInfoMaxCol = new Dictionary<string, int>() {
        {"Sandy Wich",2},
        {"Kai Batta",2},
        {"Cory Sant",2},
        {"Bree Osh",3},
        {"Stuart O. Bread",3},
        {"End Scene",5}
    };
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sandwichCleared(){
        LevelScores["Cory Sant"] = 4;
        LevelScores["Bree Osh"] = 4;
        LevelScores["Stuart O. Bread"] = 4;
    }
}
