using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class DisplayCreatures : MonoBehaviour
{
    public Animator transitionAnimator;
    public Inventory inventoryObject;
    public TMP_Text buttonText;
    public TMP_Text coinsText;

    private int rowLen = 10;
    private string previousScene;
    private List<CreatureAttributes> creaturesOnScreen = new List<CreatureAttributes>();
    private List<int> selectedCreatures = new List<int>();
    private Dictionary<string, CreatureAttributes> creatureDictionary;

    private void Start()
    {
        transitionAnimator = GameObject.FindGameObjectsWithTag("SceneTransition")[0].GetComponent<Animator>();
        inventoryObject = GameObject.FindGameObjectsWithTag("Inventory")[0].GetComponent<Inventory>();

        // Create and populate the dictionary
        creatureDictionary = new Dictionary<string, CreatureAttributes>();

        foreach (CreatureAttributes creature in inventoryObject.creatures)
        {
            string creatureName = creature.creatureName;
            creatureDictionary[creatureName] = creature;
        }

        displayPage();

        previousScene = PlayerPrefs.GetString("PreviousScene", "");

        if (previousScene == "ShopScene")
        {
            buttonText.text = "Sell!";
        } else if (previousScene == "MainScene") {
            buttonText.text = "Close";
        } else {
            buttonText.text = "Error";
        }
    }

    private void Update(){
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            Vector3 mousePosition = Input.mousePosition;
            // Convert the screen position to world position
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0;

            Debug.Log("Mouse click position: " + worldPosition);
            
            // Use the worldPosition as needed for your logic

            // int x = 0;
            // int y = 0;
            Vector3 upperLeftCorner = GetUpperLeftCornerPosition();
            Debug.Log("Upper position: " + upperLeftCorner);

            // foreach (string creatureName in inventoryObject.inventory)
            // {
            //     Instantiate(creatureDictionary[creatureName], upperLeftCorner+new Vector3(x+1.5f, y-1.5f, 0), Quaternion.identity);
            //     x++;
            //     if (x >= rowLen) {
            //         y++;
            //         x = 0;
            //     }
            // }

            Vector3 temp = upperLeftCorner-worldPosition;
            temp += new Vector3(1.5f, -1.5f, 0);
            Debug.Log("Temp: " + temp);

            int idx = Mathf.RoundToInt(-1*temp.x) + (Mathf.RoundToInt(temp.y) * rowLen);
            Debug.Log(idx);

            if (idx < creaturesOnScreen.Count){
                if (previousScene == "ShopScene"){
                    if (selectedCreatures.Contains(idx)){
                        // take out
                        Material material = new Material(creaturesOnScreen[idx].GetComponent<SpriteRenderer>().sharedMaterial);
                        material.color = new Color(material.color.r, material.color.g, material.color.b, 1f);
                        
                        creaturesOnScreen[idx].GetComponent<Renderer>().material = material;

                        selectedCreatures.Remove(idx);
                    } else {
                        // add in
                        Material material = new Material(creaturesOnScreen[idx].GetComponent<SpriteRenderer>().sharedMaterial);
                        material.color = new Color(material.color.r, material.color.g, material.color.b, 0.5f);
                        
                        creaturesOnScreen[idx].GetComponent<Renderer>().material = material;

                        selectedCreatures.Add(idx);
                    }
                }
            }
        }

        if (previousScene == "ShopScene"){
            int tempCoins = 0;
            foreach (int index in selectedCreatures)
            {
                if (index >= 0 && index < inventoryObject.inventory.Count)
                {
                    tempCoins += creaturesOnScreen[index].coins;
                }
            }
            coinsText.text = "Coins to Earn: " + tempCoins.ToString();
        } else {
            coinsText.text = "";
        }
    }

    Vector3 GetUpperLeftCornerPosition()
    {
        Camera mainCamera = Camera.main;
        Vector3 screenPosition = new Vector3(0f, Screen.height, 0f);
        Vector3 upperLeftCorner = mainCamera.ScreenToWorldPoint(screenPosition);
        upperLeftCorner.z = 0f; // Set the z-coordinate to your desired value

        return upperLeftCorner;
    }

    void displayPage(){
        int x = 0;
        int y = 0;
        Vector3 upperLeftCorner = GetUpperLeftCornerPosition();
        foreach (string creatureName in inventoryObject.inventory)
        {
            CreatureAttributes instantiatedObject = Instantiate(creatureDictionary[creatureName], (upperLeftCorner+new Vector3(x+1.5f, ((-1*y)-1.5f), 0)), Quaternion.identity);
            creaturesOnScreen.Add(instantiatedObject);
            x++;
            if (x >= rowLen) {
                y++;
                x = 0;
            }
        }
    }

    int DescendingComparison(int a, int b)
    {
        return b.CompareTo(a);
    }

    public void changeScene(){
        if (previousScene == "MainScene")
        {
            transitionAnimator.SetTrigger("SceneTransition");
            StartCoroutine(DelayedSceneChange(1f, "MainScene"));
            // SceneManager.LoadScene("MainScene");
        }
        else if (previousScene == "ShopScene")
        {
            selectedCreatures.Sort(DescendingComparison);
            // Debug.Log(selectedCreatures[0]);
            foreach (int index in selectedCreatures)
            {
                if (index >= 0 && index < inventoryObject.inventory.Count)
                {
                    inventoryObject.coins += creaturesOnScreen[index].coins;
                    inventoryObject.inventory.RemoveAt(index);
                }
            }
            // SceneManager.LoadScene("ShopScene");
            // Load the specified scene
            // animator.Play("SceneTransitionStartStart");
            // animator.SetTrigger("SceneTransition");
            // StartCoroutine(DelayedSceneChange(1f, "ShopScene"));
            transitionAnimator.SetTrigger("SceneTransition");
            StartCoroutine(DelayedSceneChange(1f, "ShopScene"));
        } else {
            Debug.Log("AAA");
        }
    }

    IEnumerator DelayedSceneChange(float delay, string sceneToLoad){
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(sceneToLoad);
    }
}