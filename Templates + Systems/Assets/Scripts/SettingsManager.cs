using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static KeyCode rightArrow = KeyCode.RightArrow;
    public static KeyCode leftArrow = KeyCode.LeftArrow;
    public static KeyCode upArrow = KeyCode.UpArrow;
    public static KeyCode downArrow = KeyCode.DownArrow;

    public static KeyCode jumpButton = KeyCode.A;

    public static KeyCode interactButton = KeyCode.A;
    public static KeyCode backButton = KeyCode.B;
    public static KeyCode swapButton = KeyCode.Z;

    // public static Item[] items;

    public static int chapter;
    
    public static int ATK;
    public static int currentATK;
    public static int DEF;
    public static int currentDEF;
    public static int maxHP;
    public static int currentHP;
    public static int maxSP;
    public static int currentSP;
    public static int XP;
    public static int coins;

    public static bool playing = true;

    public static float interactableDist = 5f;
    public static float defaultTextSpeed = 0.25f;
    public static float jumpSpeed = 7f;

    public static List<string> enemiesSeen = new List<string>();

    public static bool hasItem(string itemName){
        // for (int i = 0; i < items.Length; i++){
        //     if (items[i].GetName() == itemName){
        //         return true;
        //     }
        // }
        return false;
    }

    public static int GetChapter(){
        return chapter;
    }

    public static void Pause(){
        playing = false;
    }

    public static void Resume(){
        playing = true;
    }
}
