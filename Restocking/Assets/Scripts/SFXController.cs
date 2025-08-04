using UnityEngine;

public class SFXController : MonoBehaviour
{
    public AudioSource audioS;
    public AudioClip boxUpSFX;
    public AudioClip boxDownSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        audioS.volume = ((float) SettingsManager.soundVolume)/10.0f;
    }

    public void PlayBoxUp() {
        audioS.PlayOneShot(boxUpSFX);
    }

    public void PlayBoxDown() {
        audioS.PlayOneShot(boxDownSFX);
    }
}
