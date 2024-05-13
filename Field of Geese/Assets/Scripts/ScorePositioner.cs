using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePositioner : MonoBehaviour
{
    // private Vector3 originalPos;
    // public Vector3 pausedSpot;
    public SetManager SETMANAGER;
    public GameObject bread;
    public GameObject stars;
    public GameObject inGameAnchor;
    public GameObject pauseAnchor;

    // Start is called before the first frame update
    void Start()
    {
        SETMANAGER = GameObject.FindWithTag("SetManager").GetComponent<SetManager>();
        // originalPos = scores.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (SETMANAGER.isPaused){
            // scores.transform.position = pausedSpot;
            bread.transform.SetParent(pauseAnchor.transform);
            stars.transform.SetParent(pauseAnchor.transform);
        } else {
            // scores.transform.position = originalPos;
            bread.transform.SetParent(inGameAnchor.transform);
            stars.transform.SetParent(inGameAnchor.transform);
        }

        bread.GetComponent<RectTransform>().localPosition = new Vector3(85, 0, 0);
        stars.GetComponent<RectTransform>().localPosition = new Vector3(-45, 0, 0);
    }
}
