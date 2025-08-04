using UnityEngine;

public class LevelCompletionManager : MonoBehaviour
{
    public int numBoxesForComplete;
    public int numBoxes;

    public GameObject completeScreen;
    public GameObject startScreen;

    private float counter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        counter = 1.15f;
        numBoxes = 0;
        completeScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (numBoxes >= numBoxesForComplete){
            // Yay completed
            completeScreen.SetActive(true);
        }
        if (counter > 0){
            counter -= Time.deltaTime;
            if (counter <= 0.2f){
                startScreen.SetActive(false);
            }
        }
    }

    public void DroppedOffBox(){
        numBoxes++;
    }
}
