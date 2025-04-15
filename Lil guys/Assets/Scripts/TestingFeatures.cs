using UnityEngine;
using UnityEngine.UI;

public class TestingBehaviors : MonoBehaviour
{
    public Button myButton;

    void Start()
    {
        myButton = GetComponent<Button>();

        myButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        // Debug.Log("You have clicked the button!");

        SettingsManager.inventory.Add("tuna");
    }
}