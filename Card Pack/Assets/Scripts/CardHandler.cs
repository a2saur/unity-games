using UnityEngine;
using UnityEngine.UI;

public class CardHandler : MonoBehaviour
{
    public GameObject cardPack1;
    public GameObject cardPack2;
    public GameObject cardPack3;

    public GameObject normalCard1;
    public GameObject normalCard2;
    public GameObject normalCard3;

    public GameObject valueCard1;
    public GameObject valueCard2;

    public Sprite normalPack;
    public Sprite valuePack;
    public Sprite suitPack;

    public Sprite[] suitPackImgs;
    public Sprite[] valuePackImgs;

    public CardInventory cardInv;
    
    private string currentState = "loading";
    private int cardSelected = 0;
    private GameObject[] cardPacks;
    private Sprite[] cardTypeImgs;
    private int[] cardPackTypes;
    private int[] cardPackSpecificTypes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cardPacks = new GameObject[]{cardPack1, cardPack2, cardPack3};
        cardTypeImgs = new Sprite[]{normalPack, valuePack, suitPack};
        cardPackTypes = new int[3];
        cardPackSpecificTypes = new int[3];

        normalCard1.SetActive(false);
        normalCard2.SetActive(false);
        normalCard3.SetActive(false);
        
        valueCard1.SetActive(false);
        valueCard2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == "waiting"){
            // check for which card pack is selected
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                cardSelected++;
            } else if (Input.GetKeyDown(KeyCode.LeftArrow)){
                cardSelected--;
            }

            // check for mouse
            for (int i = 0; i < cardPacks.Length; i++)
            {
                RectTransform rectTransform = cardPacks[i].GetComponent<Image>().rectTransform;
                if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition))
                {
                    // Mouse is over this image
                    cardSelected = i;
                    if (Input.GetMouseButtonDown(0)) // Left click
                    {
                        // Select
                        currentState = "selected";
                    }
                }
            }

            // animation
            for (int i = 0; i < 3; i++){
                if (i == cardSelected%3){
                    cardPacks[i].GetComponent<Animator>().SetBool("Selected", true);
                } else {
                    cardPacks[i].GetComponent<Animator>().SetBool("Selected", false);
                }
            }

            if (Input.GetKeyDown(KeyCode.Return)){
                currentState = "selected";
            }

        } else if (currentState == "loading"){
            // spawn card packs
            float choice;
            for (int i = 0; i < 3; i++){
                choice = Random.value;
                if (choice < 0.7){
                    // normal pack
                    cardPackTypes[i] = 1;
                    cardPackSpecificTypes[i] = 0;
                    cardPacks[i].GetComponent<Image>().sprite = cardTypeImgs[cardPackTypes[i]-1];
                } else if (choice < 0.8){
                    // value pack
                    cardPackTypes[i] = 2;
                    cardPackSpecificTypes[i] = Random.Range(0, 13);
                    cardPacks[i].GetComponent<Image>().sprite = valuePackImgs[cardPackSpecificTypes[i]];
                } else {
                    // suit pack
                    cardPackTypes[i] = 3;
                    cardPackSpecificTypes[i] = Random.Range(0, 4);
                    cardPacks[i].GetComponent<Image>().sprite = suitPackImgs[cardPackSpecificTypes[i]];
                }
            }
            currentState = "waiting";
        } else if (currentState == "selected"){
            cardPack1.SetActive(false);
            cardPack2.SetActive(false);
            cardPack3.SetActive(false);
            if (cardPackTypes[cardSelected % 3] == 1){
                // normal pack
                int card1Suit = Random.Range(0, 4);
                int card1Val = Random.Range(0, 13);
                normalCard1.SetActive(true);
                normalCard1.GetComponent<Image>().sprite = cardInv.allCardImgs[card1Suit][card1Val];
                if (card1Suit == 0){
                    SystemMan.spadeCards.Add(card1Val);
                } else if (card1Suit == 1){
                    SystemMan.heartCards.Add(card1Val);
                } else if (card1Suit == 2){
                    SystemMan.clubCards.Add(card1Val);
                } else if (card1Suit == 3){
                    SystemMan.diamondCards.Add(card1Val);
                }

                int card2Suit = Random.Range(0, 4);
                int card2Val = Random.Range(0, 13);
                normalCard2.SetActive(true);
                normalCard2.GetComponent<Image>().sprite = cardInv.allCardImgs[card2Suit][card2Val];
                if (card2Suit == 0){
                    SystemMan.spadeCards.Add(card2Val);
                } else if (card2Suit == 1){
                    SystemMan.heartCards.Add(card2Val);
                } else if (card2Suit == 2){
                    SystemMan.clubCards.Add(card2Val);
                } else if (card2Suit == 3){
                    SystemMan.diamondCards.Add(card2Val);
                }

                int card3Suit = Random.Range(0, 4);
                int card3Val = Random.Range(0, 13);
                normalCard3.SetActive(true);
                normalCard3.GetComponent<Image>().sprite = cardInv.allCardImgs[card3Suit][card3Val];
                if (card3Suit == 0){
                    SystemMan.spadeCards.Add(card3Val);
                } else if (card3Suit == 1){
                    SystemMan.heartCards.Add(card3Val);
                } else if (card3Suit == 2){
                    SystemMan.clubCards.Add(card3Val);
                } else if (card3Suit == 3){
                    SystemMan.diamondCards.Add(card3Val);
                }
            } else if (cardPackTypes[cardSelected % 3] == 2){
                // value pack
                int card1Suit = Random.Range(0, 4);
                valueCard1.SetActive(true);
                valueCard1.GetComponent<Image>().sprite = cardInv.allCardImgs[card1Suit][cardPackSpecificTypes[cardSelected % 3]];
                if (card1Suit == 0){
                    SystemMan.spadeCards.Add(cardPackSpecificTypes[cardSelected % 3]);
                } else if (card1Suit == 1){
                    SystemMan.heartCards.Add(cardPackSpecificTypes[cardSelected % 3]);
                } else if (card1Suit == 2){
                    SystemMan.clubCards.Add(cardPackSpecificTypes[cardSelected % 3]);
                } else if (card1Suit == 3){
                    SystemMan.diamondCards.Add(cardPackSpecificTypes[cardSelected % 3]);
                }
                
                int card2Suit = Random.Range(0, 4);
                valueCard2.SetActive(true);
                valueCard2.GetComponent<Image>().sprite = cardInv.allCardImgs[card2Suit][cardPackSpecificTypes[cardSelected % 3]];
                if (card2Suit == 0){
                    SystemMan.spadeCards.Add(cardPackSpecificTypes[cardSelected % 3]);
                } else if (card2Suit == 1){
                    SystemMan.heartCards.Add(cardPackSpecificTypes[cardSelected % 3]);
                } else if (card2Suit == 2){
                    SystemMan.clubCards.Add(cardPackSpecificTypes[cardSelected % 3]);
                } else if (card2Suit == 3){
                    SystemMan.diamondCards.Add(cardPackSpecificTypes[cardSelected % 3]);
                }
            } else {
                // suit pack
                int card1Val = Random.Range(0, 13);
                normalCard1.SetActive(true);
                normalCard1.GetComponent<Image>().sprite = cardInv.allCardImgs[cardPackSpecificTypes[cardSelected % 3]][card1Val];
                if (cardPackSpecificTypes[cardSelected % 3] == 0){
                    SystemMan.spadeCards.Add(card1Val);
                } else if (cardPackSpecificTypes[cardSelected % 3] == 1){
                    SystemMan.heartCards.Add(card1Val);
                } else if (cardPackSpecificTypes[cardSelected % 3] == 2){
                    SystemMan.clubCards.Add(card1Val);
                } else if (cardPackSpecificTypes[cardSelected % 3] == 3){
                    SystemMan.diamondCards.Add(card1Val);
                }

                int card2Val = Random.Range(0, 13);
                normalCard2.SetActive(true);
                normalCard2.GetComponent<Image>().sprite = cardInv.allCardImgs[cardPackSpecificTypes[cardSelected % 3]][card2Val];
                if (cardPackSpecificTypes[cardSelected % 3] == 0){
                    SystemMan.spadeCards.Add(card2Val);
                } else if (cardPackSpecificTypes[cardSelected % 3] == 1){
                    SystemMan.heartCards.Add(card2Val);
                } else if (cardPackSpecificTypes[cardSelected % 3] == 2){
                    SystemMan.clubCards.Add(card2Val);
                } else if (cardPackSpecificTypes[cardSelected % 3] == 3){
                    SystemMan.diamondCards.Add(card2Val);
                }

                int card3Val = Random.Range(0, 13);
                normalCard3.SetActive(true);
                normalCard3.GetComponent<Image>().sprite = cardInv.allCardImgs[cardPackSpecificTypes[cardSelected % 3]][card3Val];
                if (cardPackSpecificTypes[cardSelected % 3] == 0){
                    SystemMan.spadeCards.Add(card3Val);
                } else if (cardPackSpecificTypes[cardSelected % 3] == 1){
                    SystemMan.heartCards.Add(card3Val);
                } else if (cardPackSpecificTypes[cardSelected % 3] == 2){
                    SystemMan.clubCards.Add(card3Val);
                } else if (cardPackSpecificTypes[cardSelected % 3] == 3){
                    SystemMan.diamondCards.Add(card3Val);
                }
            }
            currentState = "showingCards";
        } else if (currentState == "showingCards"){
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)){
                cardPack1.SetActive(true);
                cardPack2.SetActive(true);
                cardPack3.SetActive(true);

                normalCard1.SetActive(false);
                normalCard2.SetActive(false);
                normalCard3.SetActive(false);
                
                valueCard1.SetActive(false);
                valueCard2.SetActive(false);
                currentState = "loading";
            }
        }
    }
}
