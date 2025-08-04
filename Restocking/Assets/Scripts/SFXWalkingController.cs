using UnityEngine;

public class SFXWalkingController : MonoBehaviour
{
    public AudioSource audioS;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        audioS.volume = ((float) SettingsManager.soundVolume)/25.0f;
    }

    public void StartWalking() {
        audioS.Play();
    }

    public void StopWalking() {
        audioS.Stop();
    }
}
