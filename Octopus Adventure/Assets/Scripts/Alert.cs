using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Alert : MonoBehaviour
{
    public TextMeshProUGUI messageBox;

    public void SetAlert(string newText){
        messageBox.text = newText;
        StartCoroutine(CloseAlert());
    }

    IEnumerator CloseAlert()
    {
        yield return new WaitForSeconds(3);

        gameObject.SetActive(false);
    }
}
