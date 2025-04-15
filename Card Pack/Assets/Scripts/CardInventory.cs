using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardInventory : MonoBehaviour
{
    public GameObject cardInventoryPanel;
    public Image[] cardImgs;
    public Sprite[] spadeCardImgs;
    public Sprite[] heartCardImgs;
    public Sprite[] clubCardImgs;
    public Sprite[] diamondCardImgs;
    public Sprite[][] allCardImgs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cardInventoryPanel.SetActive(false);
        allCardImgs = new Sprite[][] { spadeCardImgs, heartCardImgs, clubCardImgs, diamondCardImgs };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TogglePause();
        }
    }

    public void SetUpPage(){
        List<int>[] allCards = new List<int>[] { SystemMan.spadeCards, SystemMan.heartCards, SystemMan.clubCards, SystemMan.diamondCards};
        for (int i = 0; i < 13; i++){
            cardImgs[i].sprite = allCardImgs[SystemMan.page][i];
            Color color = cardImgs[i].color;
            if (allCards[SystemMan.page].Contains(i)){
                color.a = 1;
            } else {
                color.a = 0.5f; // Set alpha to 50%
            }
            cardImgs[i].color = color;
        }
    }

    public void TogglePause(){
        if (SystemMan.pauseMode == "playing"){
            SystemMan.pauseMode = "cardPause";
            cardInventoryPanel.SetActive(true);
            SetUpPage();
        } else {
            if (SystemMan.pauseMode == "cardPause"){
                SystemMan.pauseMode = "playing";
                cardInventoryPanel.SetActive(false);
            }
        }
    }

    public void NextPage(){
        if (SystemMan.page < 3){
            SetUpPage();
            SystemMan.page++;
        }
    }

    public void PrevPage(){
        if (SystemMan.page > 0){
            SetUpPage();
            SystemMan.page--;
        }
    }
}
