using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBattle : MonoBehaviour
{
    public List<Sprite> ultimateImages;
    public HealthBar hpBar;
    public string charName;
    public int MaxATK;
    public int MaxDEF;
    public int MaxLUCK;
    public int MaxHP;
    public int MaxMana;
    public int ultimateCharge = 0;

    public string[] normalMoves;
    public string[] specialMoves;
    public int[] specialMoveCosts;

    private int ATK;
    private int DEF;
    private int LUCK;
    private int HP;
    private int Mana;

    private float timer = 0;

    private Camera mainCamera;
    private RectTransform hpBarRectTransform;
    private Vector3 offset = new Vector3(1.5f, 0, 0); // Adjust the offset as needed
    private Image ultimateGaugeImage;

    // Start is called before the first frame update
    void Start()
    {
        ATK = MaxATK;
        DEF = MaxDEF;
        LUCK = MaxLUCK;
        HP = MaxHP;
        Mana = MaxMana;

        mainCamera = Camera.main;
        hpBarRectTransform = hpBar.GetComponent<RectTransform>();

        GameObject ultimateGaugeObject = GameObject.FindGameObjectWithTag("UltimateWheel");
        if (ultimateGaugeObject != null)
        {
            ultimateGaugeImage = ultimateGaugeObject.GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.SetCharacterHealth(HP, MaxHP);
        UpdateHealthBarPosition();
    }

    public void SetUltimateGauge(){
        // Check if the UltimateGauge image and the list of images are not null
        if (ultimateGaugeImage != null && ultimateImages != null && ultimateImages.Count > 0)
        {
            // Ensure the ultimateCharge is within the bounds of the list
            if (ultimateCharge >= 0 && ultimateCharge < ultimateImages.Count)
            {
                // Set the UltimateGauge image to the image at the current index
                ultimateGaugeImage.sprite = ultimateImages[ultimateCharge];
            }
        }
    }

    void UpdateHealthBarPosition()
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position + offset);
        hpBarRectTransform.position = screenPos;
    }

    public int Attack(){
        // TO DO - if lucky, return ATK * 2 | Also, need to add variation
        return ATK;
    }

    public void Damage(int attackAmount){
        HP -= attackAmount-DEF;
    }
}
