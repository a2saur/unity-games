using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class MessageManager : MonoBehaviour
{
    public GameObject messagePanel;
    public TextMeshProUGUI messageBox;
    public GameObject waitingSymbol;
    public string[] lines;
    public float currentTextSpeed;

    public AudioSource audioPlayer;
    public AudioClip defaultLetterSound;
    private Dictionary<char, AudioClip> letterSounds = new Dictionary<char, AudioClip>();
    private int letterWait = 1;

    public bool running = false;
    public bool waiting = false;
    public bool done = false;

    private int index;
    private float cooldown = 0.5f;
    private float cooldownCounter;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();

        done = true;
        
        messagePanel.SetActive(false);
        messageBox.text = string.Empty;
        currentTextSpeed = SettingsManager.defaultTextSpeed;

        // load letter sounds
        // foreach (char letter in "abcdefghijklmnopqrstuvwxyz")
        // {
        //     string path = $"Letters/{letter}";
        //     AudioClip clip = Resources.Load<AudioClip>(path);
        //     if (clip != null)
        //     {
        //         letterSounds[letter] = clip;
        //     }
        //     else
        //     {
        //         Debug.LogWarning($"No sound found for letter: {letter}");
        //     }
        // }
    }

    void Update()
    {
        if (running){
            // finish line/next line
            if (Input.GetKeyDown(SettingsManager.interactButton)){
                if (messageBox.text == lines[index]){
                    NextLine();
                } else {
                    currentTextSpeed = SettingsManager.defaultTextSpeed/4;
                }
            }

            if (waiting == true){
                waitingSymbol.SetActive(true);
            } else {
                waitingSymbol.SetActive(false);
            }
        } else {
            cooldownCounter -= Time.deltaTime;
        }
    }

    public void StartDialogue(Dialogue newDialogue){
        if (done && cooldownCounter <= 0){
            // set the lines
            lines = newDialogue.lines;
            // call start
            StartText();
        }
    }

    public void StartText()//string[] newLines)
    {
        if (done){
            SettingsManager.Pause();

            // lines = newLines;
            messagePanel.SetActive(true);

            index = 0;
            running = true;
            waiting = false;
            done = false;

            messageBox.text = string.Empty;
            StartCoroutine(TypeLine());
        }
    }
    
    IEnumerator TypeLine()
    {
        int i = 0;
        bool closing = false;
        foreach (char c in lines[index].ToCharArray()){
            messageBox.text += c;
            i ++;
            if (closing && c == '>'){
                closing = false;
            } if (!closing){
                if (c == '<'){
                    closing = true;
                } else {
                    // if (c != ' ' && i % letterWait == 0){
                    //     if (letterSounds.TryGetValue(c, out AudioClip clip)) {
                    //         audioPlayer.PlayOneShot(clip);
                    //     } else {
                    //         // no sound
                    //         audioPlayer.PlayOneShot(defaultLetterSound);
                    //     }
                    // }
                    yield return new WaitForSeconds(currentTextSpeed);
                }
            }
        }

        waiting = true;
    }

    void NextLine()
    {
        if (index < lines.Length -1) {
            index++;
            
            waiting = false;
            currentTextSpeed = SettingsManager.defaultTextSpeed;
            messageBox.text = string.Empty;

            StartCoroutine(TypeLine());
        } else {
            running = false;
            waiting = false;
            done = true;
            cooldownCounter = cooldown;
            messagePanel.SetActive(false);

            SettingsManager.Resume();
        }
    }

    public bool CanStart(){
        if (cooldownCounter < 0 && !running){
            return true;
        } else {
            return false;
        }
    }
}
