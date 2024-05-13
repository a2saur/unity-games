using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;

    public enum ItemType { Heal, Attack }
    public ItemType itemType;

    public string GetName(){
        return itemName;
    }
}
