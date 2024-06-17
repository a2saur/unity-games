using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public TMP_Text characterSelection;
    public TMP_Text infoPanel;
    public PopUpController popUpObj;

    public GameObject[] characters;
    public GameObject[] enemiesToAttack;
    public string[] allyMoves;
    public string[] selfMoves;
    public string[] allEnemiesMoves;

    private bool playerTurn = true;
    private int turn = 0; // Character Turn
    private int selection = 0;
    private int charSelected = 0;
    private int secondarySelected = 0;
    private int moveSelected = 0;
    private string charSelectText = "";
    private string optionSelectText = "";
    private string[] options = new string[]{
        "Normal Moves",
        "Special Moves",
        "Items",
        "Ultimate",
    };
    private string moveSelectedFinal = "";

    private bool turnStart = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Normal, skill, item, SPECIAL
        if (playerTurn){
            if (turnStart){
                for (int i = 0; i < characters.Length; i++){
                    characters[i].GetComponent<CharacterBattle>().ultimateCharge++;
                }
                turnStart = false;
            }

            if (selection == 0){ // selecting character
                charSelectText = "";
                for (int i = 0; i < characters.Length; i++){
                    if (i == charSelected){
                        charSelectText += "> ";
                    } else {
                        charSelectText += "  ";
                    }
                    charSelectText += characters[i].GetComponent<CharacterBattle>().charName;
                    charSelectText += "\n";
                }

                characterSelection.text = charSelectText;
                infoPanel.text = "";

                // keyboard buttons
                if (Input.GetKeyDown(SettingsManager.keyLabels["downArrow"])) {
                    charSelected++;
                } if (Input.GetKeyDown(SettingsManager.keyLabels["upArrow"])) {
                    charSelected--;
                }

                if (charSelected < 0){
                    charSelected = characters.Length-1;
                } else if (charSelected >= characters.Length){
                    charSelected = 0;
                }

                if (Input.GetKeyDown(SettingsManager.keyLabels["selectButton"])){
                    selection++;
                }
            } else if (selection == 1){ // selecting move type
                optionSelectText = "";
                for (int i = 0; i < options.Length; i++){
                    if (i == secondarySelected){
                        optionSelectText += "> ";
                    } else {
                        optionSelectText += "  ";
                    }
                    optionSelectText += options[i];
                    optionSelectText += "\n";
                }
                infoPanel.text = optionSelectText;
                // if ultimate is ready, add to selection;

                // keyboard buttons
                if (Input.GetKeyDown(SettingsManager.keyLabels["downArrow"])) {
                    secondarySelected++;
                } if (Input.GetKeyDown(SettingsManager.keyLabels["upArrow"])) {
                    secondarySelected--;
                }

                if (secondarySelected < 0){
                    secondarySelected = options.Length-1;
                } else if (secondarySelected >= options.Length){
                    secondarySelected = 0;
                }

                if (Input.GetKeyDown(SettingsManager.keyLabels["selectButton"])){
                    selection++;
                } else if (Input.GetKeyDown(SettingsManager.keyLabels["backButton"])){
                    selection--;
                }
            } else if (selection == 2){ // selecting move
                if (options[secondarySelected] == "Ultimate"){
                    if (characters[charSelected].GetComponent<CharacterBattle>().ultimateCharge < 10){
                        popUpObj.PopUp("The Ultimate isn\'t Fully Charged!");
                        selection--;
                    } else {
                        selection++;
                    }
                } else if (options[secondarySelected] == "Items" && SettingsManager.inventory.GetNumItems() == 0){
                    popUpObj.PopUp("There are no items!");
                    selection--;
                } else {
                    optionSelectText = "";
                    if (options[secondarySelected] == "Normal Moves"){
                        for (int i = 0; i < characters[charSelected].GetComponent<CharacterBattle>().normalMoves.Length; i++){
                            if (i == moveSelected){
                                optionSelectText += "> ";
                            } else {
                                optionSelectText += "  ";
                            }
                            optionSelectText += characters[charSelected].GetComponent<CharacterBattle>().normalMoves[i];
                            optionSelectText += "\n";
                        }
                    } if (options[secondarySelected] == "Special Moves"){
                        for (int i = 0; i < characters[charSelected].GetComponent<CharacterBattle>().specialMoves.Length; i++){
                            if (i == moveSelected){
                                optionSelectText += "> ";
                            } else {
                                optionSelectText += "  ";
                            }
                            optionSelectText += characters[charSelected].GetComponent<CharacterBattle>().specialMoves[i];
                            optionSelectText += " ";
                            optionSelectText += characters[charSelected].GetComponent<CharacterBattle>().specialMoveCosts[i].ToString();
                            optionSelectText += "\n";
                        }
                    } if (options[secondarySelected] == "Items"){
                        optionSelectText = SettingsManager.inventory.GetItemBattlePanel(moveSelected);
                    }
                    infoPanel.text = optionSelectText;
                    // if ultimate is ready, add to selection;

                    // keyboard buttons
                    if (Input.GetKeyDown(SettingsManager.keyLabels["downArrow"])) {
                        moveSelected++;
                    } if (Input.GetKeyDown(SettingsManager.keyLabels["upArrow"])) {
                        moveSelected--;
                    }

                    if (options[secondarySelected] == "Normal Moves"){
                        if (moveSelected < 0){
                            moveSelected = characters[charSelected].GetComponent<CharacterBattle>().normalMoves.Length-1;
                        } else if (moveSelected >= characters[charSelected].GetComponent<CharacterBattle>().normalMoves.Length){
                            moveSelected = 0;
                        }
                    } else if (options[secondarySelected] == "Special Moves"){
                        if (moveSelected < 0){
                            moveSelected = characters[charSelected].GetComponent<CharacterBattle>().specialMoves.Length-1;
                        } else if (moveSelected >= characters[charSelected].GetComponent<CharacterBattle>().specialMoves.Length){
                            moveSelected = 0;
                        }
                    } else if (options[secondarySelected] == "Items"){
                        if (moveSelected < 0){
                            moveSelected = SettingsManager.inventory.GetItemCounts().Count-1;
                        } else if (moveSelected >= SettingsManager.inventory.GetItemCounts().Count){
                            moveSelected = 0;
                        }
                    }

                    if (Input.GetKeyDown(SettingsManager.keyLabels["selectButton"])){
                        selection++;
                        if (options[secondarySelected] == "Normal Moves"){
                            moveSelectedFinal = characters[charSelected].GetComponent<CharacterBattle>().specialMoves[moveSelected];
                        } else if (options[secondarySelected] == "Special Moves"){
                            moveSelectedFinal = characters[charSelected].GetComponent<CharacterBattle>().specialMoves[moveSelected];
                        } else if (options[secondarySelected] == "Items"){
                            moveSelectedFinal = "";
                        }
                    } else if (Input.GetKeyDown(SettingsManager.keyLabels["backButton"])){
                        selection--;
                    }
                }
            } else if (selection == 3){
                // select target
                // TO DO - Show arrow(s)
                // if (ArrayHas(allEnemiesMoves, moveSelectedFinal)) {
                //     // select all enemies
                // } else {
                if (ArrayHas(selfMoves, moveSelectedFinal)) {
                    // select self
                } else if (ArrayHas(allyMoves, moveSelectedFinal)){
                    // select from allies
                } else {
                    // select from other enemies
                }
                // }
            } else if (selection == 4){
                // attack
                int damage = characters[charSelected].GetComponent<CharacterBattle>().Attack();
                // for ()
                characters[charSelected].GetComponent<CharacterBattle>();
            }

            characters[charSelected].GetComponent<CharacterBattle>().SetUltimateGauge();
        }
    }

    bool ArrayHas(string[] list, string contains){
        for (int i = 0; i < list.Length; i++){
            if (list[i] == contains){
                return true;
            }
        }
        return false;
    }
}
