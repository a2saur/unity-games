using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MeetChars : MonoBehaviour
{
    public RawImage imgSpot;
    public TMP_Text nameSpot;
    public TMP_Text descSpot;

    public Texture[] charImgs;
    public string[] names;
    public string[] descriptions;

    private int idx = 0;

    // Start is called before the first frame update
    void Start()
    {
        imgSpot.texture = charImgs[idx];
        nameSpot.text = names[idx];
        descSpot.text = descriptions[idx];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            nextPage();
        } if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            prevPage();
        }
    }

    public void nextPage(){
        if (idx+1 < charImgs.Length){
            idx++;
        } else {
            idx = 0;
        }

        imgSpot.texture = charImgs[idx];
        nameSpot.text = names[idx];
        descSpot.text = descriptions[idx];
    }

    public void prevPage(){
        if (idx-1 > 0){
            idx--;
        } else {
            idx = charImgs.Length-1;
        }

        imgSpot.texture = charImgs[idx];
        nameSpot.text = names[idx];
        descSpot.text = descriptions[idx];
    }

    public void startButton(){
        SceneManager.LoadScene("Start");
    }

}
