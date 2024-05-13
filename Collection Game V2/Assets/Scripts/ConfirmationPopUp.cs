using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfirmationPopUp : MonoBehaviour
{
    public Button confirm;
    public Button cancel;
    public TMP_Text infoText;
    public GameObject popup;

    public bool showing;
    public bool accepted = false;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    public void Accept(){
        accepted = true;
        Hide();
    }

    public void Reject(){
        accepted = false;
        Hide();
    }

    public void Show ()
    {
        accepted = false;
        popup.SetActive(true);
        showing = true;
    }

    public void Hide ()
    {
        popup.SetActive(false);
        showing = false;
    }
}
