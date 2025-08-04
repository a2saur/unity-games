using UnityEngine;
using UnityEngine.UI;

public class ImageAnimator : MonoBehaviour
{
    public Image imageObj;
    public Sprite[] frames;
    
    private float counter;
    private int idx;
    private float timeWait = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        counter = timeWait;
        idx = 0;
    }

    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;
        if (counter <= 0){
            counter = timeWait;
            idx ++;
            imageObj.sprite = frames[idx%frames.Length];
        }
    }
}
