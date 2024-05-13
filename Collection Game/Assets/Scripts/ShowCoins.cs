using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowCoins : MonoBehaviour
{
    public Inventory inventory;
    private TMP_Text coinText;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectsWithTag("Inventory")[0].GetComponent<Inventory>();

        coinText = GetComponent<TMP_Text>(); // Get reference to the attached button component
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Coins: "+inventory.coins;
    }
}
