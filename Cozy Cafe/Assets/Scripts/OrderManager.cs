using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public GameObject[] orderBubbles;
    public GameObject[] orderBubbleImages;
    public Sprite[] items;
    public string[] itemNames;
    public MenuManager menuMan;

    public bool ordering = false;
    public int numItems;
    public string[] order;

    private float customerWait = 0;
    // private SpriteRenderer spriteRenderer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < orderBubbles.Length; i++){
            orderBubbles[i].SetActive(false);
            orderBubbleImages[i].SetActive(false);
        }

        customerWait = Random.Range(0, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (ordering){
            // check for order fulfill
        } else {
            customerWait -= Time.deltaTime;
            if (customerWait <= 0){
                CreateOrder();
            }
        }
    }

    public void CreateOrder(){
        ordering = true;

        numItems = Random.Range(1, 4);
        order = new string[numItems];

        int itemIdx;
        for (int i = 0; i < numItems; i++){
            orderBubbles[i].SetActive(true);
            orderBubbleImages[i].SetActive(true);

            itemIdx = Random.Range(0, items.Length);
            order[i] = itemNames[itemIdx];
            orderBubbleImages[i].GetComponent<SpriteRenderer>().sprite = items[itemIdx];
            orderBubbleImages[i].transform.localScale = new Vector3(0.075f, 0.075f, 0.075f);
        } for (int i = numItems; i < orderBubbles.Length; i++){
            orderBubbles[i].SetActive(false);
            orderBubbleImages[i].SetActive(false);
        }

        menuMan.order = new int[3];
        menuMan.orderLen = 0;
    }

    public void FulfillOrder(){
        ordering = false;

        for (int i = 0; i < orderBubbles.Length; i++){
            orderBubbles[i].SetActive(false);
            orderBubbleImages[i].SetActive(false);
        }

        customerWait = Random.Range(0, 1.5f);
    }
}
