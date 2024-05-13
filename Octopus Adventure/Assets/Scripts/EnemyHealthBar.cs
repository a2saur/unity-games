using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class EnemyHealthBar : MonoBehaviour
{
    public Image bar;
    public TextMeshProUGUI health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setHealth(int hp, int maxHP){
        float percentage = (float)hp/(float)maxHP;
        bar.GetComponent<RectTransform>().anchorMax = new Vector2(percentage, 1);
        health.text = hp.ToString();
    }
}
