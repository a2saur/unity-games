using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetTownInfo : MonoBehaviour
{
    public TMP_Text text_block;
    public Vector3[] housePositions = new Vector3[] {
        new Vector3(0.5f, 3.5f, 0),
        new Vector3(-6.5f, -2.5f, 0),
        new Vector3(4.5f, 0.5f, 0),
        new Vector3(-4.5f, 8.5f, 0),
        new Vector3(1.5f, 9.5f, 0),
    };

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] townInfo = GameObject.FindGameObjectsWithTag("ActiveTown");
        Town town = townInfo[0].GetComponent<Town>();

        text_block.text = town.TownName;

        for (int i = 0; i < town.GetComponent<Town>().houses.Count; i++){
            Instantiate(town.GetComponent<Town>().houses[i], housePositions[i], Quaternion.identity);
        }
        Debug.Log("Town Displayed!");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
