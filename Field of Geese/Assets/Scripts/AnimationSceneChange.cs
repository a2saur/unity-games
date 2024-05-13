using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AnimationSceneChange : MonoBehaviour
{
    private Animator animator;
    private float animationDuration = 3.4f; // Duration of the animation

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component attached to the GameObject
        StartCoroutine(PlayAnimationAndChangeScene());
    }

    IEnumerator PlayAnimationAndChangeScene()
    {
        animator.Play("Bread"); // Start the animation by name
        yield return new WaitForSeconds(animationDuration); // Wait for the animation to finish
        SceneManager.LoadScene("Start"); // Change to the next scene
    }
}
