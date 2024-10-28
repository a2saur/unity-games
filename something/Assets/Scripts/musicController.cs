using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip phaseOne;
    public AudioClip phaseTwo;
    public AudioClip phaseThree;
    public AudioClip lowHealth;

    public mainChar mc;
    public voidBoss boss;
    private string currentAudio;

    // Start is called before the first frame update
    void Start()
    {
        SwapAudioClip(phaseOne);
        currentAudio = "phase-1";
    }

    // Update is called once per frame
    void Update()
    {
        if (mc.health == 1){
            if (currentAudio != "low-health"){
                SwapAudioClip(lowHealth);
                currentAudio = "low-health";
            }
        } else {
            if (boss.phase == 1 && currentAudio != "phase-1"){
                SwapAudioClip(phaseOne);
                currentAudio = "phase-1";
            } else if (boss.phase == 2 && currentAudio != "phase-2"){
                SwapAudioClip(phaseTwo);
                currentAudio = "phase-2";
            } else if (boss.phase == 3 && currentAudio != "phase-3"){
                SwapAudioClip(phaseThree);
                currentAudio = "phase-3";
            }
        }
    }

    public void SwapAudioClip(AudioClip newClip)
    {
        audioSource.clip = newClip; // Swap to the new audio clip
        audioSource.Play(); // Play the new clip if needed
    }
}
