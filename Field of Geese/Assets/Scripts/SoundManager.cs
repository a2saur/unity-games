using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Assign these clips in the Unity Inspector
    public AudioClip defaultClip;
    public AudioClip duckClip;

    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        
        // Check if there is any GameObject with the tag "Duck"
        if (GameObject.FindGameObjectWithTag("Duck") != null)
        {
            // If found, switch to the duckClip
            audioSource.clip = duckClip;
        }
        else
        {
            // If not found, use the default clip
            audioSource.clip = defaultClip;
        }

        // // Play the assigned clip
        audioSource.Play();
    }

    void Update()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        
        // Check if there is any GameObject with the tag "Duck"
        if (GameObject.FindGameObjectWithTag("Duck") != null)
        {
            // If found, switch to the duckClip
            if (audioSource.clip != duckClip){
                audioSource.clip = duckClip;
                audioSource.Play();
            }
        }
        else
        {
            // If not found, use the default clip
            // audioSource.clip = defaultClip;
            if (audioSource.clip != defaultClip){
                audioSource.clip = defaultClip;
                audioSource.Play();
            }
        }
    }
}
