using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsController : MonoBehaviour
{
    public Button settingsButton;
    public GameObject mainMenuPanel;
    public GameObject levelSelectPanel;
    public Slider musicSlider;
    public Slider soundSlider;
    public Slider speedSlider;
    public TMP_Text musicCounter;
    public TMP_Text soundCounter;
    public TMP_Text speedCounter;
    public string menuSelection = "Main menu";
    public string[] levelSceneNames;

    // void Awake()
    // {
    //     DontDestroyOnLoad(this.gameObject);
    // }

    void Start(){
        musicSlider.value = ((float) SettingsManager.musicVolume)/10;
        soundSlider.value = ((float) SettingsManager.soundVolume)/10;
        speedSlider.value = ((float) SettingsManager.robotSpeed)/10;

        musicCounter.text = SettingsManager.musicVolume.ToString();
        soundCounter.text = SettingsManager.soundVolume.ToString();
        speedCounter.text = SettingsManager.robotSpeed.ToString();

        mainMenuPanel.SetActive(false);
        levelSelectPanel.SetActive(false);

        settingsButton.onClick.AddListener(ToggleSettings);
    }

    void Update(){
        if (!SettingsManager.playing){
            if (menuSelection == "Main menu"){
                SettingsManager.musicVolume = (int) (musicSlider.value*10);
                SettingsManager.soundVolume = (int) (soundSlider.value*10);
                SettingsManager.robotSpeed = (int) (speedSlider.value*10);
                musicCounter.text = SettingsManager.musicVolume.ToString();
                soundCounter.text = SettingsManager.soundVolume.ToString();
                speedCounter.text = SettingsManager.robotSpeed.ToString();
            }
        }
    }

    public void ToggleSettings(){
        if (SettingsManager.playing){
            SettingsManager.Pause();
            menuSelection = "Main menu";
            mainMenuPanel.SetActive(true);
        } else {
            SettingsManager.Resume();
            mainMenuPanel.SetActive(false);
            levelSelectPanel.SetActive(false);
        }
    }

    public void LevelSelect(){
        SettingsManager.Pause();
        menuSelection = "Level select";
        mainMenuPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }

    public void MainMenu(){
        SettingsManager.Pause();
        menuSelection = "Main menu";
        mainMenuPanel.SetActive(true);
        musicSlider.value = ((float) SettingsManager.musicVolume)/10;
        soundSlider.value = ((float) SettingsManager.soundVolume)/10;
        speedSlider.value = ((float) SettingsManager.robotSpeed)/10;
        musicCounter.text = SettingsManager.musicVolume.ToString();
        soundCounter.text = SettingsManager.soundVolume.ToString();
        speedCounter.text = SettingsManager.robotSpeed.ToString();
        levelSelectPanel.SetActive(false);
    }

    public void SwitchToScene(int sceneNum){
        SceneManager.LoadScene(levelSceneNames[sceneNum]);
    }

    public void SwitchToSceneByName(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    public void Quit(){
        Application.Quit();
    }
}
