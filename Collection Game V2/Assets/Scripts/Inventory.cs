using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class InventoryData
{
    public List<CreatureAttributes> creatures;
    public List<string> inventory;
    public List<string> itemsPt1;
    public List<bool> itemsPt2;
    public List<string> catalogPt1;
    public List<bool> catalogPt2;
    // public Dictionary<string, bool> clothes;
    public int coins;
    public int food;
    public Vector3 charLoc;
    public int seed;
    public int travels;
    public float chapter;
}

public class Inventory : MonoBehaviour
{
    public int advice;
    public List<CreatureAttributes> creatures;
    public List<string> inventory = new List<string>();
    public Dictionary<string, bool> items = new Dictionary<string, bool>()
    {
        {"Wings", false},
        {"Snorkel", false},
    };
    public List<string> itemsPt1;
    public List<bool> itemsPt2;
    public Dictionary<string, bool> catalog = new Dictionary<string, bool>();
    public List<string> catalogPt1;
    public List<bool> catalogPt2;
    public List<HouseAttributes> houseOptions;

    public List<ClothingAttributes> clothesOptions;
    public Dictionary<string, bool> clothes = new Dictionary<string, bool>();

    public int coins = 0;
    public int food = 0;
    public Vector3 charLoc;
    public int seed;
    public int travels = 0;
    public float chapter;
    public bool instructionsGiven = false;

    private string savePath;

    // Start is called before the first frame update
    void Start()
    {
        advice = 0;
        chapter = 0;
        seed = Random.Range(0, 100000);
        Random.InitState(seed);
        Debug.Log("Seed: "+seed);
        DontDestroyOnLoad(this.gameObject);

        savePath = Application.persistentDataPath + "/inventory.json";

        // ClearSave();
        LoadInventory();

        // Populate the creature dictionary if it's empty
        if (catalog.Count == 0)
        {
            foreach (CreatureAttributes creature in creatures)
            {
                if (creature != null)
                {
                    string creatureName = creature.creatureName;
                    Debug.Log(creatureName);
                    catalog[creatureName] = false;
                }
            }
        }
        // Populate the creature dictionary if it's empty
        if (clothes.Count == 0)
        {
            foreach (ClothingAttributes clothing in clothesOptions)
            {
                if (clothing != null)
                {
                    string clothingName = clothing.clothingName;
                    clothes[clothingName] = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        itemsPt1 = new List<string>(items.Keys);
        itemsPt2 = new List<bool>(items.Values);

        catalogPt1 = new List<string>(catalog.Keys);
        catalogPt2 = new List<bool>(catalog.Values);

    //     if (Input.GetKeyDown(KeyCode.S))
    //     {
    //         SaveInventory();
    //     }

    //     if (Input.GetKeyDown(KeyCode.L))
    //     {
    //         LoadInventory();
    //     }
    }

    public void SaveInventory()
    {
        string jsonData = JsonUtility.ToJson(this);
        File.WriteAllText(savePath, jsonData);

        Debug.Log("Inventory saved!");
    }

    public void LoadInventory()
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            JsonUtility.FromJsonOverwrite(jsonData, this);

            items = MapListToDictionary(itemsPt1, itemsPt2);//itemsPt1.Select((k, i) => new { k, v = itemsPt2[i] }).ToDictionary(x => x.k, x => x.v);
            catalog = MapListToDictionary(catalogPt1, catalogPt2);//catalogPt1.Select((k, i) => new { k, v = catalogPt2[i] }).ToDictionary(x => x.k, x => x.v);

            Debug.Log("Inventory loaded!");
        }
        else
        {
            Debug.Log("No saved inventory found.");
        }
    }

    Dictionary<string, bool> MapListToDictionary(List<string> Pt1, List<bool> Pt2)
    {
        Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
        int count = Pt1.Count;//Get count of the keys
        for (int i = 0; i < count; i++)//loop through all the indexes of the key list
        {
            if (i < Pt2.Count)//make sure that index exists in the value list
            {
                dictionary[Pt1[i]] = Pt2[i];//if an entry exists already then overwrite otherwise add a new entry.
            }
        }
        return dictionary;
    }

    public void ClearSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Save cleared!");
        }
        else
        {
            Debug.Log("No save file found to clear.");
        }
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Inventory : MonoBehaviour
// {
//     public List<CreatureAttributes> creatures;
//     public List<string> inventory = new List<string>();
//     public Dictionary<string, bool> items = new Dictionary<string, bool>()
//                                             {
//                                                 {"Wings", false},
//                                                 {"Snorkel", false},
//                                             };
//     public Dictionary<string, bool> catalog = new Dictionary<string, bool>();

//     public int coins = 0;
//     public Vector3 charLoc;
//     public int seed;

//     // Start is called before the first frame update
//     void Start()
//     {
//         seed = Random.Range(0, 100000);
//         DontDestroyOnLoad(this.gameObject);

//         // Populate the creature dictionary
//         foreach (CreatureAttributes creature in creatures)
//         {
//             if (creature != null)
//             {
//                 string creatureName = creature.creatureName;
//                 Debug.Log(creatureName);
//                 catalog[creatureName] = false;
//             }
//         }
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
