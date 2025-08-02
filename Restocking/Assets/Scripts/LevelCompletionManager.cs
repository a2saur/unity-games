using UnityEngine;

public class LevelCompletionManager : MonoBehaviour
{
    public int numBoxesForComplete;
    public int numBoxes;

    public GameObject completeScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
    }

    public void DroppedOffBox(){
        numBoxes++;
    }
}
