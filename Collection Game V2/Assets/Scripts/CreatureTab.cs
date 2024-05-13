using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreatureTab : MonoBehaviour
{
    public Image imageSpot;
    public TMP_Text nameSpot;
    public TMP_Text biomeSpot;
    public TMP_Text descriptionSpot;

    public Sprite unknownImg;
    public Sprite creatureImg;
    public bool found;
    public string name;
    public string biomes;
    public string description;


    // Start is called before the first frame update
    void Start()
    {
        nameSpot.fontSize = 17;
        biomeSpot.fontSize = 13;
        descriptionSpot.fontSize = 13;
    }

    // Update is called once per frame
    void Update()
    {
        if (found){
            imageSpot.sprite = creatureImg;
        } else {
            imageSpot.sprite = unknownImg;
        }

        nameSpot.text = "Name: "+name;
        biomeSpot.text = "Biomes: "+biomes;
        descriptionSpot.text = description;
    }
}
