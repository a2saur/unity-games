using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;

    public GameObject keysMenu;
    public GameObject keybindingButton;
    private int keysMenu_numRows = 7;
    private int keysMenu_xSpacing = 100;
    private int keysMenu_ySpacing = 50;

    public GameObject inventoryMenu;
    public GameObject inventorySlot;
    public List<ItemSlot> inventorySlots;
    public Toggle groupingToggle;
    private const int numCols = 10;
    private const int numRows = 5;
    private const int xSpacing = 75;
    private const int ySpacing = 75;

    public string currentMenu = "keys";

    private bool buffer = false;
    private bool changed = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // making keys for key binding menu
        GameObject tempButton;
        int i = 0;
        int dx;
        int dy;
        // starts from the top left
        foreach(KeyValuePair<string, string> keyLabel in SettingsManager.keycodes){
            tempButton = Instantiate(keybindingButton, keysMenu.transform);
            tempButton.GetComponent<KeyBindingButton>().SetValues(keyLabel.Key, SettingsManager.GetKeyCode(keyLabel.Value));

            dx = (int) (i/keysMenu_numRows) * keysMenu_xSpacing;
            dy = (i % keysMenu_numRows) * keysMenu_ySpacing;
            tempButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-225+dx, 200-dy, 0);
            i++;
        }

        // making slots for inventory, starts from the top left
        i = 0;
        for (int y = 0; y < numRows; y++){
            for (int x = 0; x < numCols; x++){
                tempButton = Instantiate(inventorySlot, inventoryMenu.transform);
                tempButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-335+(x*xSpacing), 200-(y*ySpacing), 0);
                inventorySlots.Add(tempButton.GetComponent<ItemSlot>());
                i++;
            }
        }
        groupingToggle.isOn = SettingsManager.groupingItems;
    }

    // Update is called once per frame
    void Update()
    {
        if (buffer){
            if (Input.GetKeyUp(SettingsManager.pauseButton)){
                SettingsManager.CallPauseMenu();
                buffer = false;
                changed = true;
            }
        } else if (SettingsManager.pauseMenu){
            if (changed){
                pauseMenu.SetActive(true);
                if (currentMenu == "keys"){
                    keysMenu.SetActive(true);
                } else {
                    keysMenu.SetActive(false);
                } 
                
                if (currentMenu == "inventory"){
                    inventoryMenu.SetActive(true);
                } else {
                    inventoryMenu.SetActive(false);
                }
                
                foreach(ItemSlot iS in inventorySlots){
                    iS.Reset();
                }
                
                if (SettingsManager.groupingItems){
                    Dictionary<string, int> inventoryCounts = new Dictionary<string, int>();
                    int currentCount;
                    // count first
                    for (int i = 0; i < SettingsManager.inventory.Count; i++){
                        if (inventoryCounts.TryGetValue(SettingsManager.inventory[i], out currentCount)) {
                            inventoryCounts[SettingsManager.inventory[i]] += 1;
                        } else {
                            inventoryCounts[SettingsManager.inventory[i]] = 1;
                        }
                    }
                    
                    int j = 0;
                    foreach(string itemKeyName in inventoryCounts.Keys){
                        inventorySlots[j].GetComponent<ItemSlot>().SetItem(itemKeyName, inventoryCounts[itemKeyName]);
                        j++;
                    }

                    for (int i = 0; i < SettingsManager.inventory.Count-inventoryCounts.Keys.Count; i++){
                        inventorySlots[inventorySlots.Count-(i+1)].GrayOut();
                    }
                } else {
                    for (int i = 0; i < SettingsManager.inventory.Count; i++){
                        inventorySlots[i].GetComponent<ItemSlot>().SetItem(SettingsManager.inventory[i]);
                    }
                }

                changed = false;
            }

            if (Input.GetKeyDown(SettingsManager.pauseButton)){
                buffer = true;
            }
        } else {
            pauseMenu.SetActive(false);
            changed = true;
        }

        // TODO check for when the page changes
    }

    public void SetToggle(bool state)
    {
        SettingsManager.groupingItems = !SettingsManager.groupingItems;
        groupingToggle.isOn = SettingsManager.groupingItems;
        changed = true;
    }
}
