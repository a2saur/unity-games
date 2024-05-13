using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetManager : MonoBehaviour
{
    public KeyCode rightArrow = KeyCode.RightArrow;
    public KeyCode leftArrow = KeyCode.LeftArrow;
    public KeyCode upArrow = KeyCode.UpArrow;
    public KeyCode downArrow = KeyCode.DownArrow;

    public KeyCode jumpButton = KeyCode.A;

    public KeyCode interactButton = KeyCode.A;
    public KeyCode backButton = KeyCode.B;
    public KeyCode swapButton = KeyCode.Z;

    public Item[] items;

    public int chapter;
    
    public int ATK;
    public int maxHP;
    public int currentHP;
    public int maxSP;
    public int currentSP;
    public int XP;
    public int coins;

    public bool playing = true;

    public float interactableDist = 5f;
    public float defaultTextSpeed = 1f;
    public float jumpSpeed = 7f;

    public List<string> enemiesSeen = new List<string>();

    public bool hasItem(string itemName){
        for (int i = 0; i < items.Length; i++){
            if (items[i].GetName() == itemName){
                return true;
            }
        }
        return false;
    }

    public int GetChapter(){
        return chapter;
    }

    public void Pause(){
        playing = false;
    }

    public void Resume(){
        playing = true;
    }
}
