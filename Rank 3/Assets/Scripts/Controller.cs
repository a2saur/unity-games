using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static KeyCode rightArrow = KeyCode.RightArrow;
    public static KeyCode leftArrow = KeyCode.LeftArrow;

    public static KeyCode jumpButton = KeyCode.UpArrow;

    public static KeyCode interactButton = KeyCode.E;
    public static KeyCode swapButton = KeyCode.R;
    public static KeyCode pauseButton = KeyCode.Escape;

    public static int numStrands = 0;

    public static bool paused = false;
    public static bool transforming = false;

    public static float interactableDist = 1.5f;
    public static float defaultTextSpeed = 0.25f;
    public static float jumpSpeed = 5f;
    public static float moveSpeed = 3f;

    public static bool hasItem(string itemName){
        // for (int i = 0; i < items.Length; i++){
        //     if (items[i].GetName() == itemName){
        //         return true;
        //     }
        // }
        return false;
    }

    public static int GetNumStrands(){
        return numStrands;
    }

    public static void Pause(){
        paused = true;
    }

    public static void Resume(){
        paused = false;
    }
}
