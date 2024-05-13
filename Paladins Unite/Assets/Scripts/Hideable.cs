using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hideable : MonoBehaviour
{
    public GameObject toHide;
    public bool hideOnStart;

    // Start is called before the first frame update
    void Start()
    {
        if (hideOnStart){
            toHide.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hide(){
        toHide.SetActive(false);
    }

    public void Show(){
        toHide.SetActive(true);
    }
}
