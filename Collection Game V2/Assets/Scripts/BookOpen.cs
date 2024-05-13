using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class BookOpen : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public int pageNumber = 1;
    // public string bookTitle;

    public GameObject bookUI;
    public GameObject infoUI;
    public GameObject page1;
    public GameObject page2;
    public List<GameObject> creaturePanels;
    public TMP_Text inventoryInfo;
    public TMP_Text inventoryInfo2;
    public TMP_Text pageText;
    public TMP_Text travelInfo;

    public TMP_Dropdown dropdown;
    public GameObject mainChar;
    public Button pauseButton;
    public Button resumeButton;
    public Button nextPageButton;
    public Button prevPageButton;
    public Button saveButton;
    public Button saveQuitButton;
    public Button clearSaveButton;
    public Button TravelButton;
    public TMP_InputField TravelLocation;

    public ConfirmationPopUp popup;
    
    public GameObject inventoryObject;

    public AudioSource paperSoundEffect;

    private List<Vector3> townLocations = new List<Vector3>();
    private int maxPage = 1000;

    void Start()
    {
        inventoryObject = GameObject.FindGameObjectsWithTag("Inventory")[0];

        bookUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        // Add a listener to the inputField's OnEndEdit event
        // inputField.onEndEdit.AddListener(OnInfoEntered);

        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(Resume);
        nextPageButton.onClick.AddListener(NextPage);
        prevPageButton.onClick.AddListener(PrevPage);
        saveButton.onClick.AddListener(SaveInventory);
        saveQuitButton.onClick.AddListener(SaveQuitInventory);
        clearSaveButton.onClick.AddListener(ClearInventory);
        TravelButton.onClick.AddListener(Travel);
        dropdown.onValueChanged.AddListener(TeleportChar);

        pageNumber = 1;
        ShowPages();

        if (inventoryObject.GetComponent<Inventory>().items["Wings"]){
            dropdown.interactable = true;
        } else {
            dropdown.interactable = false;
        }

        FillDropdown();
    }

    void SaveInventory(){
        inventoryObject.GetComponent<Inventory>().SaveInventory();
    }

    void SaveQuitInventory(){
        inventoryObject.GetComponent<Inventory>().SaveInventory();
        Application.Quit();
    }

    void ClearInventory(){
        popup.infoText.text = "This will RESET all your progress! You\'ll lose all your creatures, coins, etc.";
        popup.Show();
        StartCoroutine(ConfirmClear());
    }

    void NextPage(){
        if (pageNumber+1 < maxPage){
            pageNumber ++;
        }
        paperSoundEffect.Play();
    }

    void PrevPage(){
        if (pageNumber-1 > 0){
            pageNumber --;
            paperSoundEffect.Play();
        }
    }

    void ShowPages(){
        pageText.text = "Page "+pageNumber.ToString();

        if (pageNumber == 1){
            page1.SetActive(true);
        } else {
            page1.SetActive(false);
        }

        if (pageNumber == 2){
            page2.SetActive(true);
        } else {
            page2.SetActive(false);
        }

        if (pageNumber >= 3){
            // for the 6 prefabs, fill it out w/ creatures (pagenumber-3) -> (pagenumber-3)+6
            int i = (pageNumber-3)*6;
            Inventory inventory = inventoryObject.GetComponent<Inventory>();
            try {
                foreach (GameObject panel in creaturePanels)
                {
                    panel.SetActive(true);
                    CreatureAttributes creature = inventory.creatures[i];
                    CreatureTab panelInfo = panel.GetComponent<CreatureTab>();
                    panelInfo.found = inventory.catalog[creature.creatureName];
                    panelInfo.creatureImg = creature.GetGameObjectImage();
                    if (panelInfo.found) {
                        panelInfo.name = creature.creatureName;
                        panelInfo.description = creature.description;
                    } else {
                        panelInfo.name = "???";
                        panelInfo.description = "???";
                    }
                    string tempStr = "";
                    if (creature.SnowSpawn){
                        tempStr += "snow ";
                    } if (creature.GrassSpawn){
                        tempStr += "grass ";
                    } if (creature.BeachSpawn){
                        tempStr += "beach ";
                    } if (creature.WaterSpawn){
                        tempStr += "water ";
                    }
                    tempStr += " | Rarity: ";
                    tempStr += creature.probability.ToString();
                    panelInfo.biomes = tempStr;
                    i++;
                }
            } catch (ArgumentOutOfRangeException) {
                // pass
                maxPage = pageNumber;
            }
        } else {
            foreach (GameObject panel in creaturePanels)
            {
                panel.SetActive(false);
            }
        }
    }

    void Pause ()
    {
        bookUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void Resume ()
    {
        bookUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    // private void OnInfoEntered(string info)
    // {
    //     // Update the displayText with the entered username
    //     bookTitle = info;
    // }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && inventoryObject.GetComponent<Inventory>().chapter > 0) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }

        if (GameIsPaused) {
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                NextPage();
            } if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                PrevPage();
            }
        }

        Inventory inventory = inventoryObject.GetComponent<Inventory>();

        inventoryInfo.text = "Number of creatures: " + inventory.inventory.Count.ToString() + "\nNumber of coins: " + inventory.coins.ToString() + "\nSeed: " + inventory.seed.ToString();
        inventoryInfo2.text = "Number of creatures: " + inventory.inventory.Count.ToString() + "\nNumber of coins: " + inventory.coins.ToString() + "\nNumber of food packs: " + inventory.food.ToString();
        travelInfo.text = "Travels Left: " + inventory.travels.ToString();

        ShowPages();
    }

    private void FillDropdown()
    {
        // Clear existing options in the dropdown
        dropdown.ClearOptions();

        // Create a list of string options for the dropdown
        List<string> options = new List<string>();
        options.Add("Location");

        Town[] towns = FindObjectsOfType<Town>();
        Debug.Log(towns.Length);

        // Add town names to the options list
        foreach (Town town in towns)
        {
            Vector3 position = town.transform.position;
            townLocations.Add(position);

            int x = Mathf.RoundToInt(position.x);
            int y = Mathf.RoundToInt(position.y);
            string name = town.TownName;
            Debug.Log(name);
            string optionText = name + ": " + x + ", " + y;
            options.Add(optionText);
        }

        // Add the options to the dropdown
        dropdown.AddOptions(options);
    }

    private void TeleportChar(int value)
    {
        mainChar.transform.position = townLocations[value-1] + new Vector3(0, -1, 0);
    }

    private void Travel() {
        if (inventoryObject.GetComponent<Inventory>().travels > 0){
            popup.infoText.text = "This will take you to a new map. If you want to return to this map in the future, make sure you note the \"Seed\"";
            popup.Show();
            StartCoroutine(WaitForPopupToHide());
        }
    }

    private IEnumerator WaitForPopupToHide()
    {
        // Wait until the popup is hidden (you can customize the condition here)
        while (popup.showing)
        {
            yield return null;
        }

        if (popup.accepted){
            if (int.TryParse(TravelLocation.text, out _)){
                inventoryObject.GetComponent<Inventory>().seed = int.Parse(TravelLocation.text);
            } else {
                inventoryObject.GetComponent<Inventory>().seed = UnityEngine.Random.Range(0, 100000);
            }
            SceneManager.LoadScene("MainScene");
            inventoryObject.GetComponent<Inventory>().travels--;
        }
    }

    private IEnumerator ConfirmClear()
    {
        // Wait until the popup is hidden (you can customize the condition here)
        while (popup.showing)
        {
            yield return null;
        }

        if (popup.accepted){
            inventoryObject.GetComponent<Inventory>().ClearSave();
        }
    }
}