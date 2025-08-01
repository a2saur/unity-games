using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static bool playing = true;
    public static int musicVolume = 7;
    public static int soundVolume = 7;
    public static int robotSpeed = 5;

    public static void Pause(){
        playing = false;
    }

    public static void Resume(){
        playing = true;
    }
}
