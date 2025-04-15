using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyBindingButton : MonoBehaviour
{
    public string keyName;
    public TextMeshProUGUI buttonName;
    public TextMeshProUGUI buttonLabel;
    public Button bindingButton;

    public bool listening = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bindingButton.onClick.AddListener(OnButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if (listening){
            if (Input.anyKeyDown) {
                foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(key)) {
                        // Debug.Log(keyName);
                        SettingsManager.SetKeyBinding(keyName, key);
                        SetButtonName(key);

                        SettingsManager.keyBindingListening = false;
                        listening = false;
                        break; // Exit loop after detecting the first key press
                    }
                }
            }
        }
    }

    public void SetValues(string _keyName, KeyCode buttonCode){
        SetButtonName(buttonCode);
        SetKeyName(_keyName);
    }

    public void SetButtonName(KeyCode buttonCode){
        buttonName.text = buttonCode.ToString();
    }

    public void SetButtonName(string buttonCode){
        buttonName.text = buttonCode.ToString();
    }

    public void SetKeyName(string _keyName){
        keyName = _keyName;
        buttonLabel.text = _keyName;
    }

    public void OnButtonClicked(){
        if (!SettingsManager.keyBindingListening){
            SettingsManager.keyBindingListening = true;
            listening = true;

            SetButtonName("...");
        }
    }
}
