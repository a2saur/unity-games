using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public Image background;
    public GameObject itemSpot;
    public GameObject counterTag;
    public TextMeshProUGUI counter;
    // public SpriteRenderer itemSpriteRender;
    private Sprite itemSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // assume no item
        itemSpot.SetActive(false);
        counterTag.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetItem(string itemName){
        Color color = background.color;
        color.a = 1;
        background.color = color;

        itemSpot.SetActive(true);
        itemSprite = Resources.Load<Sprite>($"FoodImgs/{itemName}");
        itemSpot.GetComponent<Image>().sprite = itemSprite;
        counterTag.SetActive(false);
    }

    public void SetItem(string itemName, int count){
        Color color = background.color;
        color.a = 1;
        background.color = color;

        itemSpot.SetActive(true);
        itemSprite = Resources.Load<Sprite>($"FoodImgs/{itemName}");
        itemSpot.GetComponent<Image>().sprite = itemSprite;
        counterTag.SetActive(true);
        counter.text = count.ToString();
    }

    public void GrayOut(){
        Color color = background.color;
        color.a = 0.5f;
        background.color = color;
    }

    public void Reset(){
        itemSpot.SetActive(false);
        counterTag.SetActive(false);
        Color color = background.color;
        color.a = 1;
        background.color = color;
    }
}
