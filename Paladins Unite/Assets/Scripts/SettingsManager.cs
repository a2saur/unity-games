using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static bool pauseScreen = false;
    public static Inventory inventory = new Inventory();
    
    // public static KeyCode rightArrow = KeyCode.RightArrow;
    // public static KeyCode leftArrow = KeyCode.LeftArrow;
    // public static KeyCode upArrow = KeyCode.UpArrow;
    // public static KeyCode downArrow = KeyCode.DownArrow;
    // public static KeyCode selectButton = KeyCode.Space;
    // public static KeyCode backButton = KeyCode.Escape;

    public static Dictionary<string, KeyCode> keyLabels = new Dictionary<string, KeyCode>(){
        {"rightArrow", KeyCode.RightArrow},
        {"leftArrow", KeyCode.LeftArrow},
        {"upArrow", KeyCode.UpArrow},
        {"downArrow", KeyCode.DownArrow},
        {"selectButton", KeyCode.Space},
        {"backButton", KeyCode.B},
        {"pauseButton", KeyCode.Escape},
    };

    public static Dictionary<KeyCode, string> keyIcons = new Dictionary<KeyCode, string>(){
        {KeyCode.RightArrow, ">"},
        {KeyCode.LeftArrow, "<"},
        {KeyCode.UpArrow, "^"},
        {KeyCode.DownArrow, "v"},
        {KeyCode.Keypad1, "1"},
        {KeyCode.Keypad2, "2"},
        {KeyCode.Keypad3, "3"},
        {KeyCode.Keypad4, "4"},
        {KeyCode.Keypad5, "5"},
        {KeyCode.Keypad6, "6"},
        {KeyCode.Keypad7, "7"},
        {KeyCode.Keypad8, "8"},
        {KeyCode.Keypad9, "9"},
        {KeyCode.Keypad0, "0"},
        {KeyCode.A, "A"},
        {KeyCode.B, "B"},
        {KeyCode.C, "C"},
        {KeyCode.D, "D"},
        {KeyCode.E, "E"},
        {KeyCode.F, "F"},
        {KeyCode.G, "G"},
        {KeyCode.H, "H"},
        {KeyCode.I, "I"},
        {KeyCode.J, "J"},
        {KeyCode.K, "K"},
        {KeyCode.L, "L"},
        {KeyCode.M, "M"},
        {KeyCode.N, "N"},
        {KeyCode.O, "O"},
        {KeyCode.P, "P"},
        {KeyCode.Q, "Q"},
        {KeyCode.R, "R"},
        {KeyCode.S, "S"},
        {KeyCode.T, "T"},
        {KeyCode.U, "U"},
        {KeyCode.V, "V"},
        {KeyCode.W, "W"},
        {KeyCode.X, "X"},
        {KeyCode.Y, "Y"},
        {KeyCode.Z, "Z"},
        {KeyCode.Alpha0, "0"},
        {KeyCode.Alpha1, "1"},
        {KeyCode.Alpha2, "2"},
        {KeyCode.Alpha3, "3"},
        {KeyCode.Alpha4, "4"},
        {KeyCode.Alpha5, "5"},
        {KeyCode.Alpha6, "6"},
        {KeyCode.Alpha7, "7"},
        {KeyCode.Alpha8, "8"},
        {KeyCode.Alpha9, "9"},
        {KeyCode.Space, "Space"},
        {KeyCode.Return, "Enter"},
        {KeyCode.LeftBracket, "["},
        {KeyCode.RightBracket, "]"},
        {KeyCode.Semicolon, ";"},
        {KeyCode.Quote, "'"},
        {KeyCode.Comma, ","},
        {KeyCode.Period, "."},
        {KeyCode.Slash, "/"},
        {KeyCode.Backslash, "\\"},
        {KeyCode.BackQuote, "`"},
        {KeyCode.Minus, "-"},
        {KeyCode.Equals, "="},
    };

    public static Dictionary<string, bool> criteriasSpecific = new Dictionary<string, bool>(){
        {"bobaMinigame", false},
    };

    public static int chapter;
    public static bool playing = true;

    public static float interactableDist = 5f;
    public static float defaultTextSpeed = 0.05f;
    public static float jumpSpeed = 7f;

    public static List<string> enemiesSeen = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool hasItem(string itemName){
        // for (int i = 0; i < items.Length; i++){
        //     if (items[i].GetName() == itemName){
        //         return true;
        //     }
        // }
        return false;
    }

    public static bool specificCriteria(string criteriaName){
        return criteriasSpecific[criteriaName];
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

    public static void setSpecificCriteria(string criteriaName){
        criteriasSpecific[criteriaName] = true;
    }
}
