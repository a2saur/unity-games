using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public List<string> GetItemCounts(){
        List<string> itemNames = new List<string>();
        List<int> itemCounts = new List<int>();
        // returns List of strings (Item + count)
        for (int i = 0; i < items.Count; i++){
            if (itemNames.Contains(items[i].itemName)){
                // add to count
            } else {
                itemNames.Add(items[i].itemName);
                itemCounts.Add(1);
            }
        }

        List<string> itemNamesCounts = new List<string>();
        for (int i = 0; i < itemNames.Count; i++){
            itemNamesCounts.Add(itemNames[i]+" ("+itemCounts[i].ToString()+")");
        }
        return itemNamesCounts;
    }

    public int GetNumItems(){
        return items.Count;
    }

    public string GetItemBattlePanel(int index){
        // currently 3 x 2 item Panel
        int pageNum = index % (3*2);
        string itemPanel = "";
        List<string> itemNamesCounts = GetItemCounts();

        int iMax;
        if (itemNamesCounts.Count < (pageNum*3*2)+(3*2)){
            iMax = itemNamesCounts.Count;
        } else {
            iMax = (pageNum*3*2)+(3*2);
        }

        for (int i = pageNum*3*2; i < iMax; i++){
            if (i == index){
                itemPanel += "> ";
                itemPanel += itemNamesCounts[i];
            } else {
                itemPanel += "  ";
                itemPanel += itemNamesCounts[i];
            }
        }

        return itemPanel;
    }
}
