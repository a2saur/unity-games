using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMatching : MonoBehaviour
{
    public GameObject[] cards;
    public int[] counts;

    public GameObject CardCover;
    public GameObject Selector;
    public GameObject Selector2;

    private int idx = 0;
    public int colCount = 4; // num in column
    public int rowCount = 5; // num in row
    private float xSpacing = 2.25f;
    private float ySpacing = 2.25f;
    private Vector3 startPos = new Vector3(-5, -3.5f, 0);

    private List<int> cardDesignations = new List<int>();
    private List<GameObject> instantiatedCards = new List<GameObject>();
    private List<GameObject> instantiatedCovers = new List<GameObject>();

    private int firstIdx = -1;
    private bool pause = false;
    private float wait = 0;

    public UniversalSettings USO;
    void Start()
    {
        USO = GameObject.FindWithTag("UniversalSettings").GetComponent<UniversalSettings>();

        Selector2.SetActive(false);

        // startPos = new Vector3 (-((rowCount/2)*xSpacing), -((colCount/2)*ySpacing), 0);

        // add objects
        for (int i = 0; i < counts.Length; i++)
        {
            for (int j = 0; j < counts[i]; j++)
            {
                cardDesignations.Add(i);
            }
        }

        // shuffle objects
        for (int i = 0; i < cardDesignations.Count; i++)
        {
            int temp = cardDesignations[i];
            int randomIndex = Random.Range(i, cardDesignations.Count);
            cardDesignations[i] = cardDesignations[randomIndex];
            cardDesignations[randomIndex] = temp;
        }

        for (int y = 0; y < colCount; y++){
            for (int x = 0; x < rowCount; x++){
                GameObject card = Instantiate(cards[cardDesignations[(x+(y*rowCount))]], new Vector3 (startPos.x+(x*xSpacing), startPos.y+(y*ySpacing), 0), Quaternion.identity);
                GameObject cover = Instantiate(CardCover, new Vector3 (startPos.x+(x*xSpacing), startPos.y+(y*ySpacing), -1), Quaternion.identity);
                instantiatedCards.Add(card);
                instantiatedCovers.Add(cover);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause){
            // moving selector
            if (Input.GetKeyDown(USO.leftArrow)){
                idx--;
            } if (Input.GetKeyDown(USO.rightArrow)){
                idx++;
            } if (Input.GetKeyDown(USO.upArrow)){
                idx += rowCount;
            } if (Input.GetKeyDown(USO.downArrow)){
                idx -= rowCount;
            }

            if (idx > (rowCount*colCount)-1){
                idx = 0;
            } if (idx < 0){
                idx = (rowCount*colCount)-1;
            }

            Selector.transform.position = new Vector3 (startPos.x+((idx%rowCount)*xSpacing), startPos.y+(((int) (idx/rowCount))*ySpacing), 1);

            // flip over card?
            if (Input.GetKeyDown(USO.selectKey)){
                if (instantiatedCovers[idx].activeSelf){
                    instantiatedCovers[idx].SetActive(false);
                    if (firstIdx == -1){
                        firstIdx = idx;
                        Selector2.SetActive(true);
                        Selector2.transform.position = new Vector3 (startPos.x+((idx%rowCount)*xSpacing), startPos.y+(((int) (idx/rowCount))*ySpacing), 1);
                    } else {
                        if (cardDesignations[idx] == cardDesignations[firstIdx]){
                            // Yay stay flipped
                            firstIdx = -1;
                            Selector2.SetActive(false);
                        } else {
                            // Boo, wait a bit and flip back over
                            pause = true;
                            wait = 1.5f;
                        }
                    }
                }
            }
        } else {
            wait -= Time.deltaTime;
            if (wait < 0){
                pause = false;

                instantiatedCovers[idx].SetActive(true);
                instantiatedCovers[firstIdx].SetActive(true);
                firstIdx = -1;
                Selector2.SetActive(false);
            }
        }
    }
}
