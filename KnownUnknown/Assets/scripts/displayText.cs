using UnityEngine;

public class displayText : MonoBehaviour
{
    public Image blankCharImg;
    public Transform parentCanvas;

    public string text;
    public string charTypePrefix;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Image newImage = Instantiate(blankCharImg, Vector3.zero, Quaternion.identity);
        newImage.transform.SetParent(parentCanvas, false);
        newImage.rectTransform.anchoredPosition = new Vector2(0, 0);
    }
}
