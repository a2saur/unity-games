using UnityEngine;

public static class SettingsManager
{
    public static KeyCode jumpButton = KeyCode.Space;
    public static KeyCode leftButton = KeyCode.A;
    public static KeyCode downButton = KeyCode.S;
    public static KeyCode rightButton = KeyCode.D;
    public static KeyCode upButton = KeyCode.W;
    public static KeyCode attackButton = KeyCode.F;

    public static float maxAttackDelay = 0.5f;
    public static float attackDuration = 0.4f;
    
    public static float initialJumpForce = 500;
    public static float jumpForce = 1;
    public static float maxFallSpeed = 25;

    public static float moveSpeed = 5;
    public static float charHeight = 3;
    public static float charWidth = 2;
}
