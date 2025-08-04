using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static bool playing = true;
    public static bool dialogueOff = true;
    public static int musicVolume = 3;
    public static int soundVolume = 8;
    public static int robotSpeed = 5;

    public static void Pause(){
        playing = false;
    }

    public static void Resume(){
        playing = true;
    }
}
