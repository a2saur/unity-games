using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public TMP_Text characterSelection;
    public TMP_Text infoPanel;

    private int playerTurn = true;
    private int turn = 0; // Character Turn
    private int selection = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Normal, skill, item, SPECIAL
        if (playerTurn){
            for (int i = 0; i++){

            }
            if (selection == 0){
                // selecting character
            }
        }
    }
}
