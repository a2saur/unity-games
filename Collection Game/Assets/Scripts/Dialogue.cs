using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public GameObject buttons;

    public bool running;

    private int index;

    public int letterWait = 1;

    public AudioSource audioPlayer;
    public AudioClip a;
    public AudioClip b;
    public AudioClip c;
    public AudioClip d;
    public AudioClip e;
    public AudioClip f;
    public AudioClip g;
    public AudioClip h;
    public AudioClip i;
    public AudioClip j;
    public AudioClip k;
    public AudioClip l;
    public AudioClip m;
    public AudioClip n;
    public AudioClip o;
    public AudioClip p;
    public AudioClip q;
    public AudioClip r;
    public AudioClip s;
    public AudioClip t;
    public AudioClip u;
    public AudioClip v;
    public AudioClip w;
    public AudioClip x;
    public AudioClip y;
    public AudioClip z;
    public AudioClip dot;

    private Dictionary<char, AudioClip> audioAlphabets;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        audioAlphabets = new Dictionary<char, AudioClip>() {
            {'a', a},
            {'b', b},
            {'c', c},
            {'d', d},
            {'e', e},
            {'f', f},
            {'g', g},
            {'h', h},
            {'i', i},
            {'j', j},
            {'k', k},
            {'l', l},
            {'m', m},
            {'n', n},
            {'o', o},
            {'p', p},
            {'q', q},
            {'r', r},
            {'s', s},
            {'t', t},
            {'u', u},
            {'v', v},
            {'w', w},
            {'x', x},
            {'y', y},
            {'z', z},
            {',', dot},
            {'.', dot},
            {'!', dot},
            {'?', dot},
            {';', dot},
            {':', dot},
            {'-', dot},
            {'*', dot},
            {'(', dot},
            {')', dot},
            {'<', dot},
            {'>', dot},
            {'\'', dot},
            {'\"', dot},
            {'1', dot},
            {'2', dot},
            {'3', dot},
            {'4', dot},
            {'5', dot},
            {'6', dot},
            {'7', dot},
            {'8', dot},
            {'9', dot},
            {'0', dot},
        };

        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index]){
                NextLine();
            } else {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        buttons.SetActive(false);
        running = true;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    public void StopDialogue()
    {
        StopAllCoroutines();
    }

    IEnumerator TypeLine()
    {
        int i = 0;
        foreach (char c in lines[index].ToCharArray()){
            textComponent.text += c;
            if (c != ' ' && i % letterWait == 0){
                audioPlayer.PlayOneShot(audioAlphabets[Char.ToLower(c)]);
            }
            i ++;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length -1) {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        } else {
            running = false;
            buttons.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
