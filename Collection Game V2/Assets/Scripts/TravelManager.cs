using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TravelManager : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI costText;
    public int costPerPack = 100; // Change this to your actual cost

    public Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectsWithTag("Inventory")[0].GetComponent<Inventory>();
    }

    private void Update()
    {
        // max packs = coins/costPerPack
        int coins = inventory.coins;

        int maxPacks = (int)(coins/costPerPack);
        
        int packsToBuy = (int)(slider.value * maxPacks);
        int totalCost = packsToBuy * costPerPack;
        costText.text = $"Current Coins: {coins} coins\n----------\nNumber of Travel: {packsToBuy}\nTotal Cost: {totalCost} coins";
    }

    public void BuyPacks()
    {
        int coins = inventory.coins;

        int maxPacks = (int)(coins/costPerPack);
        
        int packsToBuy = (int)(slider.value * maxPacks);
        int totalCost = packsToBuy * costPerPack;

        inventory.coins -= totalCost;
        inventory.travels += packsToBuy;

        SceneManager.LoadScene("SkyWorld");
    }
}