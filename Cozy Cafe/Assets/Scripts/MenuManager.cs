using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject[] menuSlots;
    public GameObject[] imageSpots;
    public GameObject[] orderImageSpots;
    public Sprite[] itemImages;
    public string[] itemNames;

    public int[] order;
    public int orderLen;
    
    public int pageNum;
    public bool isActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        order = new int[3];
        orderLen = 0;
        pageNum = 0;
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(){
        menuPanel.SetActive(true);
        isActive = true;

        for (int i = orderLen; i < 3; i++){
            orderImageSpots[i].SetActive(false);
        }

        int x = 0;
        for (int i = pageNum*6; i < Mathf.Min((pageNum*6)+6, itemImages.Length); i++){
            menuSlots[x].SetActive(true);
            imageSpots[x].SetActive(true);
            imageSpots[x].GetComponent<Image>().sprite = itemImages[i];
            imageSpots[x].GetComponent<Image>().preserveAspect = true;

            x++;
        }
        while (x < 6){
            menuSlots[x].SetActive(false);
            imageSpots[x].SetActive(false);
            x++;
        }
    }

    public void Hide(){
        menuPanel.SetActive(false);
        isActive = false;
    }

    public void Toggle(){
        if (isActive){
            Hide();
        } else {
            Show();
        }
    }


    public void ClickedItem(int idx){
        if (orderLen < 3){
            order[orderLen] = idx+(pageNum*6);

            orderImageSpots[orderLen].SetActive(true);
            orderImageSpots[orderLen].GetComponent<Image>().sprite = itemImages[idx+(pageNum*6)];
            orderImageSpots[orderLen].GetComponent<Image>().preserveAspect = true;
            orderLen++;
        }
    }

    public void RemoveItem(int idx){
        // Shift items after idx to the left
        for (int i = idx; i < orderLen - 1; i++) {
            order[i] = order[i + 1];
            orderImageSpots[i].SetActive(true);
            orderImageSpots[i].GetComponent<Image>().sprite = itemImages[order[i]];
            orderImageSpots[i].GetComponent<Image>().preserveAspect = true;
        }

        // Clear the last slot
        orderImageSpots[orderLen - 1].SetActive(false);

        orderLen--;
    }

}
