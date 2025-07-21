using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static bool playing = true;
    public static bool pauseMenu = false;
    public static bool keyBindingListening = false;

    public static KeyCode rightArrow = KeyCode.D;
    public static KeyCode leftArrow = KeyCode.A;
    public static KeyCode upArrow = KeyCode.W;
    public static KeyCode downArrow = KeyCode.S;

    public static KeyCode interactButton = KeyCode.E;
    public static KeyCode commandButton = KeyCode.F;
    public static KeyCode pauseButton = KeyCode.Escape;
    // public static KeyCode dropButton = KeyCode.G;

    public static Dictionary<string, string> keycodes = new Dictionary<string, string>() {
		{"Right Arrow", "rightArrow"},
		{"Left Arrow", "leftArrow"},
		{"Up Arrow", "upArrow"},
		{"Down Arrow", "downArrow"},
		{"Interact Button", "interactButton"},
		{"Command Button", "commandButton"},
		{"Pause Button", "pauseButton"},
	};

    public static float moveSpeed = 4f; // Movement speed for main char
    public static float lilMoveSpeed = 3f; // Movement speed for lil guys
    public static float defaultTextSpeed = 0.1f;
    public static float thresholdInteractDist = 2f;
    public static bool groupingItems = true;

    public static string[] tags_interactable = {
        "lil-guy",
        "npc",
    };

    public static List<string> inventory = new List<string>();

    public static void Pause(){
        playing = false;
    }

    public static void Resume(){
        playing = true;
    }

    public static void CallPauseMenu(){
        playing = !playing;
        pauseMenu = !pauseMenu;
    }

    public static void SetKeyBinding(string oldKeyName, KeyCode newKey){
        FieldInfo field = typeof(SettingsManager).GetField(keycodes[oldKeyName]);
        field.SetValue(null, newKey);
        // keycodes[oldKeyName] = newKey;
    }

    public static KeyCode GetKeyCode(string keyName){
        FieldInfo field = typeof(SettingsManager).GetField(keyName);
        return (KeyCode)field.GetValue(null);
    }
}
