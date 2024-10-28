using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static bool pauseScreen = false;

    public static KeyCode rightArrow = KeyCode.RightArrow;
    public static KeyCode leftArrow = KeyCode.LeftArrow;
    public static KeyCode jumpButton = KeyCode.UpArrow;
    public static KeyCode spinAttackButton = KeyCode.D;
    public static KeyCode slashAttackButton = KeyCode.S;
    public static KeyCode dashButton = KeyCode.A;

    public static float defaultMoveSpeed = 4f; // Movement speed for main char
    public static float moveSpeed = 4f; // Movement speed for main char
    public static float jumpForce = 7.5f; // Jump force for main char
    public static float dashDuration = 0.1f; // Dash duration in seconds for main char
    public static float dashCooldown = 1f; // Dash cooldown in seconds for main char
    public static float dashSpeedMultiplier = 3f; // Dash multiplier for main char
    public static float attackDuration = 0.4f; // Dash duration in seconds for main char
    public static float attackCooldown = 0.5f; // Dash cooldown in seconds for main char

    public static void Pause(){
        pauseScreen = true;
    }

    public static void Resume(){
        pauseScreen = false;
    }
}
