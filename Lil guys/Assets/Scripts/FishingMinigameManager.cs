using UnityEngine;
using UnityEngine.UI;

public class FishingMinigameManager : MonoBehaviour
{
    public RectTransform pool;
    public RectTransform progressBar; // max height 470
    private const int maxHeight = 470;
    private const int maxTimer = 10;
    private float fishingTimer;
    public Vector2 startPosition;
    public Vector2 targetPosition;

    public RectTransform fishSilhouette;
    public Vector2[] fishPositions; // up right down left
    public GameObject[] fishButtons;
    public int[] fishRotations;
    private KeyCode[] buttons = { 
                                    SettingsManager.downArrow, 
                                    SettingsManager.leftArrow, 
                                    SettingsManager.upArrow, 
                                    SettingsManager.rightArrow, 
    };
    public float popupSpeed = 5f;
    public float speed = 5f;
    private const float fishingGain = 2f;
    
    private float t = 0f;
    private float rt = 0f;
    private bool starting = true;
    private bool fishing = true;

    private bool fishMoving;
    private bool fishCentering;
    private bool fishWaiting;

    private Vector2 prevPosition;
    private Quaternion prevRotation;
    private int fishSpot = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pool.anchoredPosition = startPosition;

        prevPosition = fishSilhouette.anchoredPosition;
        prevRotation = fishSilhouette.localRotation;
        fishMoving = false;
        fishCentering = false;

        fishingTimer = maxTimer * 0.5f;
        progressBar.sizeDelta = new Vector2(progressBar.sizeDelta.x, (fishingTimer/maxTimer) * maxHeight);

        foreach (GameObject obj in fishButtons){
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!SettingsManager.playing && fishing){
            if (starting) {
                // pull up pool
                t += Time.deltaTime * popupSpeed;
                float easedT = 1f - Mathf.Pow(1f - t, 3); // Ease-out cubic function
                pool.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, easedT);
                
                if (t >= 1f) {
                    starting = false; // Stop when reaching the target
                    fishingTimer = maxTimer * 0.5f;
                }
            } else {
                if (!fishCentering){
                    fishingTimer -= Time.deltaTime;
                } else {
                    fishingTimer -= Time.deltaTime * 0.15f;
                }

                if (fishingTimer >= maxTimer){
                    // caught it!
                    progressBar.sizeDelta = new Vector2(progressBar.sizeDelta.x, maxHeight);
                    fishing = false;
                } else if (fishingTimer <= 0){
                    // lost it...
                    fishing = false;
                } else {
                    progressBar.sizeDelta = new Vector2(progressBar.sizeDelta.x, (fishingTimer/maxTimer) * maxHeight);
                }

                // fishing time!
                if (fishMoving){
                    // rotate fish
                    rt = Time.deltaTime * speed * 3;
                    fishSilhouette.localRotation = Quaternion.Slerp(prevRotation, Quaternion.Euler(0, 0, fishRotations[fishSpot]), t);

                    // move fish
                    t += Time.deltaTime * speed;
                    float easedT = 1f - Mathf.Pow(1f - t, 3); // Ease-out cubic function
                    fishSilhouette.anchoredPosition = Vector2.Lerp(prevPosition, fishPositions[fishSpot], easedT);

                    if (t >= 1f) {
                        fishMoving = false;
                        fishWaiting = true;

                        prevRotation = fishSilhouette.localRotation;
                        prevPosition = fishSilhouette.anchoredPosition;
                        t = 0;
                        rt = 0;
                    } else if (t >= 0.15){
                        fishButtons[fishSpot].SetActive(true);
                        if (Input.GetKeyDown(buttons[fishSpot])){
                            fishingTimer += fishingGain;
                            fishButtons[fishSpot].SetActive(false);
                            fishMoving = false;
                            fishCentering = true;

                            prevRotation = fishSilhouette.localRotation;
                            prevPosition = fishSilhouette.anchoredPosition;
                            t = 0;
                            rt = 0;
                        }
                    } 
                } else if (fishWaiting) {
                    fishButtons[fishSpot].SetActive(true);
                    if (Input.GetKeyDown(buttons[fishSpot])){
                        // TODO add some progress on the progress bar
                        fishButtons[fishSpot].SetActive(false);
                        fishWaiting = false;
                        fishCentering = true;
                    }
                } else if (fishCentering) {
                    // go back to center
                    // rotate fish
                    rt = Time.deltaTime * speed * 3;
                    fishSilhouette.localRotation = Quaternion.Slerp(prevRotation, Quaternion.Euler(0, 0, 180+prevRotation.z), t);

                    // move fish
                    t += Time.deltaTime * speed;
                    float easedT = 1f - Mathf.Pow(1f - t, 3); // Ease-out cubic function
                    fishSilhouette.anchoredPosition = Vector2.Lerp(prevPosition, new Vector2(0, 0), easedT);
                    
                    if (t >= 1f) {
                        fishCentering = false;
                    }
                } else {
                    // pick a spot
                    fishSpot = Random.Range(0, 4);
                    fishCentering = false;
                    fishMoving = true;

                    prevRotation = fishSilhouette.localRotation;
                    prevPosition = fishSilhouette.anchoredPosition;

                    t = 0;
                    rt = 0;
                }
            }
        }
    }
}
