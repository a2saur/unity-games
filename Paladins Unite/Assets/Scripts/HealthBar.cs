using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; // The Slider component for the health bar
    public TMP_Text hpText; // Optional: Text component for displaying HP/Max HP (only for characters)

    // Set up health bar for a character
    public void SetCharacterHealth(int currentHP, int maxHP)
    {
        healthSlider.maxValue = maxHP;
        healthSlider.value = currentHP;
        if (hpText != null)
        {
            hpText.text = currentHP.ToString() + "/" + maxHP.ToString();
        }
    }


    // Set up health bar for an enemy
    public void SetEnemyHealth(int currentHP, int maxHP)
    {
        healthSlider.maxValue = maxHP;
        healthSlider.value = currentHP;
        if (hpText != null)
        {
            hpText.gameObject.SetActive(false); // Hide HP text for enemies
        }
    }
}
